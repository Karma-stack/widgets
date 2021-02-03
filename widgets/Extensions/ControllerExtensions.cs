using Microsoft.AspNetCore.Mvc;

namespace widgets.Extensions
{
    public static class ControllerExtensions
    {
        public static string UrlName<T>(this Controller controller)
            where T : Controller
        {
            var name = typeof(T).Name;
            return name.EndsWith("Controller")
                ? name.Substring(0, name.Length - 10) : name;
        }
    }
}
