using WSS.Departments.DAL.Connections.Abstract;

namespace WSS.Departments.DAL.Repositories.Concrete
{
    /// <summary>
    ///     Базовый класс для репозиториев
    /// </summary>
    public class BaseRepository
    {
        protected readonly IConnectionCreator ConnectionCreator;

        protected BaseRepository(IConnectionCreator connectionCreator)
        {
            ConnectionCreator = connectionCreator;
        }
    }
}