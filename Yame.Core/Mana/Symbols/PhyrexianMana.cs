namespace Yame.Core.Mana.Symbols;

public record PhyrexianMana : ColoredMana
{
    public PhyrexianMana(ManaColor color)
        : base(color)
    { }

    internal override (string[] Symbols, string[] Suffix) GetSymbolComponents() => ([ManaColorHelper.ToToken(Color)], ["P"]);
}