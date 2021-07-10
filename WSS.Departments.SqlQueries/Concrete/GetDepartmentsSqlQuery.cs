using WSS.Departments.SqlQueries.Abstract;

namespace WSS.Departments.SqlQueries.Concrete
{
    /// <summary>
    ///     Рекурсивный запрос для получения списка подразделений
    /// </summary>
    public class GetDepartmentsSqlQuery : ISqlQuery
    {
        public string Value => @";WITH items AS (
                                SELECT id Id, name Name, parent_id ParentId, row_ver as RowVersion
                                , 0 AS Level
                                FROM dbo.department
                                WHERE parent_id IS NULL AND is_deleted = 0

                                UNION ALL

                                SELECT i.id Id, i.name Name, i.parent_id ParentId, i.row_ver as RowVersion
                                , Level + 1
                                FROM dbo.department i
                                INNER JOIN items itms ON itms.id = i.parent_id AND i.is_deleted = 0
                            )

                            SELECT Id, Name, ParentId, RowVersion FROM items ORDER BY Level";
    }
}