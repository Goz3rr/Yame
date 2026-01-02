using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Core.Tests.Mana.Symbols;

public class ColorlessManaTests
{
    [Fact]
    public void ColorlessMana_HasManaValueOne()
    {
        var mana = new ColorlessMana();

        mana.ManaValue.Should().Be(1);
    }

    [Fact]
    public void ColorlessMana_ColorIdentity_IsEmpty()
    {
        var mana = new ColorlessMana();

        mana.ColorIdentity.Should().BeEmpty();
    }

    [Fact]
    public void ColorlessMana_HasNoDevotion()
    {
        var mana = new ColorlessMana();

        mana.GetDevotion(ManaColor.Blue).Should().Be(0);
    }

    [Fact]
    public void ColorlessMana_ToString_IsCanonical()
    {
        var mana = new ColorlessMana();

        mana.ToString().Should().Be("{C}");
    }
}
