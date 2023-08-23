namespace Jaspet.Entity.Result;

public class ErrorInfo
{
    public string ErrorMessage { get; set; }

    public object Error { get; set; }

    public static ErrorInfo AuthenticationError(string errorMessage = "Kullanıcı Bulunamadı", object? error = null)
    {
        return new ErrorInfo { ErrorMessage = errorMessage, Error = error };
    }


    public static ErrorInfo UserInputError(string errorMessage = "Kullanıcı Adı Veya Şifre Yanlış",
        object? error = null)
    {
        return new ErrorInfo { ErrorMessage = errorMessage, Error = error };
    }

    public static ErrorInfo UserNotfound(string errorMessage = "Veri Bulunamadı", object? error = null)
    {
        return new ErrorInfo { ErrorMessage = errorMessage, Error = error };
    }
}