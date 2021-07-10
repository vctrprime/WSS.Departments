using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.Web.Infrastructure.Attributes;

namespace WSS.Departments.Web.Controllers.API.Departments
{
    /// <summary>
    /// Контроллер для работы с подразделениями
    /// </summary>
    public class DepartmentController : BaseApiController
    {
        private readonly IDepartmentRepository _repository;
        
        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentRepository repository)
            : base(logger)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// Получить подразделения
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                IEnumerable<Department> departments = await _repository.Get();
                return Ok(departments);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(DepartmentController));
            }
        }
        
        /// <summary>
        /// Создать подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Department department)
        {
            try
            {
                department = await _repository.Insert(department);
                return Ok(department);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(DepartmentController));
            }
        }
        
        /// <summary>
        /// Обновить подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [ConcurrencySafe]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Department department)
        {
            try
            {
                department = await _repository.Update(department);
                return Ok(department);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(DepartmentController));
            }
        }
        
        /// <summary>
        /// Удалить подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [UnremovableRoot]
        [ConcurrencySafe]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Department department)
        {
            try
            {
                department = await _repository.Delete(department);
                return Ok(department);
            }
            catch(Exception exception)
            {
                return BadRequestAction(exception, nameof(DepartmentController));
            }
        }
    }
}