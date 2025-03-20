using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class UbigeoAggregate : IUbigeoAggregate
    {
        private readonly IUbigeoRepository _repository;

        public UbigeoAggregate(IUbigeoRepository repository)
        {
            _repository = repository;
        }

        public List<Ubigeo> GetAll(string provinceCode)
        {
            var ubigeo = _repository.GetAll(provinceCode);
            return ubigeo;
        }
    }
}
