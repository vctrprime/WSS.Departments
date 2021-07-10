using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using WSS.Departments.ServiceModels;

namespace WSS.Departments.Services.Extensions
{
    /// <summary>
    ///     Расширение для сериализации/десериализации в/из XML
    /// </summary>
    public static class XElementExtensions
    {
        public static XElement ToXElement<T>(this XmlDepartmentsModel obj)
        {
            using var memoryStream = new MemoryStream();
            using TextWriter streamWriter = new StreamWriter(memoryStream);

            var xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(streamWriter, obj);

            return XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
        }

        public static T FromXElement<T>(this XElement xElement)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            return (T) xmlSerializer.Deserialize(xElement.CreateReader());
        }
    }
}