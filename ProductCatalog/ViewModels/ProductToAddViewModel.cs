using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.DTOs
{
    public class ProductToAddViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days")]
        public int DurationInDays { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }


    }
}
