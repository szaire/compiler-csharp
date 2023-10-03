namespace Common.Special;

public record SpecialCharacters()
{
    private readonly char[] _ignoredEscapeSequences = { '\r', '\n', '\t', ' '};
    private readonly string[] _reservedKeywords = new[] { "else", "if", "int", "return", "void", "while" };
    
    public char[] IgnoredEscapeSequences => _ignoredEscapeSequences;

    public string[] ReservedKeywords => _reservedKeywords;
}