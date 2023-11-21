using System.ComponentModel.DataAnnotations;

namespace FileTransferringWebAPI.CustomAnatations
{
    public class ExtensionsAttribute:ValidationAttribute
    {
        private readonly string[] _extensions;
        public ExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);

                if(!_extensions.Contains(extension))
                {
                    return new ValidationResult($"Bizda faqat ({string.Join(',', _extensions)}) shu extensiondagi filelarni yuklashga ruxsat bor");
                }
            }

            return ValidationResult.Success;
        }
    }
}
