using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace NonFactors.Mvc.Grid
{
    public class GridPager<TModel> : IGridPager<TModel> where TModel : class
    {
        public RequestContext RequestContext { get; private set; }
        public String PartialViewName { get; set; }

        public GridProcessorType Type { get; set; }
        public Int32 PagesToDisplay { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 RowsPerPage { get; set; }
        public Int32 TotalRows { get; set; }
        public Int32 StartingPage
        {
            get
            {
                Int32 middlePage = (PagesToDisplay / 2) + (PagesToDisplay % 2) - 1;
                if (CurrentPage - middlePage + PagesToDisplay > TotalPages)
                    return TotalPages - PagesToDisplay;

                if (CurrentPage < middlePage)
                    return 0;

                return CurrentPage - middlePage;
            }
        }
        public Int32 TotalPages
        {
            get
            {
                return (Int32)(Math.Ceiling(TotalRows / (Double)RowsPerPage));
            }
        }

        public GridPager(RequestContext requestContext, IEnumerable<TModel> source)
        {
            PartialViewName = "MvcGrid/_Pager";
            RequestContext = requestContext;
            Int32 currentPage = 0;

            Int32.TryParse(RequestContext.HttpContext.Request.QueryString["MG-Page"], out currentPage);
            Type = GridProcessorType.Post;
            TotalRows = source.Count();
            CurrentPage = currentPage;
            PagesToDisplay = 5;
            RowsPerPage = 20;
        }

        public IEnumerable<TModel> Process(IEnumerable<TModel> items)
        {
            return items.Skip(CurrentPage * RowsPerPage).Take(RowsPerPage);
        }

        public String LinkForPage(Int32 page)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(RequestContext.RouteData.Values);
            NameValueCollection query = RequestContext.HttpContext.Request.QueryString;
            UrlHelper urlHelper = new UrlHelper(RequestContext);

            foreach (String parameter in query)
                routeValues[parameter] = query[parameter];

            routeValues["MG-Page"] = page;

            return urlHelper.Action(routeValues["action"] as String, routeValues);
        }
    }
}
