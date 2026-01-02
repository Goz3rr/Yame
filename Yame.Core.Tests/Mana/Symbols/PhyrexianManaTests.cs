using Yame.Core.Mana;

namespace Yame.Core.Tests.Mana.Symbols;

public class PhyrexianManaTests
{
    [Theory]
    [InlineData(ManaColor.Green)]
    [InlineData(ManaColor.Blue)]
    public void PhyrexianMana_HasManaValueOne(ManaColor color)
    {
        var mana = new PhyrexianMana(color);

        mana.ManaValue.Should().Be(1);
    }

    [Fact]
    public void PhyrexianMana_ColorIdentity_IsItsColor()
    {
        var mana = new PhyrexianMana(ManaColor.Black);

        mana.ColorIdentity.Should().BeEquivalentTo([ManaColor.Black]);
    }

    [Fact]
    public void PhyrexianMana_Devotion_IsOneForItsColor()
    {
        var mana = new PhyrexianMana(ManaColor.Black);

        mana.GetDevotion(ManaColor.Black).Should().Be(1);
        mana.GetDevotion(ManaColor.White).Should().Be(0);
    }

    [Fact]
    public void PhyrexianMana_ToString_IsCanonical()
    {
        var mana = new PhyrexianMana(ManaColor.Black);

        mana.ToString().Should().Be("{B/P}");
    }
}
