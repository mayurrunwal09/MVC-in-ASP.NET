using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc;

namespace Model_Popup_Using_StoredProcedure
{
    public class Helper
    {
        public static string viewtostring(Controller controller, string viewname, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as IViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewname, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions());
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
