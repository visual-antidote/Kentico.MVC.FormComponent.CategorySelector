using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace VisualAntidote.Kentico.MVC.FormComponent.CategorySelector.Helpers
{
    public static class RouteHelper
    {
        public static void ApplyCategorySelectorRoute(RouteCollection routes)
        {
            routes.MapRoute(
                name: "VisualAntidoteCategorySelectModalDialogRoute",
                url: "VisualAntidoteCategorySelectModalDialog",
                defaults: new { controller = "VisualAntidoteCategorySelectModalDialog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
