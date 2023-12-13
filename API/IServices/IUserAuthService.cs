using API.Dtos;
using API.ServiceResponses;

namespace API.IServices
{
    public interface IUserAuthService
    {
         Task Register(RegisterForm regform);
         Task<ServiceResponse<int>> Login (string email, string password);
    }
}
