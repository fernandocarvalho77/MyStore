using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyStore.Models;

public class Product
{
    public Product()
    {
        SaleProducts = new List<SaleProduct>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "The Description field is required.")]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "The Final Price field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The Final Price must be greater than or equal to zero.")]
    [Display(Name = "Final Price")]
    public decimal FinalPrice { get; set; }

    [Required(ErrorMessage = "The Stock Quantity field is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "The Stock Quantity must be greater than or equal to zero.")]
    [Display(Name = "Stock Quantity")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "The Weight field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The Weight must be greater than or equal to zero.")]
    [Display(Name = "Weight")]
    public double Weight { get; set; }

    [Required(ErrorMessage = "The Category field is required.")]
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "The Category field is required.")]
    [Display(Name = "Category")]
    public Category Category { get; set; }

    public ICollection<SaleProduct> SaleProducts { get; set; }
}