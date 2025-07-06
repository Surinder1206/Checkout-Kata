using Checkout.Pricing;

namespace Checkout.Tests.Builder;

internal class CheckoutBuilder
{
    private readonly List<string> _scannedItems = [];
    private IEnumerable<IPricingRule> _pricingRules = [
            new CompositePricingRule("A", [ new BulkPriceRule("A", 50, 3, 130)]),
            new CompositePricingRule("B", [ new BulkPriceRule("B", 30, 2, 45)]),
            new UnitPriceRule("C", 20),
            new UnitPriceRule("D", 15)
       ];

    public CheckoutBuilder WithScan(IEnumerable<string> items)
    {
        _scannedItems.AddRange(items);
        return this;
    }

    public CheckoutBuilder WithPricingRules(IEnumerable<IPricingRule> pricingRules)
    {
        _pricingRules = pricingRules;
        return this;
    }

    public Checkout Build()
    {
        var checkout = new Checkout(_pricingRules);
        _scannedItems.ToList().ForEach(checkout.Scan);
        return checkout;
    }
}
