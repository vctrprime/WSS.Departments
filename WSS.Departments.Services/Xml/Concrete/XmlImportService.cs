using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Castle.Core.Internal;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.ServiceModels;
using WSS.Departments.Services.Extensions;
using WSS.Departments.Services.Xml.Abstract;

namespace WSS.Departments.Services.Xml.Concrete
{
    public class XmlImportService : IXmlImportService
    {
        private readonly IXmlImportRepository _repository;
        
        public XmlImportService(IXmlImportRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<int> Import(XElement xml)
        {
            if (!NamesAttributesIsValid(xml, out var maxLengthAttributeValue))
            {
                throw new SerializationException($"Attribute Название is required for all elements and its length should be no more than {maxLengthAttributeValue} characters...");
            }

            var xmlModel = xml.FromXElement<XmlDepartmentsModel>();

            if (xmlModel is null) throw new SerializationException("Incorrect data");
            
            int importedCount = await _repository.Save(xmlModel.Departments);
            return importedCount;
        }

        /// <summary>
        /// Проверяем все ли атрибуты валидны
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="maxLengthAttributeValue"></param>
        /// <returns></returns>
        private bool NamesAttributesIsValid(XElement xml, out int maxLengthAttributeValue)
        {
            var departments = xml.DescendantsAndSelf().Elements("Подразделение").ToArray();
            var namesAttributes = xml.DescendantsAndSelf().Attributes("Название").ToArray();

            maxLengthAttributeValue = typeof(Department).GetProperty("Name").GetAttributes<MaxLengthAttribute>().First().Length;
            int maxLengthName = maxLengthAttributeValue;
             
            return !(departments.Length != namesAttributes.Length ||
                     namesAttributes.Any(a => string.IsNullOrEmpty(a.Value) || a.Value.Length > maxLengthName));
        }
    }
}