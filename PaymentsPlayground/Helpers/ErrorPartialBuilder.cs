using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using PaymentsPlayground.Models.ViewModels;

namespace PaymentsPlayground.Helpers
{
    public static class ErrorPartialBuilder
    {
        private const string PageName = "_ErrorPartial";
        public static PartialViewResult Build(List<ModelError> modelErrors, ViewDataDictionary dict)
        {
            return new PartialViewResult
            {
                ViewName = PageName,
                ViewData = new ViewDataDictionary<List<ModelError>>(dict, modelErrors)
            };
        }
    }
}
