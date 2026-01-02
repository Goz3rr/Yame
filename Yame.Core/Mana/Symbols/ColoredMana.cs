namespace Yame.Core.Mana.Symbols;

public record ColoredMana(ManaColor Color) : ManaSymbol
{
    public override IReadOnlySet<ManaColor> ColorIdentity => new HashSet<ManaColor>([Color]);

    public override string ToString() => $"{{{ManaColorHelper.ToToken(Color)}}}";

    public override int GetDevotion(ManaColor color) => Color == color ? 1 : 0;
}
