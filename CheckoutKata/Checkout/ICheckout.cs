namespace Checkout;

internal interface ICheckout
{
    void Scan(string item);

    int GetTotalPrice();
}
