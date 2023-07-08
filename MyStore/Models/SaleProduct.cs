using System.ComponentModel.DataAnnotations;

namespace MyStore.Models;

public class SaleProduct
{
    public SaleProduct()
    {
        Sales = new List<Sale>();
        Products = new List<Product>();
    }
    
    [Required(ErrorMessage = "The Quantity Value field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The Quantity Value must be greater than or equal to zero.")]
    [Display(Name = "Quantity")]
    public decimal Quantity { get; set; }
    
    [Required(ErrorMessage = "The Sum Value field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The Sum Value must be greater than or equal to zero.")]
    [Display(Name = "Sum Value")]
    public decimal SumValue { get; set; }
    
    public int SaleId { get; set; }
    public List<Sale> Sales { get; set; }

    public int ProductId { get; set; }
    public List<Product> Products { get; set; }
    
}