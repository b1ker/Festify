using System.Threading.Tasks;

namespace Festify.Promotion.Sales;

public interface IPaymentProcessor
{
    Task ProcessCreditCardPayment(string creditCardNumber, decimal total);
}