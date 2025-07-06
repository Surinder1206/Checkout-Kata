namespace Checkout.Pricing;

public class UnitPriceRule(string sku, int unitPrice) : IPricingRule
{
    public string SKU { get; } = sku;
    private readonly int _unitPrice = unitPrice;

    public int CalculatePrice(int quantity) => quantity * _unitPrice;
}
