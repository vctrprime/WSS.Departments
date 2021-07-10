using System.Threading.Tasks;
using Dapper;
using WSS.Departments.DAL.Connections.Abstract;
using WSS.Departments.DAL.Repositories.Abstract.Common;

namespace WSS.Departments.DAL.Repositories.Concrete.Common
{
    public class SelfTestRepository : BaseRepository, ISelfTestRepository
    {
        public SelfTestRepository(IConnectionCreator connectionCreator) : base(connectionCreator)
        {
        }
        
        public async Task Test()
        {
            await ConnectionCreator.Connection.ExecuteAsync("SELECT 1");
        }

        
    }
}