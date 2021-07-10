using System.Collections.Generic;
using System.Threading.Tasks;
using WSS.Departments.Domain.Models;

namespace WSS.Departments.DAL.Repositories.Abstract.Departments
{
    /// <summary>
    /// Репозиторий для работы с сущностью Department
    /// </summary>
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Получить поздразделения
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Department>> Get();

        /// <summary>
        /// Вставить подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task<Department> Insert(Department department);

        /// <summary>
        /// Обновить подразделение
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task<Department> Update(Department department);

        /// <summary>
        /// Удалить подразделение(проставить метку удаления)
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        Task<Department> Delete(Department department);
    }
}