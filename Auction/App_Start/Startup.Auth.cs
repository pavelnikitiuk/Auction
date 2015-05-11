using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;

namespace Auction
{
    
    public partial class Startup
    {
        private FacebookAuthenticationOptions FacebookOptions()
        {
            var options = new FacebookAuthenticationOptions();
            options.Scope.Add("email");
            options.AppId = "790189634429861";
            options.AppSecret = "d822faffb9450470ee9182aa6ebe4d9d";
            options.Provider = new FacebookAuthenticationProvider()
            {
                OnAuthenticated = async context =>
                {
                    context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken));
                    foreach (var claim in context.User)
                    {
                        var claimType = string.Format("urn:facebook:{0}", claim.Key);
                        string claimValue = claim.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));
                    }

                }
            };
            options.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            return options;
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

           

            

            app.UseFacebookAuthentication(FacebookOptions());

            //app.UseFacebookAuthentication(
            //   appId: "790189634429861",
            //   appSecret: "d822faffb9450470ee9182aa6ebe4d9d");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "244269921814-hgbqgj179nfk8389153etgenvj1ljlfl.apps.googleusercontent.com",
                ClientSecret = "AzvDlSPutn9e0iKacEk6zkLg"
            });
        }
    }
}