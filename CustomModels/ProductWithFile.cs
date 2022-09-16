using System.ComponentModel.DataAnnotations;

namespace Mekatronik.CustomModels
{
    public class ProductWithFile
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Product Name is Required")]
        public string ProductName { get; set; } = null!;
        public IFormFile ProductImage { get; set; } = null!;
        public string ProductImageStr { get; set; } = null!;
        [Required(ErrorMessage = "Product Price is Required")]
        public double ProductPrice { get; set; }
        [Required(ErrorMessage = "Product Code is Required")]
        public string ProductCode { get; set; } = null!;
        public DateTime ProductCreateDate { get; set; } = DateTime.Now;
        public DateTime ProductUpdateDate { get; set; } = DateTime.Now;
        public bool ProductStatus { get; set; } = true;
    }
}
