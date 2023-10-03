namespace Common.Tokens;

public enum GramaticalClass
{
    // Basic:
    None = 0,       // 0
    Identifier,     // 1
    Symbol,         // 2
    Number,         // 3
    
    // Composed:
    Keyword,        // 4
    BinarySymbol    // 5
}

public class Token
{
    private int _gramaticalClass;
    private string _value;

    public Token(GramaticalClass gramaticalClass, string value)
    {
        _gramaticalClass = (int) gramaticalClass;
        _value = value;
    }

    public int GramaticalClass
    {
        get => _gramaticalClass;
        set => _gramaticalClass = value;
    }

    public string Value
    {
        get => _value;
        set => _value = value;
    }
}