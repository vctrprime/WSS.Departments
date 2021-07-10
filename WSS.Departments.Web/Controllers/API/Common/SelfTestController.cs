using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WSS.Departments.DAL.Repositories.Abstract.Common;

namespace WSS.Departments.Web.Controllers.API.Common
{
    /// <summary>
    /// Самотест апи
    /// </summary>
    [Route("api/[controller]")]
    public class SelfTestController : BaseApiController
    {
        private readonly ISelfTestRepository _repository;
        
        public SelfTestController(ILogger<SelfTestController> logger, ISelfTestRepository repository) : base(logger)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Тест
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _repository.Test();
                return Ok("Test successful!");
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(SelfTestController));
            }
        }
    }
}