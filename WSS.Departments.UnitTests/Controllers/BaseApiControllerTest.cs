using System;
using System.Linq;
using WSS.Departments.Web.Controllers.API;
using Xunit;

namespace WSS.Departments.UnitTests.Controllers
{
    /// <summary>
    ///     Базовый класс для теста контроллеров
    /// </summary>
    public class BaseApiControllerTest
    {
        protected void VerifyMethodHasAttribute<T>(BaseApiController controller, string action) where T : Attribute
        {
            //Act
            var type = controller.GetType();
            var methodInfo = type.GetMethod(action);
            var attributes = methodInfo?.GetCustomAttributes(typeof(T), true);

            //Arrange
            Assert.True(attributes?.Any());
        }
    }
}