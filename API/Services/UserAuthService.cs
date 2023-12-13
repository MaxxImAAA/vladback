using API.Data;
using API.Dtos;
using API.IServices;
using API.Models;
using API.ServiceResponses;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly ApplicationDbContext db;
        public UserAuthService(ApplicationDbContext db)
        {
            this.db = db;   
        }

        public async Task<ServiceResponse<int>> Login(string email, string password)
        {
            var service = new ServiceResponse<int>();
            var user = await db.UserAuths.FirstOrDefaultAsync(x=>x.Email == email && x.Password == password);

            if(user == null)
            {
                service.Description = "Пользователь не найден";
                service.StatusCode = false;
            }
            else
            {
                await db.Entry(user).Reference(x => x.User).LoadAsync();
                service.Data = user.User.Id;
                service.Description = "Вход успешен";
                service.StatusCode = true;

            }

            return service;
        }

        public async Task Register(RegisterForm regform)
        {
            try
            {
                var newUser = new UserAuth()
                {
                    Email = regform.Email,
                    Password = regform.Password,
                    User = new User
                    {
                        Name = regform.Name,
                        Cart = new Cart(),
                        Responses = new List<Response>(),

                    }
                };

               await db.UserAuths.AddAsync(newUser);
               await  db.SaveChangesAsync();


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
