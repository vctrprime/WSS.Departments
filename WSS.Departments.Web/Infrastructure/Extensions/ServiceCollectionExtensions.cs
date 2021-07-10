using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using WSS.Departments.DAL.Config.Abstract;
using WSS.Departments.DAL.Config.Concrete;
using WSS.Departments.DAL.Connections.Abstract;
using WSS.Departments.DAL.Connections.Concrete;
using WSS.Departments.DAL.Repositories.Abstract.Common;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.DAL.Repositories.Concrete.Common;
using WSS.Departments.DAL.Repositories.Concrete.Departments;
using WSS.Departments.Profiles;
using WSS.Departments.Services.Converters.Abstract;
using WSS.Departments.Services.Converters.Concrete;
using WSS.Departments.Services.Xml.Abstract;
using WSS.Departments.Services.Xml.Concrete;
using WSS.Departments.SqlQueries.Abstract;
using WSS.Departments.SqlQueries.ForRepositories;

namespace WSS.Departments.Web.Infrastructure.Extensions
{
    /// <summary>
    /// Расширение для внедрения различных зависимостей
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModules(this IServiceCollection services)
        {
            services
                .RegisterServices()
                .RegisterRepositories()
                .RegisterSqlQueries()
                .RegisterMapperProfiles();
        }
        
        /// <summary>
        /// Зарегистрировать сервисы
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDbConfig, DbConfig>(config => 
                new DbConfig( Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));
            services.AddTransient<IConnectionCreator, SqliteConnectionCreator>();

            services.AddTransient<IFileToXElementConverter, FileToXElementConverter>();
            
            services.AddTransient<IXmlImportService, XmlImportService>();
            services.AddTransient<IXmlExportService, XmlExportService>();
            
            return services;
        }
        
        /// <summary>
        /// Зарегистрировать репозитории
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISelfTestRepository, SelfTestRepository>();
            
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IXmlImportRepository, XmlImportRepository>();
            services.AddTransient<IXmlExportRepository, XmlExportRepository>();
            
            return services;
        }
        
        /// <summary>
        /// Зарегистрировать запросы
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterSqlQueries(this IServiceCollection services)
        {
            var sqlAssembly = typeof(ISqlQuery).Assembly;
            
            services.RegisterAssemblyTypes(sqlAssembly)
                .Where(t => t.GetInterfaces().Any(ti =>
                                ti.Name == nameof(ISqlQuery)))
                .AsScoped()
                .Bind();
            
            services.RegisterType<DepartmentRepositorySqlQueries>().AsScoped().PropertiesAutowired().Bind();
            services.RegisterType<XmlImportRepositorySqlQueries>().AsScoped().PropertiesAutowired().Bind();
            services.RegisterType<XmlExportRepositorySqlQueries>().AsScoped().PropertiesAutowired().Bind();
            
            return services;
        }
        
        /// <summary>
        /// Зарегистрировать профили маппинга
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection RegisterMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(XmlDepartmentProfile)
            );
            return services;
        }
    }
}