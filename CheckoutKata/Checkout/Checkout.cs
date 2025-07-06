using Checkout.Pricing;

namespace Checkout;

public class Checkout(IEnumerable<IPricingRule> pricingRules) : ICheckout
{
    private readonly IDictionary<string, IPricingRule> _pricingRules =
        pricingRules?.ToDictionary(r => r.SKU)
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

        foreach (var scannedItem in _scannedItems)
        {
            if (_pricingRules.TryGetValue(scannedItem.Key, out var pricingRule))
            {
                totalPrice += pricingRule.CalculatePrice(scannedItem.Value);
            }
        }

        return totalPrice;
    }
}
