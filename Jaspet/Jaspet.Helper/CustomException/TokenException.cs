namespace Jaspet.Helper.CustomException;

public class TokenException : Exception
{
    public TokenException(string message = "Token Hatası Oluştu") : base(message)
    {
    }
}