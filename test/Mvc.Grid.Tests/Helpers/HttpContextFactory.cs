using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NonFactors.Mvc.Grid.Tests
{
    public class HttpContextFactory
    {
        public static HttpContext CreateHttpContext(String queryString = null)
        {
            HttpRequest request = new HttpRequest(String.Empty, "http://localhost:4601/", queryString);
            HttpResponse response = new HttpResponse(new StringWriter());
            HttpContext context = new HttpContext(request, response);

            RouteValueDictionary routeValues = request.RequestContext.RouteData.Values;
            routeValues["controller"] = "home";
            routeValues["action"] = "index";
            RouteTable.Routes.Clear();
            RouteTable.Routes.MapRoute(
                "Default",
                "{controller}/{action}");

            return context;
        }
        public static HttpContextBase CreateHttpContextBase(String queryString = null)
        {
            return new HttpContextWrapper(CreateHttpContext(queryString));
        }
    }
}
