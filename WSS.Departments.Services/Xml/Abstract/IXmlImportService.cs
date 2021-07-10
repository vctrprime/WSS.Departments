using System.IO;
using System.Threading.Tasks;

namespace WSS.Departments.Services.Xml.Abstract
{
    /// <summary>
    ///     Импорт подразделений из XElement
    /// </summary>
    public interface IXmlImportService
    {
        /// <summary>
        ///     Импорт
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>Количество импортированных записей</returns>
        Task<int> Import(Stream stream);
    }
}