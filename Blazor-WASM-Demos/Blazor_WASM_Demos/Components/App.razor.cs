using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
namespace BlazorDemos
{
    public class AppBase : ComponentBase
    {
        [CascadingParameter]
        public HttpContext? HttpContext { get; set; }

        protected override void OnInitialized()
        {
            HttpContext?.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(
                        CultureInfo.CurrentCulture,
                        CultureInfo.CurrentUICulture)));
        }
    }
}