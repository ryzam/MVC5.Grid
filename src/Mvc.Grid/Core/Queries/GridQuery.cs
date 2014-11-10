using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace NonFactors.Mvc.Grid
{
    public class GridQuery : NameValueCollection
    {
        public GridQuery() : base()
        {
        }
        public GridQuery(NameValueCollection query) : base(query)
        {
        }

        public override String ToString()
        {
            List<String> query = new List<String>();
            foreach (String key in AllKeys)
                foreach (String value in GetValues(key))
                    query.Add(HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(value));

            return "?" + String.Join("&", query);
        }
    }
}
