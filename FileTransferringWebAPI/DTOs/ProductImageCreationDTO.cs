﻿namespace FileTransferringWebAPI.DTOs
{
    public class ProductImageCreationDTO
    {
        public long ProductId { get; set; }

        public IFormFile File { get; set; }
    }
}