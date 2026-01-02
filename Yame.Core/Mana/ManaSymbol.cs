namespace Yame.Core.Mana;

public abstract record ManaSymbol
{
    public virtual int ManaValue => 1;
    public virtual IReadOnlySet<ManaColor> ColorIdentity => new HashSet<ManaColor>([]);

    public virtual int GetDevotion(ManaColor color) => 0;
}