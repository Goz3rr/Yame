namespace Yame.Core.Mana.Symbols;

public record GenericMana(int Amount) : ManaSymbol
{
    public override int ManaValue => Amount;

    internal override (string[] Symbols, string[] Suffix) GetSymbolComponents() => ([Amount.ToString()], []);
}
