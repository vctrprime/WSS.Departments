namespace WSS.Departments.DAL.Config.Abstract
{
    /// <summary>
    /// Конфигурация для коннекта к БД
    /// </summary>
    public interface IDbConfig
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        string ConnectionString { get; }
    }
}