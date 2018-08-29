using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Web.Infrastructure.Filters
{
    public class ApiError
    {
        public string Message { get; set; }
        public bool IsError { get; set; }
        public string Detail { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }

        public ApiError(string message)
        {
            Message = message;
            IsError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            IsError = true;
            if (modelState == null || !modelState.Any(m => m.Value.Errors.Count > 0))
                return;

            Message = "Please correct the specified errors and try again.";
            Errors = modelState.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Errors.Select(x => x.ErrorMessage).ToArray());
        }
    }
}
