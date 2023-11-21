using System.ComponentModel.DataAnnotations;

namespace FileTransferringWebAPI.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }

        public string FilePath { get; set; }

        public long ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
