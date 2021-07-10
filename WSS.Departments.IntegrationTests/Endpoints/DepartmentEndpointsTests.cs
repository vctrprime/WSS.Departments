using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WSS.Departments.Domain.Models;
using WSS.Departments.Web.Infrastructure;
using Xunit;

namespace WSS.Departments.IntegrationTests.Endpoints
{
    /// <summary>
    /// Тестирование endpoints для DepartmentController
    /// </summary>
    public class DepartmentEndpointsTests
    {
        private readonly HttpClient _client;
        
        public DepartmentEndpointsTests()
        {
            Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", "Data Source=wss-departments.cphmuvw3nwzx.eu-central-1.rds.amazonaws.com;Initial Catalog=wssdb;User ID=admin;Password=wss-admin;");
            var appFactory = new WebApplicationFactory<Web.Startup>();
            _client = appFactory.CreateClient();
        }
        
        /// <summary>
        /// Нельзя добавить с пустым именем
        /// </summary>
        [Fact]
        public async Task PostReturnsBadForEmptyName()
        {
            //arrange
            var json = JsonConvert.SerializeObject(new Department
            {
                ParentId = 1,
                Name = string.Empty
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //act
            var result = await _client.PostAsync("api/departments", content);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
        
        /// <summary>
        /// Нельзя удалить корень
        /// </summary>
        [Fact]
        public async Task DeleteReturnsBadForRoot()
        {
            //arrange
            var json = JsonConvert.SerializeObject(new Department
            {
                ParentId = null,
                Name = "test"
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("http://localhost/api/departments"),
                Content = content
            };
            //act
            var result = await _client.SendAsync(request);
            var message = await result.Content.ReadAsStringAsync();
            
            //assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Equal(Errors.RootElementCannotBeDeletedError, message);
        }
    }
    
    
}