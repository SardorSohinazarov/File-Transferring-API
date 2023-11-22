using FileTransferringWebAPI.DTOs;
using FileTransferringWebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace FileTransferringWebAPI.Controllers
{
    public partial class ProductController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProductImageAsync([FromForm] ProductImageCreationDTO imageDTO)
        {
            var file = imageDTO.File;

            var filePath = Path.Combine(
                _environment.WebRootPath,
                Guid.NewGuid().ToString() + "-" + file.Name + Path.GetExtension(file.FileName));

            FileStream fileStream = System.IO.File.Create(filePath);
            await file.CopyToAsync(fileStream);
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
        public async Task<IActionResult> DownloadFile(string filepath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }
    }
}
