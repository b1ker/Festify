using System;
using System.Threading.Tasks;
using Festify.Promotion.Data;

namespace Festify.Promotion.Sales;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly PromotionContext repository;
    
    public PaymentProcessor(PromotionContext repository)
    {
        this.repository = repository;
    }
    public async Task ProcessCreditCardPayment(string creditCardNumber, decimal total)
    {
        // We are just adding payment info, in real life this operations is more complex
        await repository.InsertPayment(new Payment
        {
            PaymentGuid = new Guid(),
            CreditCardNumber = creditCardNumber,
            Total = total,
            PaidDate = DateTime.UtcNow
        });
    }

    public string ObfuscateCreditCardNumber(string creditCardNumber)
    {
        if (creditCardNumber.Length > 4)
        {
            creditCardNumber = creditCardNumber.Remove(0, creditCardNumber.Length - 4).Insert(0, "X");
        }

        return creditCardNumber;
    }
}