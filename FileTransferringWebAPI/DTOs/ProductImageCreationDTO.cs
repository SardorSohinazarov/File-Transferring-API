using FileTransferringWebAPI.Entities;

namespace FileTransferringWebAPI.DTOs
{
    public class ProductImageCreationDTO
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

        public IFormFile File { get; set; }

    }
}
