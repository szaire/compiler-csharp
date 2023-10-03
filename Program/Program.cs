using Common;
using Common.Analyser;

// done: fazer a cópia do código em python em C#
// TODO: Aplicar as classes "None" e "BinarySymbol"
// TODO: Aplicar as classes "None" e "BinarySymbol"
// TODO: Aplicar as classes "None" e "BinarySymbol"
// TODO: Aplicar as classes "None" e "BinarySymbol"
// TODO: Aplicar as classes "None" e "BinarySymbol"

namespace LexicAnalyser;
class Program
{
    public static void Main(string[] args)
    {
        try
        {
            /*
            string? fileName = Console.ReadLine();
            
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string soluctionDir = Path.GetFullPath(Path.Combine(baseDir, @"..\.."));
            string filePath = Path.Combine(soluctionDir, fileName);
            */
            LexicalAnalyser la = new("/home/samueo/Documents/UNIFOR/CC6/ASPECTOS TEORICOS/ATIVIDADE 2/AnalisadorLexico/InputFiles/input2.cmin");
            la.RunAnalyser();
            la.PrintTokens();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nMessage ---\n{0}", ex.Message );
            Console.WriteLine("\nHelpLink ---\n{0}", ex.HelpLink );
            Console.WriteLine("\nSource ---\n{0}", ex.Source );
            Console.WriteLine("\nStackTrace ---\n{0}", ex.StackTrace );
            Console.WriteLine("\nTargetSite ---\n{0}", ex.TargetSite );
        }
    }
}