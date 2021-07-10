using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.Web.Controllers.API.Departments;
using WSS.Departments.Web.Infrastructure.Attributes;
using Xunit;

namespace WSS.Departments.UnitTests.Controllers
{
    /// <summary>
    ///     Тест методов DepartmentController
    /// </summary>
    public class DepartmentControllerTest : BaseApiControllerTest
    {
        private readonly IEnumerable<Department> _departments;
        private readonly ILogger<DepartmentController> _logger;
        private readonly Mock<IDepartmentRepository> _mock;

        public DepartmentControllerTest()
        {
            _mock = new Mock<IDepartmentRepository>();
            _logger = Mock.Of<ILogger<DepartmentController>>();
            _departments = new Department[]
            {
                new() {Id = 1, Name = "Test 1"},
                new() {Id = 2, Name = "Test 2", ParentId = 1}
            };
        }

        #region get

        /// <summary>
        ///     Get возвращает Ok
        /// </summary>
        [Fact]
        public async Task GetReturnsOkResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Get())
                .ReturnsAsync(_departments);
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Get();
            var objectResult = (result as OkObjectResult)?.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<IEnumerable<Department>>(objectResult);
            Assert.Equal(_departments.Count(),
                (objectResult as IEnumerable<Department> ?? Array.Empty<Department>()).Count());
        }

        /// <summary>
        ///     Get возвращает Bad
        /// </summary>
        [Fact]
        public async Task GetReturnsBadResult()
        {
            // Arrange
            _mock.Setup(repository => repository.Get())
                .Throws(new Exception("test"));
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Get();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region post

        /// <summary>
        ///     Post возвращает Ok
        /// </summary>
        [Fact]
        public async Task PostReturnsOkResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Insert(department))
                .ReturnsAsync(department);
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Post(department);
            var objectResult = (result as OkObjectResult)?.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<Department>(objectResult);
        }

        /// <summary>
        ///     Post возвращает Bad
        /// </summary>
        [Fact]
        public async Task PostReturnsBadResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Insert(department))
                .Throws(new Exception("test"));
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Post(department);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region put

        /// <summary>
        ///     У Put есть атрибут ConcurrencySafeAttribute
        /// </summary>
        [Fact]
        public void VerifyPutHasConcurrencySafeAttribute()
        {
            //Arrange
            var controller = new DepartmentController(_logger, _mock.Object);

            VerifyMethodHasAttribute<ConcurrencySafeAttribute>(controller, "Put");
        }

        /// <summary>
        ///     Put возвращает Ok
        /// </summary>
        [Fact]
        public async Task PutReturnsOkResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Update(department))
                .ReturnsAsync(department);
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Put(department);
            var objectResult = (result as OkObjectResult)?.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<Department>(objectResult);
        }

        /// <summary>
        ///     Put возвращает Bad
        /// </summary>
        [Fact]
        public async Task PutReturnsBadResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Update(department))
                .Throws(new Exception("test"));
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Put(department);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region delete

        /// <summary>
        ///     У delete есть атрибут ConcurrencySafeAttribute
        /// </summary>
        [Fact]
        public void VerifyDeleteHasConcurrencySafeAttribute()
        {
            //Arrange
            var controller = new DepartmentController(_logger, _mock.Object);

            VerifyMethodHasAttribute<ConcurrencySafeAttribute>(controller, "Delete");
        }

        /// <summary>
        ///     У delete есть атрибут UnremovableRootAttribute
        /// </summary>
        [Fact]
        public void VerifyDeleteHasUnremovableRootAttribute()
        {
            //Arrange
            var controller = new DepartmentController(_logger, _mock.Object);

            VerifyMethodHasAttribute<UnremovableRootAttribute>(controller, "Delete");
        }

        /// <summary>
        ///     Delete возвращает Ok
        /// </summary>
        [Fact]
        public async Task DeleteReturnsOkResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Delete(department))
                .ReturnsAsync(department);
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Delete(department);
            var objectResult = (result as OkObjectResult)?.Value;

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(objectResult);
            Assert.IsAssignableFrom<Department>(objectResult);
        }

        /// <summary>
        ///     Delete возвращает Bad
        /// </summary>
        [Fact]
        public async Task DeleteReturnsBadResult()
        {
            // Arrange
            var department = _departments.First();
            _mock.Setup(repository => repository.Delete(department))
                .Throws(new Exception("test"));
            var controller = new DepartmentController(_logger, _mock.Object);

            // Act
            var result = await controller.Delete(department);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion
    }
}