using FileTransferringWebAPI.DTOs;
using FileTransferringWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileTransferringWebAPI.Controllers
{
    public partial class ProductController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductImageAsync([FromForm] ProductImageCreationDTO imageDTO)
        {
            var filePath = Path.Combine(
                _environment.WebRootPath,
                Guid.NewGuid().ToString() + "-" + imageDTO.File.Name + Path.GetExtension(imageDTO.File.FileName));

            FileStream fileStream = System.IO.File.Create(filePath);
            await imageDTO.File.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            fileStream.Close();

            var productImage = new ProductImage()
            {
                ProductId = imageDTO.ProductId,
                FilePath = filePath
            };

            var addedProductImages = await _context.ProductImages.AddAsync(productImage);
            await _context.SaveChangesAsync();

            return Ok(addedProductImages.Entity);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductImagesAsync()
            => Ok(await _context.ProductImages.ToListAsync());

        [HttpGet]
        public async Task<IActionResult> GetProductImageByProductIdAsync(long id)
        {
            var product = await _context.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);

            return File(System.IO.File.ReadAllBytes(product.Images[0].FilePath), "image/png");
        }
    }
}
