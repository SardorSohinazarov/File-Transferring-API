namespace FileTransferringWebAPI.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public List<ProductImage>? Images { get; set; }
    }
}
