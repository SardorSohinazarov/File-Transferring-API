using System.ComponentModel.DataAnnotations;

namespace FileTransferringWebAPI.Entities
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Message kiritilishi shart")]
        [MaxLength(15,ErrorMessage = "Nameni uzunligi 15 dan kam bo'lishi kerak")]
        public string Name { get; set; }

        public List<ProductImage>? Images { get; set; }
    }
}
