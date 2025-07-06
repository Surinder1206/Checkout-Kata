namespace Checkout.Pricing;

public class BulkPriceRule(string sku, int unitPrice, int bulkQuantity, int bulkPrice) : IPricingRule
{
    public string SKU { get; } = sku;
    private readonly int _unitPrice = unitPrice;
    private readonly int _bulkQuantity = bulkQuantity;
    private readonly int _bulkPrice = bulkPrice;

    public int CalculatePrice(int quantity)
    {
        int parirsCount = quantity / _bulkQuantity;
        int remainder = quantity % _bulkQuantity;

        return parirsCount * _bulkPrice + remainder * _unitPrice;
    }
}
