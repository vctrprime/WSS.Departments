using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using WSS.Departments.Services.Converters.Abstract;

namespace WSS.Departments.Services.Converters.Concrete
{
    public class FileToXElementConverter : IFileToXElementConverter
    {
        public async Task<XElement> Convert(Stream stream)
        {
            string fileContents;
            using (var reader = new StreamReader(stream))
            {
                fileContents = await reader.ReadToEndAsync();
            }
            XDocument xml = XDocument.Parse(fileContents);

            return xml.Root;
        }
    }
}