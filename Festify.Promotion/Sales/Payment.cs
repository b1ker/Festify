using System;
using Microsoft.EntityFrameworkCore;

namespace Festify.Promotion.Sales;

public class Payment
{
    public int PaymentId { get; set; }
    public Guid PaymentGuid { get; set; }
    public string CreditCardNumber { get; set; }
    [Precision(19, 4)]
    public decimal Total { get; set; }
    public DateTime PaidDate { get; set; }
}