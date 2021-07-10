using WSS.Departments.SqlQueries.Concrete;

namespace WSS.Departments.SqlQueries.ForRepositories
{
    /// <summary>
    /// SQL-запросы для репозитория DepartmentRepository
    /// </summary>
    public class DepartmentRepositorySqlQueries
    {
        public GetDepartmentsSqlQuery GetDepartmentsSqlQuery { get; set; }
        
        public InsertDepartmentSqlQuery InsertDepartmentSqlQuery { get; set; }
        
        public UpdateDepartmentSqlQuery UpdateDepartmentSqlQuery { get; set; }
        
        public DeleteDepartmentSqlQuery DeleteDepartmentSqlQuery { get; set; }
    }
}