using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace PlatformDemo.Filters
{
    public class Version1DiscontinueResourceFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
            {
                context.Result = new BadRequestObjectResult(
                    new
                    {
                        Versioning = new[] { "This version of API has expired, please use the latest one" }
                    });
            }
        }
    }
}
