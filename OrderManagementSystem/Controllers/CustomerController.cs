using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Dtos;
using OrderManagementSystem.Models;
using OrderManagementSystem.Repositories;
using AutoMapper;
using OrderManagementSystem.Dots;

namespace OrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Email = dto.Email
            };

            await _customerRepository.AddAsync(customer);
            await _customerRepository.SaveChangesAsync();

            return Ok(customer);
        }


        [HttpGet("{customerId}/orders")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                return NotFound();

            var orderDtos = _mapper.Map<List<OrderDto>>(customer.Orders);
            return Ok(orderDtos);
        }
    }
}
