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

        if (_scannedItems.TryGetValue(item, out var count))
        {
            _scannedItems[item] = count + 1;
            return;
        }
        _scannedItems[item] = 1;
    }

    public int GetTotalPrice()
    {
        var totalPrice = 0;
        foreach (var item in _scannedItems)
        {
            if (_pricingRules.TryGetValue(item.Key, out int unitPrice))
            {
                totalPrice += unitPrice * item.Value;
            }
        }

        return totalPrice;
    }
}
