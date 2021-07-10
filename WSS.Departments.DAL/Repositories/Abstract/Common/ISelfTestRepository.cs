using System.Threading.Tasks;

namespace WSS.Departments.DAL.Repositories.Abstract.Common
{
    /// <summary>
    /// Репозиторий самотеста апи
    /// </summary>
    public interface ISelfTestRepository
    {
        /// <summary>
        /// Отправить тестовый запрос
        /// </summary>
        /// <returns></returns>
        Task Test();
    }
}