using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Mekatronik.MyExtensions
{
    public static class StringExtensions
    {
        public static string ToDMY(this DateTime dtm)
        {

            return dtm.ToString("dd-MMM-yyyy HH:mm");
        }
        public static string GetDomain(HttpContext httpContext)
        {
            
            return "http://" + httpContext.Request.Host.Value;
        }
        public static string GetRequestApiStr(this HttpContext httpContext,string endpoint)
        {
            return "http://" + httpContext.Request.Host.Value + endpoint;
        }
        public static string ToFormatDateHourAndMinute(this DateTime dtm)
        {

            return dtm.ToString("HH:mm");
        }
        public static string HtmlDe(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }
        public static string HtmlEn(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }
    
        public static string GetPageLink(this ActionContext actionContext,int pg)
        {

            string req = actionContext.HttpContext.Request.GetRawUrl();
            req = req.TrimEnd('/');
            if (req.Contains("?pg="))
            {
                req = req.Split('?')[0];
            }
            return req + "?pg=" + pg.ToString();
        }
        public static string GetRawUrl(this HttpRequest request)
        {
            var httpContext = request.HttpContext;

            var requestFeature = httpContext.Features.Get<IHttpRequestFeature?>();

            return requestFeature.RawTarget;
        }
       

   

    
    }
}
