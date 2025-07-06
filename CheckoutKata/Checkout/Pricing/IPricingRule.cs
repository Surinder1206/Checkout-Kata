namespace Checkout.Pricing;

public interface IPricingRule
{
    int CalculatePrice(int quantity);
    string SKU { get; }
}
