using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        public LibeyUserAggregate(ILibeyUserRepository repository)
        {
            _repository = repository;
        }
        public void Create(UserUpdateorCreateCommand command)
        {
            LibeyUser libeyUser = new LibeyUser
            (
                command.DocumentNumber,
                command.DocumentTypeId,
                command.Name,
                command.FathersLastName,
                command.MothersLastName,
                command.Address,
                command.UbigeoCode,
                command.Phone,
                command.Email,
                command.Password                
            );
            _repository.Create(libeyUser);
        }     

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }
        public List<LibeyUser> GetAll()
        {
            var users = _repository.GetAll();
            return users;
        }

        public LibeyUser Delete(string documentNumber)
        {
            return _repository.Delete(documentNumber);            
        }

        public LibeyUserResponse Update(LibeyUserResponse libeyUser)
        {
            return _repository.Update(libeyUser);
        }
    }
}