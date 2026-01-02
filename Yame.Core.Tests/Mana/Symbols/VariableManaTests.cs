using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana.Symbols;

public class VariableManaTests
{
    [Theory]
    [InlineData('X')]
    [InlineData('Y')]
    [InlineData('Z')]
    public void VariableMana_HasManaValueZero(char symbol)
    {
        var mana = new VariableMana(symbol);

        mana.ManaValue.Should().Be(0);
    }

    [Theory]
    [InlineData('X')]
    [InlineData('Y')]
    [InlineData('Z')]
    public void VariableMana_ColorIdentity_IsEmpty(char symbol)
    {
        var mana = new VariableMana(symbol);

        mana.ColorIdentity.Should().BeEmpty();
    }

    [Fact]
    public void VariableMana_ToString_IsCanonical()
    {
        var mana = new VariableMana('X');

        mana.ToString().Should().Be("{X}");
    }
}
