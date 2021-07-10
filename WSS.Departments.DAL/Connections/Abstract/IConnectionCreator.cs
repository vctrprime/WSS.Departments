using System;
using System.Data.Common;

namespace WSS.Departments.DAL.Connections.Abstract
{
    /// <summary>
    ///     Создатель подключения к БД
    /// </summary>
    public interface IConnectionCreator : IDisposable
    {
        /// <summary>
        ///     Подключение
        /// </summary>
        DbConnection Connection { get; }
    }
}