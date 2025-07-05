namespace Checkout;

public class Checkout(Dictionary<string, int> pricingRules) : ICheckout
{
    private readonly Dictionary<string, int> _pricingRules = pricingRules
        ?? throw new ArgumentNullException(nameof(pricingRules), "Pricing rules must not be null");


    public void Scan(string item)
    {
        if (string.IsNullOrWhiteSpace(item))
            throw new ArgumentNullException(nameof(item), "SKU must not be null or empty");

        if (!_pricingRules.ContainsKey(item))
            throw new ArgumentException($"Unknown SKU scanned: {item}");
    }

    public int GetTotalPrice()
    {
        return 0;
    }
}
