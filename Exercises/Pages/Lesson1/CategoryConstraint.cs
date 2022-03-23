using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Exercises.Pages.Lesson1
{
    public class CategoryConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string catproc = values["cat"].ToString();
            string subcatproc = values["subcat"].ToString();
            if (catproc.StartsWith("cat"))
            {
                catproc = catproc.Replace("cat", "");
                foreach (char c in catproc)
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrWhiteSpace(subcatproc) && !(subcatproc == ""))
                {
                    if (subcatproc.StartsWith("subcat"))
                    {
                        subcatproc = subcatproc.Replace("subcat", "");
                        foreach (char c2 in subcatproc)
                        {
                            if (c2 < '0' || c2 > '9')
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    return false;
                }
                return true;
            }
            return true;
        }
    }
}
