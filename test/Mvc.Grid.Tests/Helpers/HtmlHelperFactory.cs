using NSubstitute;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Tests.Helpers
{
    public class HtmlHelperFactory
    {
        public static HtmlHelper CreateHtmlHelper()
        {
            ViewContext viewContext = CreateViewContext(CreateControllerContext());
            IViewDataContainer viewDataContainer = new ViewPage();
            viewDataContainer.ViewData = viewContext.ViewData;

            return new HtmlHelper(viewContext, viewDataContainer);
        }

        private static ControllerContext CreateControllerContext()
        {
            HttpContextBase http = HttpContextFactory.CreateHttpContextBase();

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