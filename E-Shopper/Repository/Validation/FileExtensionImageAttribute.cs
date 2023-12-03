using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace E_Shopper.Repository.Validation
{
    public class FileExtensionImageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "png", "jpg", "jpeg" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if(!result)
                {
                    return new ValidationResult($"file chọn phải là các file có phần mở rộng là {string.Join(", ",extensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}
