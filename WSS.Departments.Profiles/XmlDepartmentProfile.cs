using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Internal;
using WSS.Departments.Domain.Models;
using WSS.Departments.Domain.Models.Xml;
using WSS.Departments.ServiceModels;

namespace WSS.Departments.Profiles
{   
    /// <summary>
    /// Профиль для маппинга XmlDepartments
    /// </summary>
    public class XmlDepartmentProfile : Profile
    {
        public XmlDepartmentProfile()
        {
            CreateMap<Department, XmlDepartment>();
            
            CreateMap<IEnumerable<Department>, XmlDepartmentsModel>()
                .ForMember(dest => dest.Departments,
                    act => act.MapFrom(m => GetFirstLevelDepartments(m.ToList())))
                .AfterMap((src, dest, context) => dest.Departments.ForAll(d => PopulateChildren(d, src.ToList(), context)));;

        }

        /// <summary>
        /// Получить первый урвоень потомков
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        private IEnumerable<Department> GetFirstLevelDepartments(List<Department> departments)
        {
            var root = departments.SingleOrDefault(d => !d.ParentId.HasValue);
            
            var result = departments.Where(d => d.ParentId == root?.Id);
            
            return result;
        }
        
        /// <summary>
        /// Заполнить потомков
        /// </summary>
        /// <param name="root"></param>
        /// <param name="departments"></param>
        /// <param name="context"></param>
        private void PopulateChildren(XmlDepartment root, List<Department> departments, ResolutionContext context)
        {
            root.Children = new List<XmlDepartment>();
            var children = departments.Where(d => d.ParentId == root.Id);

            foreach (var child in children)
            {
                var xmlChild = context.Mapper.Map<XmlDepartment>(child);
                
                root.Children.Add(xmlChild);
                PopulateChildren(xmlChild, departments, context);
            }
        }
    }
}