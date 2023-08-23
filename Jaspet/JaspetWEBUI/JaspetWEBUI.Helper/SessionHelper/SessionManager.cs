using JaspetWEBUI.Core.DTO;



namespace JaspetWEBUI.Helper.SessionHelper
{
    public class SessionManager
    {
      
        public static LoginDTO? LoggedUser 
        {
            get => AppHttpContext.Current.Session.GetObject<LoginDTO>("LoginDTO");
            set => AppHttpContext.Current.Session.SetObject("LoginDTO", value);
        }

       
    }
}
