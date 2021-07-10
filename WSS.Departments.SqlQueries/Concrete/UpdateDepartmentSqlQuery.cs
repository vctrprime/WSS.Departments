using WSS.Departments.SqlQueries.Abstract;

namespace WSS.Departments.SqlQueries.Concrete
{
    /// <summary>
    /// Обновить подразделение
    /// </summary>
    public class UpdateDepartmentSqlQuery : ISqlQuery
    {
        public string Value => @"UPDATE dbo.department
                                SET parent_id = @ParentId, name = @Name
                                OUTPUT inserted.id Id, inserted.name Name, inserted.parent_id ParentId, inserted.row_ver RowVersion
                                WHERE id = @Id AND row_ver = @RowVersion";
    }
}