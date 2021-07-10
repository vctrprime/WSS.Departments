using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using WSS.Departments.DAL.Repositories.Abstract.Departments;
using WSS.Departments.ServiceModels;
using WSS.Departments.Services.Extensions;
using WSS.Departments.Services.Xml.Abstract;

namespace WSS.Departments.Services.Xml.Concrete
{
    public class XmlExportService : IXmlExportService
    {
        private readonly IMapper _mapper;
        private readonly IXmlExportRepository _repository;

        public XmlExportService(IXmlExportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<XElement> Export()
        {
            var departments = await _repository.Get();

            var xmlModel = _mapper.Map<XmlDepartmentsModel>(departments);

            var xml = xmlModel.ToXElement<XmlDepartmentsModel>();

            return xml;
        }
    }
}