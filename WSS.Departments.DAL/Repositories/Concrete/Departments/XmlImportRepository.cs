using System.Threading.Tasks;
using Dapper;
using WSS.Departments.DAL.Connections.Abstract;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models.Xml;
using WSS.Departments.SqlQueries.ForRepositories;

namespace WSS.Departments.DAL.Repositories.Concrete.Departments
{
    public class XmlImportRepository : BaseRepository, IXmlImportRepository
    {
        private readonly XmlImportRepositorySqlQueries _sqlQueries;
        private int _insertedCount;

        public XmlImportRepository(IConnectionCreator connectionCreator, XmlImportRepositorySqlQueries sqlQueries) :
            base(connectionCreator)
        {
            _sqlQueries = sqlQueries;
        }

        public async Task<int> Save(XmlDepartment[] departments)
        {
            _insertedCount = 0;
            foreach (var department in departments) await InsertDepartment(department);

            return _insertedCount;
        }

        private async Task InsertDepartment(XmlDepartment department)
        {
            _insertedCount++;
            var id =
                await ConnectionCreator.Connection.ExecuteScalarAsync<int>(
                    _sqlQueries.InsertDepartmentSqlQuery.Value, department);

            if (department.Children is null) return;

            foreach (var child in department.Children)
            {
                child.ParentId = id;
                await InsertDepartment(child);
            }
        }
    }
}