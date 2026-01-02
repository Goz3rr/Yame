namespace Yame.Core.Mana;

public record ManaCost(IReadOnlyList<ManaSymbol> Symbols)
{
    public int ManaValue => Symbols.Sum(s => s.ManaValue);

    public IReadOnlySet<ManaColor> ColorIdentity => new HashSet<ManaColor>([.. Symbols.SelectMany(s => s.ColorIdentity)]);

    public ManaCost()
        : this([])
    { }

    public ManaCost(ManaSymbol symbol)
        : this([symbol])
    { }

    public ManaCost(ManaSymbol symbol, params ManaSymbol[] symbols)
        : this([symbol, .. symbols])
    { }

    public int GetDevotion(ManaColor color) => Symbols.Sum(s => s.GetDevotion(color));

    public override string ToString() => string.Concat(Symbols.Select(s => s.ToString()));
}