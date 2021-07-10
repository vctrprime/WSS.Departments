using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WSS.Departments.Web.Infrastructure.Attributes
{
    /// <summary>
    /// Проверка удалось ли обновить объект (или RowVersion не совпадает)
    /// </summary>
    public class ConcurrencySafeAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult {Value: null})
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ContentResult
                {
                    Content = Errors.ConcurrencyError
                };
            }
            
            base.OnResultExecuting(context);

        }
    }
}