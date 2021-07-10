using System.Threading.Tasks;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models.Xml;

namespace WSS.Departments.UnitTests.ForTests.Repositories
{
    /// <summary>
    ///     Репозиторий IXmlImportRepository для тестов
    /// </summary>
    public class XmlImportRepositoryForTests : IXmlImportRepository
    {
        public async Task<int> Save(XmlDepartment[] departments)
        {
            return departments.Length;
        }
    }
}