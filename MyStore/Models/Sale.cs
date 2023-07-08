using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Azure.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyStore.Models;

public class Sale
{
    public Sale()
    {
        SaleIdentifier = $"V" + DateTime.Now.Year + Id.ToString().PadLeft(6, '0');
        SaleProducts = new List<SaleProduct>();
    }

    public int Id { get; set; }

    [Required(ErrorMessage = "The Sale Identifier field is required.")]
    [Display(Name = "Sale Identifier")]
    public string SaleIdentifier { get; set; }

    [Required(ErrorMessage = "The Date field is required.")]
    [DataType(DataType.Date)]
    [Display(Name = "Date")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "The Time field is required.")]
    [DataType(DataType.Time)]
    [Display(Name = "Time")]
    public DateTime Time { get; set; }

    [Required(ErrorMessage = "The Client field is required.")]
    [Display(Name = "Client")]
    public int? ClientId { get; set; }

    [ValidateNever]
    [Required(ErrorMessage = "The Client field is required.")]
    [Display(Name = "Client")]
    public Client? Client { get; set; }

    [Display(Name = "Observations")] public string? Observations { get; set; }

    [Required(ErrorMessage = "The Final Value field is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "The Final Value must be greater than or equal to zero.")]
    [Display(Name = "Final Value")]
    public decimal FinalValue { get; set; }

    [Required(ErrorMessage = "The Status field is required.")]
    [Display(Name = "Status")]
    public SaleStatus Status { get; set; }

    [Required(ErrorMessage = "The Paid field is required.")]
    [Display(Name = "Paid")]
    public bool Paid { get; set; }

    public ICollection<SaleProduct> SaleProducts { get; set; }
}

public enum SaleStatus
{
    [Display(Name = "Pending")] Pending = 1,
    [Display(Name = "Complete")] Paid = 2,
    [Display(Name = "Paid")] Canceled = 3,
    [Display(Name = "Canceled")] Complete = 4
}