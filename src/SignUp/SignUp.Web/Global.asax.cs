using SignUp.Web.Logging;
using System;
using System.Web;

namespace SignUp.Web
{
    public class Global : HttpApplication
    {
        private static bool _StartFailed = false;

        void Application_Start(object sender, EventArgs e)
        {
            try
            {
                SignUp.PreloadStaticDataCache();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Exception in Application_Start");
                _StartFailed = true;
            }
        }

        void Application_BeginRequest(object sender, EventArgs e)
        {
            if (_StartFailed)
            {                
                Server.Transfer("Error.aspx");
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            Log.Error(ex, "Unhandled exception");
        }       
    }
}