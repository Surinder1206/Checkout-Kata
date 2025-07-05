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
}

