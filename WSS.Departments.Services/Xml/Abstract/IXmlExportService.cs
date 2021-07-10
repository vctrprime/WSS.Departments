using System.Threading.Tasks;
using System.Xml.Linq;

namespace WSS.Departments.Services.Xml.Abstract
{
    /// <summary>
    /// Экспорт подразделений в XElement
    /// </summary>
    public interface IXmlExportService
    {
        /// <summary>
        /// Экспортировать
        /// </summary>
        /// <returns></returns>
        Task<XElement> Export();
    }
}