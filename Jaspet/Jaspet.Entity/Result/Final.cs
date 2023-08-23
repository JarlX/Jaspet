namespace Jaspet.Entity.Result;

using System.Net;

public class Final<T>
{
    private Final(T data, string message, int statusCode)
    {
        Data = data;
        StatusCode = statusCode;
        Message = message;
    }

    private Final(string message, int statusCode, ErrorInfo authenticationError)
    {
        Message = message;
        StatusCode = statusCode;
        ErrorInfo = authenticationError;
    }

    private Final(string message, int statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }

    public T Data { get; set; }

    public string Message { get; set; }

    public int StatusCode { get; set; }

    public ErrorInfo ErrorInfo { get; set; }


    public static Final<T> OkWithData(T data, string message = "İşlem Başarılı",
        int statusCode = (int)HttpStatusCode.OK)
    {
        return new Final<T>(data, message, statusCode);
    }

    public static Final<T> AuthenticationError(string message = "Kullanıcı Bulunamadı",
        int statusCode = (int)HttpStatusCode.Unauthorized)
    {
        return new Final<T>(message, statusCode, ErrorInfo.AuthenticationError());
    }

    public static Final<T> UserInputError(string message = "Kullanıcı Adı Veya Şifre Yanlış",
        int statusCode = (int)HttpStatusCode.NotFound)
    {
        return new Final<T>(message, statusCode, ErrorInfo.UserInputError());
    }

    public static Final<T> NotFound(string message = "Veri Bulunamadı", int statusCode = (int)HttpStatusCode.NotFound)
    {
        return new Final<T>(message, statusCode, ErrorInfo.UserNotfound());
    }

    public static Final<T> OkWithOutData(string message = "İşlem Başarılı", int statusCode = (int)HttpStatusCode.OK)
    {
        return new Final<T>(message, statusCode);
    }
}