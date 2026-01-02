namespace Yame.Core.Mana.Symbols;

public record GenericMana(int Amount) : ManaSymbol
{
    public override int ManaValue => Amount;

    public override string ToString() => $"{{{Amount}}}";
}
