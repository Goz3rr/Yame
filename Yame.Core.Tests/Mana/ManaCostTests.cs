using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana;

public class ManaCostTests
{
    [Fact]
    public void ManaCost_SumsSymbolManaValues()
    {
        var cost = new ManaCost(
            new GenericMana(2),
            new ColoredMana(ManaColor.Green),
            new HybridMana(ManaColor.Green, ManaColor.Blue),
            new VariableMana('X')
        );

        cost.ManaValue.Should().Be(4);
    }

    [Fact]
    public void ColorIdentity_IsUnionOfAllSymbols()
    {
        var cost = new ManaCost(
            new GenericMana(2),
            new ColoredMana(ManaColor.Green),
            new HybridMana(ManaColor.Green, ManaColor.Blue),
            new PhyrexianMana(ManaColor.Black)
        );

        cost.ColorIdentity.Should().BeEquivalentTo([ManaColor.Green, ManaColor.Blue, ManaColor.Black]);
    }

    [Fact]
    public void ColorIdentity_Ignores_Generic_And_Colorless()
    {
        var cost = new ManaCost(
            new GenericMana(2),
            new ColorlessMana()
        );

        cost.ColorIdentity.Should().BeEmpty();
    }

    [Fact]
    public void ToString_ConcatenatesSymbolStringsInOrder()
    {
        var cost = new ManaCost(
            new GenericMana(2),
            new ColoredMana(ManaColor.Green),
            new HybridMana(ManaColor.Green, ManaColor.Blue),
            new PhyrexianMana(ManaColor.Black),
            new VariableMana('X')
        );

        cost.ToString().Should().Be("{2}{G}{G/U}{B/P}{X}");
    }
}
