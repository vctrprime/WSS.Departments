using WSS.Departments.DAL.Config.Abstract;

namespace WSS.Departments.DAL.Config.Concrete
{
    public class DbConfig : IDbConfig
    {
        public string ConnectionString { get; }

        public DbConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}