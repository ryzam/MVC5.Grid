using NSubstitute;
using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NonFactors.Mvc.Grid.Tests.Helpers
{
    public class HttpContextFactory
    {
        public static HttpContext CreateHttpContext(String queryString = null)
        {
            HttpRequest request = new HttpRequest(String.Empty, "http://localhost:19175/domain/", queryString);
            Hashtable browserCapabilities = new Hashtable { { "cookies", "true" } };
            HttpBrowserCapabilities browser = new HttpBrowserCapabilities();
            HttpResponse response = new HttpResponse(new StringWriter());
            HttpContext httpContext = new HttpContext(request, response);
            browser.Capabilities = browserCapabilities;
            request.Browser = browser;

            RouteValueDictionary routeValues = request.RequestContext.RouteData.Values;
            routeValues["controller"] = "home";
            routeValues["action"] = "index";
            MapRoutes();

            return httpContext;
        }
        public static HttpContextBase CreateHttpContextBase()
        {
            HttpContext httpContext = CreateHttpContext();

            HttpRequestBase httpRequestBase = Substitute.ForPartsOf<HttpRequestWrapper>(httpContext.Request);
            httpRequestBase.ApplicationPath.Returns("/domain");

            HttpContextBase httpContextBase = Substitute.ForPartsOf<HttpContextWrapper>(httpContext);
            httpContextBase.Response.Returns(new HttpResponseWrapper(httpContext.Response));
            httpContextBase.Server.Returns(Substitute.For<HttpServerUtilityBase>());
            httpContextBase.Request.Returns(httpRequestBase);

            return httpContextBase;
        }

        private static void MapRoutes()
        {
            RouteTable.Routes.Clear();
            RouteTable.Routes
                .MapRoute(
                    "Default",
                    "{controller}/{action}");
        }
    }
}
