using System.Collections.Generic;
using System.Threading.Tasks;
using WSS.Departments.Domain.Models;

namespace WSS.Departments.DAL.Repositories.Abstract.Departments
{
    /// <summary>
    /// Репозиторий для работы с сущностью Department для экспорта в XML
    /// </summary>
    public interface IXmlExportRepository
    {
        /// <summary>
        /// Получить подразделения для экспорта
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Department>> Get();
    }
}