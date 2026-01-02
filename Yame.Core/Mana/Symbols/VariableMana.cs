namespace Yame.Core.Mana.Symbols;

public record VariableMana(char Symbol) : ManaSymbol
{
    public override int ManaValue => 0;

    public override string ToString() => $"{{{Symbol}}}";
}
