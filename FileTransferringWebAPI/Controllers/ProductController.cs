using FileTransferringWebAPI.Data;
using FileTransferringWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileTransferringWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public partial class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(Product product)
        {
            var addedProduct = await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return Ok(addedProduct.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            return Ok(await _context.Products.ToListAsync());
        }
    }
}
