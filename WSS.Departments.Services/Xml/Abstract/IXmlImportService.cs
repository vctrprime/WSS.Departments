using System.Threading.Tasks;
using System.Xml.Linq;

namespace WSS.Departments.Services.Xml.Abstract
{
    /// <summary>
    /// Импорт подразделений из XElement
    /// </summary>
    public interface IXmlImportService
    {
        /// <summary>
        /// Импорт
        /// </summary>
        /// <param name="xml"></param>
        /// <returns>Количество импортированных записей</returns>
        Task<int> Import(XElement xml);
    }
}