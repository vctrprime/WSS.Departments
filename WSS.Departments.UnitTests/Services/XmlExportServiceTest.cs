using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.Profiles;
using WSS.Departments.Services.Xml.Concrete;
using Xunit;

namespace WSS.Departments.UnitTests.Services
{
    /// <summary>
    ///     Тест серсива для экспорта
    /// </summary>
    public class XmlExportServiceTest
    {
        private readonly IEnumerable<Department> _departments;
        private readonly IMapper _mapper;
        private readonly Mock<IXmlExportRepository> _mock;

        public XmlExportServiceTest()
        {
            _mock = new Mock<IXmlExportRepository>();
            var config = new MapperConfiguration(opts => { opts.AddProfile<XmlDepartmentProfile>(); });
            _mapper = config.CreateMapper();

            _departments = new Department[]
            {
                new() {Id = 1, Name = "Test 1"},
                new() {Id = 2, Name = "Test 2", ParentId = 1}
            };
        }

        /// <summary>
        ///     Экспорт работает правильно
        /// </summary>
        [Fact]
        public async Task ExportReturnsValidXmlElement()
        {
            // Arrange
            _mock.Setup(repository => repository.Get())
                .ReturnsAsync(_departments);
            var service = new XmlExportService(_mock.Object, _mapper);

            // Act
            var result = await service.Export();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.DescendantsAndSelf().Count(), _departments.Count());
        }
    }
}