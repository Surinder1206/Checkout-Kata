using Checkout.Tests.Builder;
using FluentAssertions;
using NUnit.Framework;

namespace Checkout.Tests;

internal class CheckoutTests
{

    [Test]
    public void Constructor_should_throw_argument_null_exception_when_pricing_rules_are_null()
    {
        // Arrange & Act
        Action act = () => new CheckoutBuilder()
        .WithPricingRules(null!)
        .Build();

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
        var checkout = new CheckoutBuilder().Build();

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
        var checkout = new CheckoutBuilder().Build();

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
        var checkout = new CheckoutBuilder().Build();

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
        var checkout = new CheckoutBuilder()
            .WithScan([scannedItem])
            .Build();

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
        var checkout = new CheckoutBuilder()
            .WithScan(scannedItems)
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(expectedTotal);
    }

    [Test]
    public void GetTotalPrice_should_return_total_when_same_item_A_scanned_two_times()
    {
        // Arrange
        var checkout = new CheckoutBuilder()
            .WithScan(["A", "A"])
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(100);
    }

    [Test]
    public void GetTotalPrice_should_return_discounted_price_when_three_A_items_are_scanned()
    {
        // Arrange
        var checkout = new CheckoutBuilder()
            .WithScan(["A", "A", "A"])
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(130);
    }

    [Test]
    public void GetTotalPrice_should_return_discounted_price_when_six_A_items_are_scanned()
    {
        // Arrange
        var checkout = new CheckoutBuilder()
            .WithScan(["A", "A", "A", "A", "A", "A"])
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(260);
    }

    [Test]
    public void GetTotalPrice_should_return_discounted_price_when_two_B_items_are_scanned()
    {
        // Arrange
        var checkout = new CheckoutBuilder()
            .WithScan(["B", "B"])
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(45);
    }

    [Test]
    public void GetTotalPrice_should_return_discounted_price_when_four_B_items_are_scanned()
    {
        // Arrange
        var checkout = new CheckoutBuilder()
            .WithScan(["B", "B", "B", "B"])
            .Build();

        // Act
        var total = checkout.GetTotalPrice();

        // Assert
        total.Should().Be(90);
    }
}

