namespace Yame.Core.Mana;

public enum ManaColor
{
    White,
    Blue,
    Black,
    Red,
    Green,
}

public static class ManaColorHelper
{
    public static ManaColor Parse(string token) => token switch
    {
        "W" => ManaColor.White,
        "U" => ManaColor.Blue,
        "B" => ManaColor.Black,
        "R" => ManaColor.Red,
        "G" => ManaColor.Green,
        _ => throw new FormatException($"Invalid mana color: {token}")
    };

    public static string ToToken(ManaColor color) => color switch
    {
        ManaColor.White => "W",
        ManaColor.Blue => "U",
        ManaColor.Black => "B",
        ManaColor.Red => "R",
        ManaColor.Green => "G",
        _ => throw new ArgumentOutOfRangeException(nameof(color), color, $"Unknown {nameof(ManaColor)} value")
    };
}