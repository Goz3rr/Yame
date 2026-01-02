using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana.Symbols;

public class HybridManaTests
{
    [Fact]
    public void HybridMana_UsesHighestManaValue()
    {
        var hybrid = new HybridMana(
            new GenericMana(2),
            new ColoredMana(ManaColor.White));

        hybrid.ManaValue.Should().Be(2);
    }

    [Fact]
    public void HybridMana_ColorIdentity_IsUnionOfSides()
    {
        var mana = new HybridMana(ManaColor.Green, ManaColor.Blue);

        mana.ColorIdentity.Should()
            .BeEquivalentTo([ManaColor.Green, ManaColor.Blue]);
    }

    [Fact]
    public void HybridMana_Devotion_AddsBothSides()
    {
        var mana = new HybridMana(ManaColor.Green, ManaColor.Blue);

        mana.GetDevotion(ManaColor.Green).Should().Be(1);
        mana.GetDevotion(ManaColor.Blue).Should().Be(1);
        mana.GetDevotion(ManaColor.Red).Should().Be(0);
    }

    [Fact]
    public void HybridMana_WithColorless_OnlyCountsColoredSide()
    {
        var mana = new HybridMana(
            new ColorlessMana(),
            new ColoredMana(ManaColor.Green));

        mana.GetDevotion(ManaColor.Green).Should().Be(1);
        mana.GetDevotion(ManaColor.Blue).Should().Be(0);
    }

    [Theory]
    [InlineData(ManaColor.Green, ManaColor.Blue, "{G/U}")]
    [InlineData(ManaColor.White, ManaColor.Black, "{W/B}")]
    [InlineData(ManaColor.Red, ManaColor.Green, "{R/G}")]
    public void HybridMana_TwoColors_ToString_IsCanonical(ManaColor left, ManaColor right, string expected)
    {
        var mana = new HybridMana(left, right);

        mana.ToString().Should().Be(expected);
    }

    [Theory]
    [InlineData(2, ManaColor.White, "{2/W}")]
    [InlineData(3, ManaColor.Blue, "{3/U}")]
    public void HybridMana_GenericAndColor_ToString_IsCanonical(int generic, ManaColor color, string expected)
    {
        var mana = new HybridMana(generic, color);

        mana.ToString().Should().Be(expected);
    }

    [Theory]
    [InlineData(ManaColor.Green, "{C/G}")]
    [InlineData(ManaColor.Red, "{C/R}")]
    public void HybridMana_ColorlessAndColor_ToString_IsCanonical(ManaColor color, string expected)
    {
        var mana = new HybridMana(
            new ColorlessMana(),
            new ColoredMana(color));

        mana.ToString().Should().Be(expected);
    }

    [Fact]
    public void HybridPhyrexianMana_ToString_IsCanonical()
    {
        var mana = new HybridMana(
            new PhyrexianMana(ManaColor.Black),
            new PhyrexianMana(ManaColor.Green));

        mana.ToString().Should().Be("{B/G/P}");
    }
}
