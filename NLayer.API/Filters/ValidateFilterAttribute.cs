using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors=context.ModelState.Values.SelectMany(x => x.Errors)
                    .Select(a=>a.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDTO<NoContentDTO>
                    .Fail(400, errors));

            }
        }
    }
}
