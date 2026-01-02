using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana.Symbols;

public class GenericManaTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    public void GenericMana_HasCorrectManaValue(int amount)
    {
        var mana = new GenericMana(amount);

        mana.ManaValue.Should().Be(amount);
    }

    [Fact]
    public void GenericMana_ColorIdentity_IsEmpty()
    {
        var mana = new GenericMana(2);

        mana.ColorIdentity.Should().BeEmpty();
    }

    [Fact]
    public void GenericMana_Devotion_IsZero()
    {
        var mana = new GenericMana(3);

        mana.GetDevotion(ManaColor.Red).Should().Be(0);
    }

    [Fact]
    public void GenericMana_ToString_IsCanonical()
    {
        var mana = new GenericMana(2);

        mana.ToString().Should().Be("{2}");
    }
}
