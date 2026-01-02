namespace Yame.Core.Mana.Symbols;

public record ColorlessMana : ManaSymbol
{
    internal override (string[] Symbols, string[] Suffix) GetSymbolComponents() => (["C"], []);
}
