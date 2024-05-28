
using ZooFerma.Models.Dao;
using ZooFerma.Models.Dto; 

namespace ZooFerma.Services.Dao.Impls
{
    public class UserServiceImpl : IUserService
    {

        private readonly DataContext _dataContext;
        private readonly CryptService _cryptService;

        public UserServiceImpl(DataContext dataContext, CryptService cryptService) 
        {  
            _dataContext = dataContext;
            _cryptService = cryptService;
        }

        public List<string> GetEmails()
        {
            return _dataContext.Users.Select(x => x.email).ToList();
        }

        public bool SaveNewPassword(string email, string newPass)
        {
            User? user = _dataContext.Users.Where(x => x.email.Equals(email)).FirstOrDefault();
            if(user != null) 
            {
                user.password = _cryptService.Encrypt(newPass);
                _dataContext.SaveChanges();

                return true;
            }

            return false;
        }

        public void SaveUser(RegistrationDto dto)
        {
            dto.password = _cryptService.Encrypt(dto.password);
            User newUser = new User(dto);
            newUser.status = false;
            newUser.user_role = 2;
            _dataContext.Users.Add(newUser);
            _dataContext.SaveChanges();
        }

        public User? GetUser(string email)
        {
            return _dataContext.Users.Where(x => x.email.Equals(email)).FirstOrDefault();
        }

        public string? GetPassword(string email)
        {
            return _dataContext.Users.Where(x => x.email.Equals(email)).Select(x => x.password).FirstOrDefault();
        }

        public User? GetUserByEmailAndPassword(string email, string password)
        {
            password = _cryptService.Encrypt(password);
            return _dataContext.Users.Where(x => x.email == email && x.password == password).FirstOrDefault();
        }
    }
}
