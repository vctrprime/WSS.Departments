using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WSS.Departments.Domain.Models;

namespace WSS.Departments.Web.Infrastructure.Attributes
{
    /// <summary>
    /// Запрещаем удаление, если пытаются удалить корневой элемент
    /// </summary>
    public class UnremovableRootAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var department = context.ActionArguments["department"] as Department;
            
            if (department is not {ParentId: null}) return;
            
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ContentResult()
            {
                Content = Errors.RootElementCannotBeDeletedError
            };
        }
    }
}