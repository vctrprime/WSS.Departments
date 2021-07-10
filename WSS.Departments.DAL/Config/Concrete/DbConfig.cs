using WSS.Departments.DAL.Config.Abstract;

namespace WSS.Departments.DAL.Config.Concrete
{
    public class DbConfig : IDbConfig
    {
        public DbConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}