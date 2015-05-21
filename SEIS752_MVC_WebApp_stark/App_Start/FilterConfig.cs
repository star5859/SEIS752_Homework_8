using System.Web;
using System.Web.Mvc;

namespace SEIS752_MVC_WebApp_stark
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
