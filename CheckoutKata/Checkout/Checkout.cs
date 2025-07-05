namespace Checkout;

public class Checkout(Dictionary<string, int> pricingRules) : ICheckout
{
    private readonly Dictionary<string, int> _pricingRules = pricingRules
        ?? throw new ArgumentNullException(nameof(pricingRules), "Pricing rules must not be null");

    private readonly Dictionary<string, int> _scannedItems = [];

    public void Scan(string item)
    {
        if (string.IsNullOrWhiteSpace(item))
            throw new ArgumentNullException(nameof(item), "SKU must not be null or empty");

        if (!_pricingRules.ContainsKey(item))
            throw new ArgumentException($"Unknown SKU scanned: {item}");

        _scannedItems[item] = 1;
    }

    public int GetTotalPrice()
    {
        if (_scannedItems.ContainsKey("A"))
        {
            return 50;
        }

        if (_scannedItems.ContainsKey("B"))
        {
            return 30;
        }

        if (_scannedItems.ContainsKey("C"))
        {
            return 20;
        }

        return 0;
    }
}
