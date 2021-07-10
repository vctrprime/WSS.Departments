using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WSS.Departments.DAL.Config.Abstract;
using WSS.Departments.DAL.Connections.Abstract;

namespace WSS.Departments.DAL.Connections.Concrete
{
    /// <summary>
    /// Подключение к MS SQL
    /// </summary>
    public class SqliteConnectionCreator : IConnectionCreator
    {
        private readonly IDbConfig _dbConfig;
        private DbConnection _connection;

        public SqliteConnectionCreator(IDbConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        
        public DbConnection Connection
        {
            get
            {
                _connection ??= CreateConnection();

                return _connection;
            }
        }
        
        /// <summary>
        /// Создать подключение к MS SQL
        /// </summary>
        /// <returns></returns>
        private DbConnection CreateConnection()
        {
            _connection = new SqlConnection(_dbConfig.ConnectionString);

            _connection.Open();

            return _connection;
        }
        
        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection.Dispose();

                _connection = null;
            }
        }
    }
}