using WSS.Departments.SqlQueries.Abstract;

namespace WSS.Departments.SqlQueries.Concrete
{
    /// <summary>
    ///     Запрос для удаления подразделения (выставляет метку удаления)
    /// </summary>
    public class DeleteDepartmentSqlQuery : ISqlQuery
    {
        public string Value => @"UPDATE dbo.department
                                SET is_deleted = 1
                                OUTPUT inserted.id Id, inserted.name Name, inserted.parent_id ParentId, inserted.row_ver RowVersion
                                WHERE id = @Id AND row_ver = @RowVersion";
    }
}