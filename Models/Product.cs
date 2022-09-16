using System.ComponentModel.DataAnnotations;

namespace Mekatronik.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Product Image is Required")]
        public string ProductImage { get; set; } = null!;
        [Required(ErrorMessage = "Product Price is Required")]
        public double ProductPrice { get; set; }
        [Required(ErrorMessage = "Product Code is Required")]
        public string ProductCode { get; set; } = null!;
        public DateTime ProductCreateDate { get; set; } 
        public DateTime ProductUpdateDate { get; set; }
        public bool ProductStatus { get; set; } = true;
    }
}
