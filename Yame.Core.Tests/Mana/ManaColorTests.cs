using Yame.Core.Mana;

namespace Yame.Core.Tests.Mana;

public class ManaColorTests
{
    [Theory]
    [InlineData("W", ManaColor.White)]
    [InlineData("U", ManaColor.Blue)]
    [InlineData("B", ManaColor.Black)]
    [InlineData("R", ManaColor.Red)]
    [InlineData("G", ManaColor.Green)]
    public void Parse_ValidToken_ParsesCorrectly(string token, ManaColor expected)
    {
        var result = ManaColorHelper.Parse(token);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("X")]
    [InlineData("WU")]
    [InlineData("1")]
    public void Parse_InvalidToken_ThrowsFormatException(string token)
    {
        Action act = () => ManaColorHelper.Parse(token);

        act.Should().Throw<FormatException>()
           .WithMessage("Invalid mana color*");
    }

    [Theory]
    [InlineData(ManaColor.White, "W")]
    [InlineData(ManaColor.Blue, "U")]
    [InlineData(ManaColor.Black, "B")]
    [InlineData(ManaColor.Red, "R")]
    [InlineData(ManaColor.Green, "G")]
    public void ToToken_ValidColor_ReturnsCorrectToken(ManaColor color, string expected)
    {
        var token = ManaColorHelper.ToToken(color);

        token.Should().Be(expected);
    }
}
