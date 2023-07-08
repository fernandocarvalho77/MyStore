using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MyStore.Data;

namespace MyStore.Models;

public class Client
{
    public Client()
    {
        Sales = new List<Sale>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "The Full Name field is required.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "The Email field is required.")]
    [EmailAddress(ErrorMessage = "The provided Email is not valid.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The Date of Birth field is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "The Address field is required.")]
    [Display(Name = "Address")]
    public string Address { get; set; }

    [Required(ErrorMessage = "The City field is required.")]
    [Display(Name = "City")]
    public string City { get; set; }

    [Required(ErrorMessage = "The Postal Code field is required.")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }

    [Required(ErrorMessage = "The NIF field is required.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "The NIF must have 9 digits.")]
    [Display(Name = "NIF")]
    public string NIF { get; set; }

    [Required(ErrorMessage = "The Customer Number field is required.")]
    [Display(Name = "Customer Number")]
    public int CustomerNumber { get; set; }

    public List<Sale> Sales { get; set; }
}