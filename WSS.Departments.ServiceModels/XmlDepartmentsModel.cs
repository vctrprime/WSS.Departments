using System.Xml.Serialization;
using WSS.Departments.Domain.Models.Xml;

namespace WSS.Departments.ServiceModels
{
    /// <summary>
    /// Класс для сериализации списка подразделений в XML
    /// </summary>
    [XmlRoot(ElementName = "Подразделения")]
    public class XmlDepartmentsModel
    {
        /// <summary>
        /// Потомки первого уровня
        /// </summary>
        [XmlElement("Подразделение")]
        public XmlDepartment[] Departments { get; set; }
        
    }
}