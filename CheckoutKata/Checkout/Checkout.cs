namespace Checkout;

public class Checkout(Dictionary<string, int> pricingRules) : ICheckout
{
    private readonly Dictionary<string, int> _pricingRules = pricingRules
        ?? throw new ArgumentNullException(nameof(pricingRules), "Pricing rules must not be null");

    public void Scan(string item)
    {
        throw new NotImplementedException();
    }

    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }
}
