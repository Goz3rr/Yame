using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana.Symbols;

public class ColoredManaTests
{
    [Theory]
    [InlineData(ManaColor.White)]
    [InlineData(ManaColor.Blue)]
    [InlineData(ManaColor.Black)]
    [InlineData(ManaColor.Red)]
    [InlineData(ManaColor.Green)]
    public void ColoredMana_HasManaValueOne(ManaColor color)
    {
        var mana = new ColoredMana(color);

        mana.ManaValue.Should().Be(1);
    }

    [Theory]
    [InlineData(ManaColor.White)]
    [InlineData(ManaColor.Blue)]
    [InlineData(ManaColor.Black)]
    [InlineData(ManaColor.Red)]
    [InlineData(ManaColor.Green)]
    public void ColoredMana_ColorIdentity_IsItsColor(ManaColor color)
    {
        var mana = new ColoredMana(color);

        mana.ColorIdentity.Should().BeEquivalentTo([color]);
    }

    [Fact]
    public void ColoredMana_Devotion_MatchesColor()
    {
        var mana = new ColoredMana(ManaColor.Green);

        mana.GetDevotion(ManaColor.Green).Should().Be(1);
        mana.GetDevotion(ManaColor.Blue).Should().Be(0);
    }

    [Fact]
    public void ColoredMana_ToString_IsCanonical()
    {
        var mana = new ColoredMana(ManaColor.Green);

        mana.ToString().Should().Be("{G}");
    }
}
