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
}

