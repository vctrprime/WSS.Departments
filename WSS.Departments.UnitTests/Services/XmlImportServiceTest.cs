using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WSS.Departments.Services.Converters.Concrete;
using WSS.Departments.Services.Xml.Abstract;
using WSS.Departments.Services.Xml.Concrete;
using WSS.Departments.UnitTests.ForTests.Repositories;
using Xunit;

namespace WSS.Departments.UnitTests.Services
{
    /// <summary>
    ///     Тесты сервиса для импорта
    /// </summary>
    public class XmlImportServiceTest
    {
        private readonly IXmlImportService _service;

        public XmlImportServiceTest()
        {
            _service = new XmlImportService(
                new XmlImportRepositoryForTests(),
                new FileToXElementConverter()
            );
        }

        /// <summary>
        ///     Импорт валидного xml работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsValidResult()
        {
            // Arrange
            Stream fs = File.OpenRead("Files/validXml.xml");
            // Act
            var exception = await Record.ExceptionAsync(async () => await _service.Import(fs));

            //Assert
            Assert.Null(exception);
        }

        /// <summary>
        ///     Импорт xml с пустыми атрибутами не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForEmptyAttribute()
        {
            // Arrange
            Stream fs = File.OpenRead("Files/emptyAttrbibuteXml.xml");

            // Act
            // Assert
            await Assert.ThrowsAsync<SerializationException>(async () => await _service.Import(fs));
        }

        /// <summary>
        ///     Импорт xml с отстутсвующими атрибутами не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForNotExistAttribute()
        {
            // Arrange
            Stream fs = File.OpenRead("Files/notExistAttributeXml.xml");

            // Act
            // Assert
            await Assert.ThrowsAsync<SerializationException>(async () => await _service.Import(fs));
        }

        /// <summary>
        ///     Импорт с слишком длинным значение Название не работает
        /// </summary>
        [Fact]
        public async Task ImportReturnsExceptionForLongAttribute()
        {
            // Arrange
            Stream fs = File.OpenRead("Files/longAttributeXml.xml");

            // Act
            // Assert
            await Assert.ThrowsAsync<SerializationException>(async () => await _service.Import(fs));
        }
    }
}