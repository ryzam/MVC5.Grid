using NSubstitute;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests
{
    public class HtmlHelperFactory
    {
        public static HtmlHelper CreateHtmlHelper(String queryString = null)
        {
            ViewContext viewContext = CreateViewContext(CreateControllerContext(queryString));
            IViewDataContainer viewDataContainer = new ViewPage();
            viewDataContainer.ViewData = viewContext.ViewData;

            return new HtmlHelper(viewContext, viewDataContainer);
        }

        private static ControllerContext CreateControllerContext(String queryString)
        {
            HttpContextBase http = HttpContextFactory.CreateHttpContextBase(queryString);

            return new ControllerContext(http, http.Request.RequestContext.RouteData, Substitute.For<ControllerBase>());
        }
        private static ViewContext CreateViewContext(ControllerContext controllerContext)
        {
            ViewContext viewContext = new ViewContext(
                controllerContext,
                Substitute.For<IView>(),
                new ViewDataDictionary(),
                new TempDataDictionary(),
                new StringWriter());

            return viewContext;
        }
    }
}