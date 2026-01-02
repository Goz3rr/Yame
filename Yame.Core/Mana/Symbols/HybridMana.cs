namespace Yame.Core.Mana.Symbols;

public record HybridMana(ManaSymbol Left, ManaSymbol Right) : ManaSymbol
{
    public override int ManaValue => Math.Max(Left.ManaValue, Right.ManaValue);

    public override IReadOnlySet<ManaColor> ColorIdentity => new HashSet<ManaColor>([.. Left.ColorIdentity, .. Right.ColorIdentity]);

    public HybridMana(ManaColor leftColor, ManaColor rightColor)
        : this(new ColoredMana(leftColor), new ColoredMana(rightColor))
    { }

    public HybridMana(int generic, ManaColor colored)
        : this(new GenericMana(generic), new ColoredMana(colored))
    { }

    public override int GetDevotion(ManaColor color) => Left.GetDevotion(color) + Right.GetDevotion(color);

    internal override (string[] Symbols, string[] Suffix) GetSymbolComponents()
    {
        var l = Left.GetSymbolComponents();
        var r = Right.GetSymbolComponents();
        return ([.. l.Symbols, .. r.Symbols], [.. l.Suffix, .. r.Suffix]);
    }
}
