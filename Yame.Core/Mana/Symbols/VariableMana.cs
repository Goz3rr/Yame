namespace Yame.Core.Mana.Symbols;

public record VariableMana(char Symbol) : ManaSymbol
{
    public override int ManaValue => 0;

    internal override (string[] Symbols, string[] Suffix) GetSymbolComponents() => ([Symbol.ToString()], []);
}
