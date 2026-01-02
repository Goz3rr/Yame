namespace Yame.Core.Mana;

public abstract record ManaSymbol;

public record ColoredMana(ManaColor Color) : ManaSymbol;

public record GenericMana(int Amount) : ManaSymbol;

public record VariableMana(char Symbol) : ManaSymbol;

public record HybridMana(ManaSymbol Left, ManaSymbol Right) : ManaSymbol
{
    public HybridMana(ManaColor leftColor, ManaColor rightColor)
        : this(new ColoredMana(leftColor), new ColoredMana(rightColor))
    { }

    public HybridMana(int generic, ManaColor colored)
        : this(new GenericMana(generic), new ColoredMana(colored))
    { }
}

public record PhyrexianMana(ManaColor Color) : ManaSymbol;