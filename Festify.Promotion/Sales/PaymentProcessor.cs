using System.Threading.Tasks;

namespace Festify.Promotion.Sales;

public class PaymentProcessor : IPaymentProcessor
{
    public Task ProcessCreditCardPayment(string creditCardNumber, decimal total)
    {
        throw new System.NotImplementedException();
    }
}