using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Castle.Core.Internal;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.Domain.Models.Xml;
using WSS.Departments.Services.Xml.Concrete;
using WSS.Departments.UnitTests.ForTests.Repositories;
using Xunit;

namespace WSS.Departments.UnitTests.Services
{
    /// <summary>
    /// Тесты сервиса для импорта
    /// </summary>
    public class XmlImportServiceTest
    {
        private readonly IXmlImportRepository _repositoryForTest;
        
        public XmlImportServiceTest()
        {
            _repositoryForTest = new XmlImportRepositoryForTests();
        }
        
        /// <summary>
        /// Импорт валидного xml работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsValidResult()
        {
            // Arrange
            XmlDepartment[] departments =
            {
                new() { Name = "Департамент 1" },
                new() { Name = "Департамент 2" }
            };
            var validXElement = new XElement("Подразделения", 
                new XElement("Подразделение", new XAttribute("Название", departments[0].Name)),
                new XElement("Подразделение", new XAttribute("Название", departments[1].Name)));
            
            var service = new XmlImportService(_repositoryForTest);

            // Act
            var result = await service.Import(validXElement);
            
            //Assert
            Assert.Equal(result, departments.Length);
        }
        
        /// <summary>
        /// Импорт xml с пустыми атрибутами не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForEmptyAttribute()
        {
            // Arrange
            XmlDepartment[] departments =
            {
                new() { Name = "" },
                new() { Name = "Департамент 2" }
            };
            var invalidXElement = new XElement("Подразделения", 
                new XElement("Подразделение", new XAttribute("Название", departments[0].Name)),
                new XElement("Подразделение", new XAttribute("Название", departments[1].Name)));
            
            var service = new XmlImportService(_repositoryForTest);

            // Act
            // Assert
            await Assert.ThrowsAsync<SerializationException>(async () => await service.Import(invalidXElement));
        }
        
        /// <summary>
        /// Импорт xml с отстутсвующими атрибутами не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForNotExistAttribute()
        {
            // Arrange
            XmlDepartment[] departments =
            {
                new() { Name = "" },
                new() { Name = "Департамент 2" }
            };
            var invalidXElement = new XElement("Подразделения", 
                new XElement("Подразделение"),
                new XElement("Подразделение", new XAttribute("Название", departments[1].Name)));
            
            var service = new XmlImportService(_repositoryForTest);

            // Act
            // Assert
            await Assert.ThrowsAsync<SerializationException>(async () => await service.Import(invalidXElement));
        }
        
        /// <summary>
        /// Импорт с слишком длинным значение Название не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForLongAttribute()
        {
            
            // Arrange
            XmlDepartment[] departments =
            {
                new() { Name = GetLongName() },
                new() { Name = "Департамент 2" }
            };
            var invalidXElement = new XElement("Подразделения", 
                new XElement("Подразделение", new XAttribute("Название", departments[0].Name)),
                new XElement("Подразделение", new XAttribute("Название", departments[1].Name)));
            
            var service = new XmlImportService(_repositoryForTest);

            // Act
            await Assert.ThrowsAsync<SerializationException>(async () => await service.Import(invalidXElement));
        }


        private string GetLongName()
        {
            var maxLength = typeof(Department).GetProperty("Name").GetAttributes<MaxLengthAttribute>().First().Length;
            StringBuilder sb = new StringBuilder("t");
            while (sb.Length <= maxLength)
            {
                sb.Append("t");
            }

            return sb.ToString();
        }
    }
}