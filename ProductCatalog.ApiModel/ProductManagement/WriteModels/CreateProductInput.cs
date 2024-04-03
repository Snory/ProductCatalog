using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ApiModel.ProductManagement.WriteModels
{
    public class CreateProductInput
    {

        [Required]
        public required string Ean {  get; set; }
        
        [Required]
        public required string Description { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public int Price { get; set; }
        
        [Required]
        public required string CurrencyCode { get; set; }
    }
}
