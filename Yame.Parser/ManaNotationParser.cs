using System.Text.RegularExpressions;
using Yame.Core.Mana;
using Yame.Core.Mana.Symbols;

namespace Yame.Parser;

public partial class ManaNotationParser
{
    [GeneratedRegex(@"\{([^}]+)\}", RegexOptions.Compiled)]
    private static partial Regex TokenRegex();

    public ManaCost Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new ManaCost();

        var symbols = TokenRegex().Matches(input)
            .Select(m => m.Groups[1].Value)
            .Select(ParseSymbol)
            .ToList();

        return new ManaCost(symbols);
    }

    private static bool IsValidHybridComponent(ManaSymbol symbol) =>
        symbol is ColoredMana or GenericMana or PhyrexianMana;

    private static ManaSymbol ParseSymbol(string token)
    {
        var parts = token.Split('/');

        if (parts.Length == 1)
            return ParseSingle(parts[0]);

        // Phyrexian mana
        if (parts[^1] == "P")
        {
            var colors = parts[..^1].Select(p => new PhyrexianMana(ManaColorHelper.Parse(p))).ToList();

            return colors.Count switch
            {
                1 => colors[0],
                2 => new HybridMana(colors[0], colors[1]),
                _ => throw new FormatException($"Invalid phyrexian mana symbol: {{{token}}}"),
            };
        }

        // Hybrid mana
        if (parts.Length == 2)
        {
            var left = ParseSingle(parts[0]);
            var right = ParseSingle(parts[1]);

            if (!IsValidHybridComponent(left) || !IsValidHybridComponent(right))
                throw new FormatException($"Invalid hybrid mana symbol: {{{token}}}");

            return new HybridMana(left, right);
        }

        throw new FormatException($"Invalid hybrid mana symbol: {{{token}}}");
    }

    private static ManaSymbol ParseSingle(string token)
    {
        if (int.TryParse(token, out int value))
            return new GenericMana(value);

        if (token.Length == 1 && "XYZ".Contains(token))
            return new VariableMana(token[0]);

        // TODO: Snow
        if (token == "S")
            throw new NotImplementedException();

        // TODO: Legendary
        if (token == "L")
            throw new NotImplementedException();

        if (token.Length == 1)
            return new ColoredMana(ManaColorHelper.Parse(token));

        throw new FormatException($"Unknown mana symbol: {token}");
    }
}