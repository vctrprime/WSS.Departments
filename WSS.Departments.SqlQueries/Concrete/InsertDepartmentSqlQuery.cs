using WSS.Departments.SqlQueries.Abstract;

namespace WSS.Departments.SqlQueries.Concrete
{
    /// <summary>
    /// Вставить подразделение
    /// </summary>
    public class InsertDepartmentSqlQuery : ISqlQuery
    {
        public string Value => @"INSERT INTO dbo.department (name, parent_id)
                                 OUTPUT inserted.id Id, inserted.name Name, inserted.parent_id ParentId, inserted.row_ver RowVersion
                                 VALUES (dbo.fun_GetName(@Name, @ParentId), dbo.fun_GetParentId(@ParentId))";
    }
}