using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;

namespace Admin.MVC.Helper.Alerts
{
    public static class AlertExtensions
    {
        const string Alerts = "_Alerts";
        public static List<Alert> GetAlerts(this TempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(Alerts))
            {
                tempData[Alerts] = new List<Alert>();
            }

            return (List<Alert>)tempData[Alerts];
        }
        public static IActionResult WithSuccess(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "success", message);
        }

        public static IActionResult WithInfo(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "info", message);
        }

        public static IActionResult WithWarning(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "warning", message);
        }

        public static IActionResult WithError(this ActionResult result, string message)
        {
            return new AlertDecoratorResult(result, "danger", message);
        }
    }
}
