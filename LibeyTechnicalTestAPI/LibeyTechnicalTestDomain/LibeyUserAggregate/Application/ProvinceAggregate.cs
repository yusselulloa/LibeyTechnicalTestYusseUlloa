using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class ProvinceAggregate : IProvinceAggregate
    {
        private readonly IProvinceRepository _repository;

        public ProvinceAggregate(IProvinceRepository repository)
        {
            _repository = repository;
        }

        public List<Province> GetAll(string regionCode)
        {
            var province = _repository.GetAll(regionCode);
            return province;
        }
    }
}
