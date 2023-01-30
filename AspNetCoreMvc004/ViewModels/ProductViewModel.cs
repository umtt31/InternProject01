using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvc004.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")] 
        public string? Name { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public int? Stock { get; set; }
        
        public string? Color { get; set; }

        public bool IsPublish { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public int Expire { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "This space can not be empty!")]
        public DateTime? PublishDate { get; set; }
    }
}
