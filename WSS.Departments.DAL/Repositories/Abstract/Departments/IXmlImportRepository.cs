using System.Threading.Tasks;
using WSS.Departments.Domain.Models.Xml;

namespace WSS.Departments.DAL.Repositories.Abstract.Departments
{
    /// <summary>
    /// Репозиторий для работы с сущностью XmlDepartment для импорта XML
    /// </summary>
    public interface IXmlImportRepository
    {
        /// <summary>
        /// Сохранить древовидный перечень подразделений
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        Task<int> Save(XmlDepartment[] departments);
    }
}