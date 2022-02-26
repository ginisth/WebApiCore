using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlatformDemo.Models;

namespace PlatformDemo.Filters
{
    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            Ticket ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                bool isValid = true;

                if (ticket.EnteredDate.HasValue == false)
                {
                    isValid = false;
                    context.ModelState.AddModelError("EnteredDate", "EnteredDate is required");
                }

                if (ticket.EnteredDate.HasValue &&
                    ticket.DueDate.HasValue &&
                    ticket.EnteredDate > ticket.DueDate)
                {
                    isValid = false;
                    context.ModelState.AddModelError("DueDate", "DueDate has to be later than  EnteredDate");
                }

                if (!isValid)
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}
