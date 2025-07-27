using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Dtos;
using AutoMapper;

namespace OrderManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(OrderManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _context.Invoices
                .Include(i => i.Order)
                .ToListAsync();

            var invoiceDtos = _mapper.Map<List<InvoiceDto>>(invoices);
            return Ok(invoiceDtos);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetById(int invoiceId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Order)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

            if (invoice == null)
                return NotFound();

            var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return Ok(invoiceDto);
        }
    }
}
