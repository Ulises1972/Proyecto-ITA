using Rotativa.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


using PuppeteerSharp;

namespace TutoriasWeb
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // DESCARGAR CHROMIUM AL INICIAR LA APP
            new BrowserFetcher().DownloadAsync().Wait();
        }
    }
}
