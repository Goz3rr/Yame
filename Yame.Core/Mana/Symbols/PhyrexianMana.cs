using Yame.Core.Mana.Symbols;

namespace Yame.Core.Mana;

public record PhyrexianMana : ColoredMana
{
    public PhyrexianMana(ManaColor color)
        : base(color)
    { }

    public override string ToString() => $"{{{ManaColorHelper.ToToken(Color)}/P}}";
}