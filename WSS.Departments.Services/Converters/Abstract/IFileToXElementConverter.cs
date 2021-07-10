using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WSS.Departments.Services.Converters.Abstract
{
    /// <summary>
    /// Конвертер из файла в XElement
    /// </summary>
    public interface IFileToXElementConverter
    {
        /// <summary>
        /// Конвертировать
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Task<XElement> Convert(Stream stream);
    }
}