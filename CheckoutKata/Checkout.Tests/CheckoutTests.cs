using FluentAssertions;
using NUnit.Framework;

namespace Checkout.Tests;

internal class CheckoutTests
{
    private Dictionary<string, int> _pricingRules;

    [SetUp]
    public void Initialize()
    {
        _pricingRules = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };
    }

    [Test]
    public void Constructor_should_throw_argument_null_exception_when_pricing_rules_are_null()
    {
        // Arrange & Act
        Action act = () => new Checkout(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("pricingRules");
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Scan_should_throw_argument_exception_when_SKU_is_null_or_empty_or_whitespace(string sku)
    {
        // Arrange
        var checkout = new Checkout(_pricingRules);

        // Act
        Action act = () => checkout.Scan(sku);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithParameterName("item");

    }

    [Test]
    public void Scan_should_throw_argument_exception_when_unknown_item_is_scanned()
    {
        // Arrange
        var sku = "invalid";
        var checkout = new Checkout(_pricingRules);

        // Act
        Action act = () => checkout.Scan(sku);

        // Assert
        act.Should().Throw<ArgumentException>()
           .WithMessage($"Unknown SKU scanned: {sku}");
    }

    [Test]
    public void GetTotalPrice_should_return_zero_when_no_items_scanned()
    {
        // Arrange
        Checkout checkout = new(_pricingRules);

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(0);
    }

    [TestCase("A", 50)]
    [TestCase("B", 30)]
    [TestCase("C", 20)]
    [TestCase("D", 15)]
    public void GetTotalPrice_should_return_total_price_when_single_item_is_scanned(string scannedItem, int expectedPrice)
    {
        // Arrange
        Checkout checkout = new(_pricingRules);
        checkout.Scan(scannedItem);

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(expectedPrice);
    }

    [TestCase(new[] { "A", "B" }, 80)]
    [TestCase(new[] { "A", "C" }, 70)]
    [TestCase(new[] { "A", "D" }, 65)]
    [TestCase(new[] { "B", "C" }, 50)]
    [TestCase(new[] { "A", "B", "C" }, 100)]
    [TestCase(new[] { "A", "B", "C", "D" }, 115)]
    public void GetTotalPrice_should_return_correct_total_for_multiple_scanned_items(string[] scannedItems, int expectedTotal)
    {
        // Arrange
        var checkout = new Checkout(_pricingRules);
        foreach (var item in scannedItems)
        {
            checkout.Scan(item);
        }

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(expectedTotal);
    }
}

