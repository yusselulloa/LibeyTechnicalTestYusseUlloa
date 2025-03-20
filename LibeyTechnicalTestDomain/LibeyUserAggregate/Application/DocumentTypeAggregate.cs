using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class DocumentTypeAggregate : IDocumentTypeAggregate
    {
        private readonly IDocumentTypeRepository _repository;

        public DocumentTypeAggregate(IDocumentTypeRepository repository)
        {
            _repository = repository;
        }

        public List<DocumentType> GetAll()
        {
            var documentType = _repository.GetAll();
            return documentType;
        }
    }
}
