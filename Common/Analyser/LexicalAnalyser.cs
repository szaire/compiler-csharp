using Common.Special;
using Common.Tokens;

namespace Common.Analyser;

// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *  
// * O analisador léxico abaixo visa realizar a análise léxica * 
// * da linguagem C-                                           * 
// * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
public class LexicalAnalyser
{
    private readonly string? _filePath;
    
    protected readonly SpecialCharacters Special = new();
    protected readonly List<Token> TokenList = new();

    public LexicalAnalyser(string filePath)
    {
        this._filePath = filePath;
    }

    public void RunAnalyser()
    {
        if (_filePath != null)
        {
            string codeStream = File.ReadAllText(_filePath);
            if (codeStream != null)
            {
                Analyse(codeStream);
            }
            else throw new NullReferenceException("Cmin file is null");
        }
        else throw new FileNotFoundException("File not found.");
    }
    
    private bool Analyse(string codeStream)
    {
        for (int i = 0; i < codeStream.Length; i++)
        {
            bool isScapeSequence = IsScapeSequence(codeStream[i]);
            
            bool isNumber = IsDigit(codeStream[i]);
            bool isIdentifier = IsIdentifier(ref codeStream, ref i);
            // bool isSymbol = IsSymbolOrBinarySymbol(ref codeStream, ref i);
            bool isSymbol = IsSymbol(codeStream[i]);
            
            CheckIncorrectCharacters(
                codeStream[i], 
                !isScapeSequence, 
                !isNumber, 
                !isIdentifier, 
                !isSymbol);
        }
        return true;
    }

    private bool NoneClass(char character)
    {
        TokenList.Add(new Token(GramaticalClass.None, character.ToString()));
        return true;
    }
    
    private bool IsIdentifier(ref string stream, ref int currentIndex)
    {
        string localStream = stream;
        string word = "";
        bool isKeyword = false;
        
        if (Char.IsLetter(localStream[currentIndex]))
        {
            while (currentIndex < localStream.Length && 
                   (Char.IsLetterOrDigit(localStream[currentIndex]) || 
                    Char.IsNumber(localStream[currentIndex]) || 
                    localStream[currentIndex].Equals('_')))
            {
                word += localStream[currentIndex].ToString();
                if (CheckIfKeyword(word))
                {
                    TokenList.Add(new Token(GramaticalClass.Keyword, word));
                    isKeyword = true;
                    currentIndex++;
                    break;
                }
                currentIndex++;
            }
            if (!isKeyword)
            {
                TokenList.Add(new Token(GramaticalClass.Identifier, word));
            }
            currentIndex -= 1;

            return true;
        }

        return false;
    }

    private bool IsSymbolOrBinarySymbol(ref string stream, ref int currentIndex)
    {
        string localStream = stream;
        string symbol = "";

        if (IsSymbol(localStream[currentIndex]))
        {
            symbol += localStream[currentIndex].ToString();
            if (CheckIfBinarySymbol(symbol, localStream[currentIndex + 1].ToString()))
            {
                symbol += localStream[currentIndex+1].ToString();
                currentIndex++;
            }
        }

        if (symbol != "")
        {
            TokenList.Add(new Token(GramaticalClass.Symbol, symbol));
            return true;
        }
        
        return false;
    }
    
    private bool IsSymbol(char character)
    {
        switch (character)
        {
            case '(': case ')': case '[': case ']': case '{': case '}': 
            case '+': case '-': case '*': case '/':
            case '<': case '>': case '=': case '!':
            case ',': case ';':
                TokenList.Add(new Token(GramaticalClass.Symbol, character.ToString()));
                return true;
            default:
                return false;
        }
    }

    private bool IsNumber(ref string stream, ref int currentIndex)
    {
        return false;
    }
    
    private bool IsDigit(char character)
    {
        if (Char.IsNumber(character))
        {
            TokenList.Add(new Token(GramaticalClass.Number, character.ToString()));
            return true;
        }

        return false;
    }

    private bool IsScapeSequence(char character)
    {
        if (Special.IgnoredEscapeSequences.Contains(character))
            return true;
        return false;
    }

    // *=============================== UTILs ===============================*
    
    public void PrintTokens()
    {
        Console.WriteLine("================================");
        foreach (var token in TokenList)
        {
            Console.WriteLine($"[gramatical class: {token.GramaticalClass} | value: {token.Value}]");
        }
    }

    private void CheckIncorrectCharacters(char character, params bool[] booleanSequence)
    {
        bool finalCheck = true;

        foreach (bool boolInput in booleanSequence)
        {
            finalCheck = finalCheck && boolInput;
        }
        
        if (finalCheck)
        {
            NoneClass(character);
            throw new ArgumentException("Lexical Error caught.");
        }
    }

    private bool CheckIfKeyword(string word)
    {
        return Special.ReservedKeywords.Contains(word);
    }

    private bool CheckIfBinarySymbol(string symbol, string postSymbol)
    {
        if (!IsSymbol(postSymbol[0])) return false;
        
        if (symbol == "=" || symbol == "<" || symbol == ">" || symbol == "!")
        {
            switch (postSymbol)
            {
                case "=":
                    return true;
                default:
                    return false;
            }
        }
        
        if (symbol == "/")
        {
            switch (postSymbol)
            {
                case "*":
                    return true;
                default:
                    return false;
            }
        }
        
        if (symbol == "*")
        {
            switch (postSymbol)
            {
                case "/":
                    return true;
                default:
                    return false;
            }
        }
        
        return false;
    }
}