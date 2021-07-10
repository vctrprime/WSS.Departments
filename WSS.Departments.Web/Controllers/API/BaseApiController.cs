using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WSS.Departments.Web.Controllers.API
{
    /// <summary>
    /// Базовый апи контроллер
    /// </summary>
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private readonly ILogger _logger;

        /// <inheritdoc />
        public BaseApiController(ILogger logger)
        {
            _logger = logger;
        }
        
        protected IActionResult BadRequestAction(Exception exception, string controllerName)
        {
            _logger.LogError(exception, controllerName);
            return BadRequest(exception.Message);
        }
    }
}