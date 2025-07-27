using AutoMapper;
using OrderManagementSystem.Dtos;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Customer, CustomerDto>();

            CreateMap<OrderDto, Order>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<Invoice, InvoiceDto>()
    .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Order.Status));
        }
    }
}
