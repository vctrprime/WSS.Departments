using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WSS.Departments.Services.Xml.Abstract;

namespace WSS.Departments.Web.Controllers.API.Departments
{
    /// <summary>
    ///     Контроллер для импорта подразделений
    /// </summary>
    [Route("api/xml")]
    public class XmlImportController : BaseApiController
    {
        private readonly IXmlImportService _xmlImportService;

        public XmlImportController(ILogger<XmlImportController> logger,
            IXmlImportService xmlImportService)
            : base(logger)
        {
            _xmlImportService = xmlImportService;
        }

        /// <summary>
        ///     Импорт
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            if (file is null) return BadRequestAction(new ArgumentNullException(), nameof(XmlImportController));

            try
            {
                var importedCount = await _xmlImportService.Import(file.OpenReadStream());
                return Ok($"Успешно импортировано {importedCount} записей!");
            }
            catch (Exception exception)
            {
                return BadRequestAction(exception, nameof(XmlImportController));
            }
        }
    }
}