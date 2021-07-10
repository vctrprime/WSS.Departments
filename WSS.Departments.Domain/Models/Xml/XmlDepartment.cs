using System.Collections.Generic;
using System.Xml.Serialization;

namespace WSS.Departments.Domain.Models.Xml
{
    /// <summary>
    /// Подразделение для XML
    /// </summary>
    public class XmlDepartment 
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [XmlIgnore]
        public int Id { get; set; }
        
        /// <summary>
        /// Родительский идентификатор
        /// </summary>
        [XmlIgnore]
        public int? ParentId { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        [XmlAttribute("Название")]
        public string Name { get; set; }
        
        /// <summary>
        /// Дочерние подразделения
        /// </summary>
        [XmlElement("Подразделение")]
        public List<XmlDepartment> Children { get; set; }
    }
}