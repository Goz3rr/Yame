using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Parser.Tests;

public class ManaNotationParserTests
{
    private readonly ManaNotationParser _sut;

    public ManaNotationParserTests()
    {
        _sut = new ManaNotationParser();
    }

    [Fact]
    public void Parse_EmptyString_ReturnsEmptyManaCost()
    {
        var result = _sut.Parse(string.Empty);
        result.Symbols.Should().BeEmpty();
    }

    [Theory]
    [InlineData("{W}", ManaColor.White)]
    [InlineData("{U}", ManaColor.Blue)]
    [InlineData("{B}", ManaColor.Black)]
    [InlineData("{R}", ManaColor.Red)]
    [InlineData("{G}", ManaColor.Green)]
    public void Parse_SingleColoredMana_ParsesCorrectly(string input, ManaColor expected)
    {
        var result = _sut.Parse(input);

        result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(expected);
    }

    [Fact]
    public void Parse_ColorlessMana_ParsesCorrectly()
    {
        var result = _sut.Parse("{C}");

        result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<ColorlessMana>();
    }

    [Theory]
    [InlineData("{0}", 0)]
    [InlineData("{1}", 1)]
    [InlineData("{2}", 2)]
    [InlineData("{10}", 10)]
    public void Parse_GenericMana_ParsesCorrectly(string input, int expected)
    {
        var result = _sut.Parse(input);

        result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<GenericMana>()
            .Which.Amount.Should().Be(expected);
    }

    [Theory]
    [InlineData("{X}", 'X')]
    [InlineData("{Y}", 'Y')]
    [InlineData("{Z}", 'Z')]
    public void Parse_VariableMana_ParsesCorrectly(string input, char expected)
    {
        var result = _sut.Parse(input);

        result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<VariableMana>()
            .Which.Symbol.Should().Be(expected);
    }

    [Theory]
    [InlineData("{W/U}", ManaColor.White, ManaColor.Blue)]
    [InlineData("{U/B}", ManaColor.Blue, ManaColor.Black)]
    [InlineData("{R/G}", ManaColor.Red, ManaColor.Green)]
    public void Parse_HybridMana_ParsesCorrectly(
        string input,
        ManaColor leftExpected,
        ManaColor rightExpected)
    {
        var result = _sut.Parse(input);

        var hybrid = result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<HybridMana>().Which;

        hybrid.Left.Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(leftExpected);

        hybrid.Right.Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(rightExpected);
    }

    [Theory]
    [InlineData("{C/W}", ManaColor.White)]
    [InlineData("{C/U}", ManaColor.Blue)]
    public void Parse_ColorlessHybridMana_ParsesCorrectly(string input, ManaColor color)
    {
        var result = _sut.Parse(input);

        var hybrid = result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<HybridMana>().Which;

        hybrid.Left.Should().BeOfType<ColorlessMana>();
        hybrid.Right.Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(color);
    }

    [Theory]
    [InlineData("{2/W}", 2, ManaColor.White)]
    [InlineData("{3/U}", 3, ManaColor.Blue)]
    public void Parse_GenericHybridMana_ParsesCorrectly(string input, int genericExpected, ManaColor colorExpected)
    {
        var result = _sut.Parse(input);

        var hybrid = result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<HybridMana>().Which;

        hybrid.Left.Should().BeOfType<GenericMana>()
            .Which.Amount.Should().Be(genericExpected);

        hybrid.Right.Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(colorExpected);
    }

    [Theory]
    [InlineData("{Q}")]
    [InlineData("{G/X}")]
    [InlineData("{/G}")]
    [InlineData("{G//U}")]
    [InlineData("{G/}")]
    public void Parse_InvalidSymbol_ThrowsFormatException(string input)
    {
        Action act = () => _sut.Parse(input);

        act.Should().Throw<FormatException>();
    }

    [Theory]
    [InlineData("{G/P}", ManaColor.Green)]
    [InlineData("{U/P}", ManaColor.Blue)]
    public void Parse_PhyrexianMana_ParsesCorrectly(string input, ManaColor expected)
    {
        var result = _sut.Parse(input);

        result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<PhyrexianMana>()
            .Which.Color.Should().Be(expected);
    }

    [Theory]
    [InlineData("{P}")]
    [InlineData("{P/B}}")]
    [InlineData("{/P}")]
    [InlineData("{2/P}")]
    [InlineData("{C/P}")]
    public void Parse_InvalidPhyrexian_Throws(string input)
    {
        Action act = () => _sut.Parse(input);

        act.Should().Throw<FormatException>();
    }


    [Theory]
    [InlineData("{B/G/P}", ManaColor.Black, ManaColor.Green)]
    [InlineData("{W/U/P}", ManaColor.White, ManaColor.Blue)]
    [InlineData("{R/G/P}", ManaColor.Red, ManaColor.Green)]
    public void Parse_HybridPhyrexianMana_ParsesCorrectly(
        string input,
        ManaColor left,
        ManaColor right)
    {
        var result = _sut.Parse(input);

        var hybrid = result.Symbols.Should().ContainSingle()
            .Which.Should().BeOfType<HybridMana>().Which;

        hybrid.Left.Should().BeOfType<PhyrexianMana>()
            .Which.Color.Should().Be(left);

        hybrid.Right.Should().BeOfType<PhyrexianMana>()
            .Which.Color.Should().Be(right);
    }

    [Theory]
    [InlineData("{B/P/G}")]
    [InlineData("{B/G/P/P}")]
    [InlineData("{B//P}")]
    [InlineData("{2/R/P}")]
    [InlineData("{C/U/P}")]
    [InlineData("{B/X/P}")]
    public void Parse_InvalidHybridPhyrexian_Throws(string input)
    {
        Action act = () => _sut.Parse(input);

        act.Should().Throw<FormatException>();
    }

    [Fact]
    public void Parse_MultipleSymbols_ParsesInOrder()
    {
        var result = _sut.Parse("{2}{G}{G/U}{X}");

        result.Symbols.Should().HaveCount(4);

        result.Symbols[0].Should().BeOfType<GenericMana>()
            .Which.Amount.Should().Be(2);

        result.Symbols[1].Should().BeOfType<ColoredMana>()
            .Which.Color.Should().Be(ManaColor.Green);

        result.Symbols[2].Should().BeOfType<HybridMana>();

        result.Symbols[3].Should().BeOfType<VariableMana>()
            .Which.Symbol.Should().Be('X');
    }
}
