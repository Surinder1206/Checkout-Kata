namespace Checkout.Pricing;

public class CompositePricingRule(string sku, IEnumerable<IPricingRule> rules) : IPricingRule
{
    public string SKU { get; } = sku;
    private readonly List<IPricingRule> _rules = [.. rules];

    public int CalculatePrice(int quantity)
    {
        foreach (var rule in _rules)
        {
            int price = rule.CalculatePrice(quantity);
            if (price > 0)
                return price;
        }

        return 0;
    }
}

