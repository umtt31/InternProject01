using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc004.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        [StringLength(50, ErrorMessage = "Name attribute can not be greater than 50 chars")]
        // [Remote(action: "HasProductName", controller: "Products")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        [Range(200000, 20000000, ErrorMessage = "Price can not be smaller than 200.000 and can not be greater then 20.000.000")]
        // [RegularExpression(@"^[0 - 9] + (\.[0 - 9]{1, 2})", ErrorMessage = "Price must have maximum 2 digit as decimal")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        [Range(1, 120, ErrorMessage = "Stock can not be greater than 120!")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public string? Color { get; set; }

        public bool IsPublish { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public int? Expire { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        [StringLength(300, MinimumLength = 50, ErrorMessage = "Desription must be between 50 and 300 chars")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public DateTime? PublishDate { get; set; }
    }
}
