using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using WSS.Departments.DAL.Connections.Abstract;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.SqlQueries.ForRepositories;

namespace WSS.Departments.DAL.Repositories.Concrete.Departments
{
    public class XmlExportRepository : BaseRepository, IXmlExportRepository
    {
        private readonly XmlExportRepositorySqlQueries _sqlQueries;
        
        public XmlExportRepository(IConnectionCreator connectionCreator, XmlExportRepositorySqlQueries sqlQueries) : base(connectionCreator)
        {
            _sqlQueries = sqlQueries;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var departments = await ConnectionCreator.Connection.QueryAsync<Department>(_sqlQueries.GetDepartmentsSqlQuery.Value);
            return departments;
        }
    }
}