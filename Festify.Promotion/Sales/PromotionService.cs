using Festify.Promotion.Messages.Sales;
using MassTransit;
using System.Threading.Tasks;
using Festify.Promotion.Shows;

namespace Festify.Promotion.Sales;

public class PromotionService
{
    private IPublishEndpoint publishEndpoint;
    private IPaymentProcessor paymentProcessor;

    public PromotionService(IPublishEndpoint publishEndpoint, IPaymentProcessor paymentProcessor)
    {
        this.publishEndpoint = publishEndpoint;
        this.paymentProcessor = paymentProcessor;
    }

    public async Task PurchaseTicket(Show show, string creditCardNumber, decimal total)
    {
        await paymentProcessor.ProcessCreditCardPayment(creditCardNumber, total);
        await publishEndpoint.Publish(new OrderPlaced());
    }
}
