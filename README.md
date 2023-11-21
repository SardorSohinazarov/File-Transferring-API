# File-Transferring-API

# Bu repository file transfer va Data Anotation (Validation)  ga yo'naltirilgan

> Filelarni upload qilish 

```csharp
var filePath = Path.Combine(
    _environment.WebRootPath,
    Guid.NewGuid().ToString() + "-" + file.Name + Path.GetExtension(file.FileName));

FileStream fileStream = System.IO.File.Create(filePath);
await file.CopyToAsync(fileStream);
await fileStream.FlushAsync();
fileStream.Close();

//IFormFile typedagi fileni olib yangi filePath yaratib o'sha pathga ko'chirib o'tayapmiz
```

> Custom Validation Attribute yozish

```csharp
 [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
 //bu attributeni faqat property yoki fieldga qo'llash mumkin
 public class ExtensionsAttribute : ValidationAttribute //voris olamiz
 {
     private readonly string[] _extensions;
     public ExtensionsAttribute(string[] extensions) //constructor orqali attribute malumotlari kirib kaladi -> [Extensions(new string[".jpg",".png"])]
     {
         _extensions = extensions;
     }

     protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) // value bu qiymat
     {
         var file = value as IFormFile;

         if (file != null)
         {
             var extension = Path.GetExtension(file.FileName);

             if (!_extensions.Contains(extension))
             {
                 return new ValidationResult($"Bizda faqat ({string.Join(',', _extensions)}) shu extensiondagi filelarni yuklashga ruxsat bor");
             }
         }

         return ValidationResult.Success;
     }
 }
```

Bu mark down [bu yerdan o'rganib oldim](https://www.markdownguide.org)

