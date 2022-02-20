using PlatformDemo.Models;
using System.ComponentModel.DataAnnotations;

namespace PlatformDemo.ModelValidations
{
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = (Ticket)validationContext.ObjectInstance;

            if (ticket != null && !string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (!ticket.DueDate.HasValue)
                    return new ValidationResult("Due Date is required when th ticket has an owner");
            }

            return ValidationResult.Success;
        }
    }
}
