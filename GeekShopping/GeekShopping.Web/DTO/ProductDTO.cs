using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Web.DTO
{
    public class ProductDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }

        public string ImageURL { get; set; }

        [Range(1, 100)]
        public int Count { get; set; } = 1;

        public string FormattedName()
        {
            return (Name.Length < 24) ? Name : $"{Name[..21]}...";
        }

        public string FormattedDescription()
        {
            return (Description.Length < 355) ? Description : $"{Description[..352]}...";
        }
    }
}
