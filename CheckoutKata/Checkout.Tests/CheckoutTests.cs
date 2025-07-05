using FluentAssertions;
using NUnit.Framework;

namespace Checkout.Tests;

internal class CheckoutTests
{
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
        var pricingRules = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };
        var checkout = new Checkout(pricingRules);

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
        var pricingRules = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };

        var sku = "invalid";
        var checkout = new Checkout(pricingRules);

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
        var pricingRules = new Dictionary<string, int>
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 }
        };

        Checkout checkout = new(pricingRules);

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(0);
    }

    [Test]
    public void GetTotalPrice_should_return_total_price_when_item_A_is_scanned()
    {
        // Arrange
        var pricingRules = new Dictionary<string, int>
        {
            { "A", 50 }
        };

        Checkout checkout = new(pricingRules);
        checkout.Scan("A");

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(50);
    }

    [Test]
    public void GetTotalPrice_should_return_total_price_when_item_B_is_scanned()
    {
        // Arrange
        var pricingRules = new Dictionary<string, int>
        {
            { "B", 30 }
        };

        Checkout checkout = new(pricingRules);
        checkout.Scan("B");

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(30);
    }

    [Test]
    public void GetTotalPrice_should_return_total_price_when_item_C_is_scanned()
    {
        // Arrange
        var pricingRules = new Dictionary<string, int>
        {
            { "C", 20 }
        };

        Checkout checkout = new(pricingRules);
        checkout.Scan("C");

        // Act
        var totalPrice = checkout.GetTotalPrice();

        // Assert
        totalPrice.Should().Be(20);
    }
}

