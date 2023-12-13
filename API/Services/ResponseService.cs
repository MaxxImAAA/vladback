using API.Data;
using API.Dtos;
using API.IServices;
using API.Models;
using API.ServiceResponses;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Response = API.Models.Response;

namespace API.Services
{
    public class ResponseService : IResponseService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public ResponseService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
            
        }
        public async  Task AddResponse(int userid, string message)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userid);
            await db.Entry(user).Reference(x => x.UserAuth).LoadAsync();
            var response = new Response
            {
                Email = user.UserAuth.Email,
                Message = message,
                Name = user.Name,
                User = user
            };

            await db.Responses.AddAsync(response);
            await db.SaveChangesAsync();
            
        }

        public async Task<ServiceResponse<List<ResponseDto>>> GetAllResponse()
        {
            var service = new ServiceResponse<List<ResponseDto>>();

            try
            {
                var responses = await db.Responses.ToListAsync();

                service.Data = mapper.Map<List<ResponseDto>>(responses);
                service.Description = "Все отзывы выведены";
                service.StatusCode = true;

            }
            catch (Exception ex)
            {
                service.Description = ex.Message;
                service.StatusCode = false;
            }

            return service;
        }
    }
}
