using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Dtos;
using OrderManagementSystem.Models;
using OrderManagementSystem.Repositories;

using AutoMapper;
using OrderManagementSystem.Dots;
using OrderManagementSystem.Services;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly EmailService _emailService;

        public OrderController(IOrderRepository orderRepository, OrderService orderService, IMapper mapper, IProductRepository productRepository , IInvoiceRepository  invoiceRepository , EmailService emailService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
            _mapper = mapper;
            _productRepository = productRepository;
            _invoiceRepository = invoiceRepository;
            _emailService = emailService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        //{
        //    if (createOrderDto.OrderItems == null || !createOrderDto.OrderItems.Any())
        //        return BadRequest("Order must contain at least one item.");

        //    var orderItems = new List<OrderItem>();

        //    foreach (var item in createOrderDto.OrderItems)
        //    {
        //        var product = await _productRepository.GetByIdAsync(item.ProductId);
        //        if (product == null)
        //            return BadRequest($"Product with ID {item.ProductId} not found.");

        //        if (item.Quantity > product.Stock)
        //            return BadRequest($"Insufficient stock for product {product.Name}.");

        //        orderItems.Add(new OrderItem
        //        {
        //            ProductId = item.ProductId,
        //            Quantity = item.Quantity,
        //            UnitPrice = item.UnitPrice,
        //            Discount = item.Discount,
        //            Product = product
        //        });
        //    }

        //    var order = new Order
        //    {
        //        CustomerId = createOrderDto.CustomerId,
        //        OrderDate = DateTime.Now,
        //        Status = "Pending",
        //        PaymentMethod = createOrderDto.PaymentMethod,
        //        OrderItems = orderItems,
        //        TotalAmount = _orderService.CalculateTotalWithDiscount(orderItems)
        //    };

        //    await _orderRepository.AddAsync(order);
        //    await _orderRepository.SaveChangesAsync();

        //    var orderDto = _mapper.Map<OrderDto>(order);
        //    return Ok(orderDto);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (createOrderDto.OrderItems == null || !createOrderDto.OrderItems.Any())
                return BadRequest("Order must contain at least one item.");

            var orderItems = new List<OrderItem>();

            foreach (var item in createOrderDto.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    return BadRequest($"Product with ID {item.ProductId} not found.");

                if (item.Quantity > product.Stock)
                    return BadRequest($"Insufficient stock for product {product.Name}.");

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,
                    Product = product
                });

                // اختياري: ممكن تقلل المخزون كده
                product.Stock -= item.Quantity;
            }

            var order = new Order
            {
                CustomerId = createOrderDto.CustomerId,
                OrderDate = DateTime.Now,
                Status = "Pending",
                PaymentMethod = createOrderDto.PaymentMethod,
                OrderItems = orderItems,
                TotalAmount = _orderService.CalculateTotalWithDiscount(orderItems)
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync(); // ← نحفظ الأوردر الأول

            var invoice = new Invoice
            {
                OrderId = order.OrderId, // ← بعد الحفظ، الـ ID بيكون موجود
                InvoiceDate = DateTime.Now,
                TotalAmount = order.TotalAmount
            };

            await _invoiceRepository.AddAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }



        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return NotFound();

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return NotFound();

            order.Status = newStatus;
            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();

            // ⬇️ إرسال الإيميل
            if (order.Customer?.Email != null)
            {
                string subject = "Order Status Updated";
                string body = $"Dear {order.Customer.Name},<br>Your order #{order.OrderId} status has been updated to <b>{newStatus}</b>.";
                await _emailService.SendEmailAsync(order.Customer.Email, subject, body);
            }

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

    }
}
