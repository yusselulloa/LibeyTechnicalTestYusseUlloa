using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly Context _context;
        public LibeyUserRepository(Context context)
        {
            _context = context;
        }
        public void Create(LibeyUser libeyUser)
        {
            bool exists = _context.LibeyUsers.Any(u => u.DocumentNumber == libeyUser.DocumentNumber);
            if (exists)
            {
                throw new Exception("El usuario ya está registrado con este número de documento.");
            }

            _context.LibeyUsers.Add(libeyUser);
            _context.SaveChanges();
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var user = _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber)).ToList();
            var list = user.Select(LibeyUserResponse => new LibeyUserResponse()
            {
                DocumentNumber = LibeyUserResponse.DocumentNumber,
                Active = LibeyUserResponse.Active,
                Address = LibeyUserResponse.Address,
                DocumentTypeId = LibeyUserResponse.DocumentTypeId,
                Email = LibeyUserResponse.Email,
                FathersLastName = LibeyUserResponse.FathersLastName,
                MothersLastName = LibeyUserResponse.MothersLastName,
                Name = LibeyUserResponse.Name,
                Password = LibeyUserResponse.Password,
                Phone = LibeyUserResponse.Phone,
                UbigeoCode = LibeyUserResponse.UbigeoCode,
                RegionCode = GetRegion((LibeyUserResponse.UbigeoCode ?? "").PadRight(2).Substring(0, 2)).RegionCode,                
                ProvinceCode = GetProvince((LibeyUserResponse.UbigeoCode ?? "").PadRight(2).Substring(0, 4)).ProvinceCode
            }).ToList();
            
            
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();           
        }

        public Region GetRegion(string region) {
            var q = _context.Region.Where(x => x.RegionCode.Equals(region));
                   
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new Region();
        }

        public Province GetProvince(string provinceCode)
        {
            var q = _context.Province.Where(x => x.ProvinceCode.Equals(provinceCode));

            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new Province();
        }


        public List<LibeyUser> GetAll()
        {
            return _context.LibeyUsers.AsNoTracking().ToList();
        }

        public LibeyUser Delete(string documentNumber)
        {
            var usuario = _context.LibeyUsers.Find(documentNumber);
            if (usuario == null)
                return null;

            _context.LibeyUsers.Remove(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public LibeyUserResponse Update(LibeyUserResponse libeyUser)
        {
            var user = _context.LibeyUsers.Find(libeyUser.DocumentNumber);

            if (user == null)
                return null;

            _context.Entry(user).CurrentValues.SetValues(libeyUser);
            _context.SaveChanges();

            return libeyUser;
        }
    }
}