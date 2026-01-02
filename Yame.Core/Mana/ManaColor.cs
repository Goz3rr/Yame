namespace Yame.Core.Mana;

public enum ManaColor
{
    Colorless = 0,
    White = 1,
    Blue = 2,
    Black = 3,
    Red = 4,
    Green = 5,
}

public static class ManaColorHelper
{
    public static ManaColor Parse(string token) => token switch
    {
        "C" => ManaColor.Colorless,
        "W" => ManaColor.White,
        "U" => ManaColor.Blue,
        "B" => ManaColor.Black,
        "R" => ManaColor.Red,
        "G" => ManaColor.Green,
        _ => throw new FormatException($"Invalid mana color: {token}")
    };
}