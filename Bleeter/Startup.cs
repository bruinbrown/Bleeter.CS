using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Bleeter.Model;
using Akka.Actor;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Bleeter.Realtime;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;

namespace Bleeter
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            BleeterSystem.HydrateSystem();
            ApplicationUserManager.StartupAsync().Wait();
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(28),
                Provider = new SimpleAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            var bearerOptions = new OAuthBearerAuthenticationOptions();
            var tokenProvider = new OAuthBearerAuthenticationProvider();
            tokenProvider.OnRequestToken = ctx =>
            {
                var authHeader = ctx.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    var split = authHeader.Split(' ');
                    if (split[0] == "Bearer")
                    {
                        ctx.Token = split[1];
                    }
                }
                var queryKey = ctx.Request.Query["key"];
                if (queryKey != null)
                {
                    ctx.Token = queryKey;
                }
                return Task.FromResult(false);
            };
            bearerOptions.Provider = tokenProvider;
            app.UseOAuthBearerAuthentication(bearerOptions);

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();

            var contentOpts = new FileServerOptions();
            contentOpts.RequestPath = new PathString("/content");
            contentOpts.FileSystem = new PhysicalFileSystem("content");
            app.UseFileServer(contentOpts);

            var scriptOpts = new FileServerOptions();
            scriptOpts.RequestPath = new PathString("/scripts");
            scriptOpts.FileSystem = new PhysicalFileSystem("scripts");
            app.UseFileServer(scriptOpts);

            var fontOpts = new FileServerOptions();
            fontOpts.RequestPath = new PathString("/fonts");
            fontOpts.FileSystem = new PhysicalFileSystem("fonts");
            app.UseFileServer(fontOpts);

            app.UseFileServer();

            app.MapSignalR<CallablePersistentConnection>("/realtime");

            app.UseWebApi(config);
        }
    }
}
