using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyStore.Models;

public class Category
{
    public Category()
    {
        SubCategories = new List<Category>();
        Products = new List<Product>();
    }
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name field is required.")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Parent Category")]
    public int? ParentCategoryId { get; set; }

    [ValidateNever]
    [Display(Name = "Parent Category")]
    public Category? ParentCategory { get; set; }

    [ValidateNever]
    public List<Category> SubCategories { get; set; }

    [ValidateNever] 
    public List<Product> Products { get; set; }
}