namespace Yame.Parser;

public enum TokenType
{
    Word,
    Symbol,
    Punctuation,
    Keyword,

    Newline,
}

public record Token(TokenType Type, string Text);
