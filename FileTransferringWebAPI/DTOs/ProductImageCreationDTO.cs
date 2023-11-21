using FileTransferringWebAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace FileTransferringWebAPI.DTOs
{
    public class ProductImageCreationDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Product Id Majbur")]
        [Range(0, long.MaxValue, ErrorMessage = "Product Id bu oraliqda bo'lishi mumkin emas")]
        public long ProductId { get; set; }

        public IFormFile File { get; set; }

    }
}
