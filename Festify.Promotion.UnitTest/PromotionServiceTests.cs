using Festify.Promotion.Messages.Sales;
using Festify.Promotion.Sales;
using Festify.Promotion.Shows;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Festify.Promotion.UnitTest;

public class PromotionServiceTests
{
    [Fact]
    public async Task WhenCustomerPurchasesItem_ThenPurchaseIsPublished()
    {
        InMemoryTestHarness harness = new InMemoryTestHarness();
        await harness.Start();

        // Arrange
        IPublishEndpoint publishEndpoint = harness.Bus;
        var producer = new PromotionService(publishEndpoint, new FakePaymentProcessor());

        // Act
        await producer.PurchaseTicket(new Show(),  string.Empty, Decimal.Zero);

        await harness.InactivityTask;

        // Assert
        harness.Published.Select<OrderPlaced>()
            .Count().Should().Be(1);

        await harness.Stop();
    }

    [Fact]
    public async Task WhenCustomerPurchasesItem_ThenCustomerIsCharged()
    {
        const string cardNumber = "123456";
        const decimal amount = 21.12m;
        
        var fakePaymentProcessor = new FakePaymentProcessor();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<PromotionService>();
        serviceCollection.AddSingleton<IPaymentProcessor>(fakePaymentProcessor);
        serviceCollection.AddMassTransitInMemoryTestHarness(cfg =>
        {
        });
        await using var provider = serviceCollection.BuildServiceProvider(true);

        var harness = provider.GetRequiredService<InMemoryTestHarness>();
        await harness.Start();

        using (var scope = provider.CreateScope())
        {
            var producer = scope.ServiceProvider.GetRequiredService<PromotionService>();
            await producer.PurchaseTicket(
                new Show(), cardNumber, amount);

            await harness.InactivityTask;

            fakePaymentProcessor.Payments.Count().Should().Be(1);
            fakePaymentProcessor.Payments.Should().Contain(
                new FakePaymentProcessor.Payment(cardNumber, amount)
            );
        }

        await harness.Stop();
    }
}