using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WSS.Departments.Services.Xml.Abstract;

namespace WSS.Departments.Web.Controllers.API.Departments
{
    /// <summary>
    ///     Контроллер для экспорта подразделений
    /// </summary>
    [Route("api/xml")]
    public class XmlExportController : BaseApiController
    {
        private readonly IXmlExportService _xmlExportService;

        public XmlExportController(ILogger<XmlExportController> logger,
            IXmlExportService xmlExportService)
            : base(logger)
        {
            _xmlExportService = xmlExportService;
        }

        /// <summary>
        ///     Экспорт
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("export")]
        public async Task<IActionResult> Get(string fileName = "departments")
        {
            try
            {
                var xml = await _xmlExportService.Export();
                return File(Encoding.UTF8.GetBytes(xml.ToString()), "text/xml", $"{fileName}.xml");
            }
            catch (Exception exception)
            {
                return BadRequestAction(exception, nameof(XmlExportController));
            }
        }
    }
}