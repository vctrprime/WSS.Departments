using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using WSS.Departments.DAL.Connections.Abstract;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.Domain.Models;
using WSS.Departments.SqlQueries.ForRepositories;

namespace WSS.Departments.DAL.Repositories.Concrete.Departments
{
    public class DepartmentRepository : BaseRepository, IDepartmentRepository
    {
        private readonly DepartmentRepositorySqlQueries _sqlQueries;

        public DepartmentRepository(IConnectionCreator connectionCreator, DepartmentRepositorySqlQueries sqlQueries) :
            base(connectionCreator)
        {
            _sqlQueries = sqlQueries;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var departments =
                await ConnectionCreator.Connection.QueryAsync<Department>(_sqlQueries.GetDepartmentsSqlQuery.Value);
            return departments;
        }

        public async Task<Department> Insert(Department department)
        {
            return await QuerySingle(department, _sqlQueries.InsertDepartmentSqlQuery.Value);
        }

        public async Task<Department> Update(Department department)
        {
            return await QuerySingle(department, _sqlQueries.UpdateDepartmentSqlQuery.Value);
        }

        public async Task<Department> Delete(Department department)
        {
            return await QuerySingle(department, _sqlQueries.DeleteDepartmentSqlQuery.Value);
        }

        private async Task<Department> QuerySingle(Department department, string query)
        {
            department =
                await ConnectionCreator.Connection.QuerySingleOrDefaultAsync<Department>(
                    query, department);

            return department;
        }
    }
}