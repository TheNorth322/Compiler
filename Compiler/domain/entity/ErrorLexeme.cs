namespace Compiler.domain.entity;

public class ErrorLexeme
{
    public ErrorLexeme(Lexeme lexeme, string message)
    {
        Lexeme = lexeme;
        Message = message;
    }

    public Lexeme Lexeme { get; }
    public string Message { get; }
    
    
}