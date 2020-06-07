using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Project.Startup1))]

namespace Project
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app) 
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                
                ExpireTimeSpan = TimeSpan.FromMinutes(30),
               
                LoginPath = new PathString(@"/Account/Login"),
                
            });


            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
