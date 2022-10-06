using Weather.Models;

namespace Weather.Services;
public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    //IEnumerable<User> GetAll();
    User GetById(int id);
}

