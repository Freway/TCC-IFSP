using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using i18n;
using i18n.Helpers;

namespace Donor
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Change from the default of 'en'.
            LocalizedApplication.Current.DefaultLanguage = "pt-br";

            // Change from the default of 'i18n.langtag'.
            LocalizedApplication.Current.CookieName = "i18n_langtag";

            // Change from the of temporary redirects during URL localization
            LocalizedApplication.Current.PermanentRedirects = true;

            // This line can be used to disable URL Localization.
            //i18n.UrlLocalizer.UrlLocalizationScheme = i18n.UrlLocalizationScheme.Void;

            // Change the URL localization scheme from Scheme1.
            UrlLocalizer.UrlLocalizationScheme = UrlLocalizationScheme.Scheme2;

            // Change i18n's expectation for the ASP.NET application's virtual application root path on the server, 
            // used by Url Localization. Defaults to "/".
            //i18n.LocalizedApplication.Current.ApplicationPath = "/mysite";

            // Specifies whether the key for a message may be assumed to be the value for
            // the message in the default language. Defaults to true.
            //i18n.LocalizedApplication.Current.MessageKeyIsValueInDefaultLanguage = false;

            // Specifies a custom method called after a nugget has been translated
            // that allows the resulting message to be modified, for instance according to content type.
            // See [Issue #300](https://github.com/turquoiseowl/i18n/issues/300) for example usage case.
            LocalizedApplication.Current.TweakMessageTranslation = delegate (HttpContextBase context, Nugget nugget, LanguageTag langtag, string message)
            {
                switch (context.Response.ContentType)
                {
                    case "text/html":
                        return message.Replace("\'", "&apos;");
                }
                return message;
            };

            // Blacklist certain URLs from being 'localized' via a callback.
            UrlLocalizer.IncomingUrlFilters += delegate (Uri url) {
                if (url.LocalPath.EndsWith("sitemap.xml", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                return true;
            };

            // Extend (+=) or override (=) the default handler for Set-PAL event.
            // The default handler applies the setting to both the CurrentCulture and CurrentUICulture
            // settings of the thread, as shown below.
            LocalizedApplication.Current.SetPrincipalAppLanguageForRequestHandlers = delegate (HttpContextBase context, ILanguageTag langtag)
            {
                // Do own stuff with the language tag.
                // The default handler does the following:
                if (langtag != null)
                {
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = langtag.GetCultureInfo();
                }
            };

            // Blacklist certain URLs from being translated using a regex pattern. The default setting is:
            //i18n.LocalizedApplication.Current.UrlsToExcludeFromProcessing = new Regex(@"(?:\.(?:less|css)(?:\?|$))|(?i:i18nSkip|glimpse|trace|elmah)");

            // Whitelist content types to translate. The default setting is:
            //i18n.LocalizedApplication.Current.ContentTypesToLocalize = new Regex(@"^(?:(?:(?:text|application)/(?:plain|html|xml|javascript|x-javascript|json|x-json))(?:\s*;.*)?)$");

            // Change the types of async postback blocks that are localized
            //i18n.LocalizedApplication.Current.AsyncPostbackTypesToTranslate = "updatePanel,scriptStartupBlock,pageTitle";

            // Change which languages are parsed from the request, like skipping  the "Accept-Language"-header value. The default setting is:
            //i18n.HttpContextExtensions.GetRequestUserLanguagesImplementation = (context) => LanguageItem.ParseHttpLanguageHeader(context.Request.Headers["Accept-Language"]);

            // Override the i18n service injection. See source code for more details!
            //i18n.LocalizedApplication.Current.RootServices = new Myi18nRootServices();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
