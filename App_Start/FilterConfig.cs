using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //globalny filtr ochrony autoryzacji przed anonimowymi użytkownikami
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
