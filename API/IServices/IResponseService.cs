using API.Dtos;
using API.ServiceResponses;

namespace API.IServices
{
    public interface IResponseService
    {
        Task<ServiceResponse<List<ResponseDto>>> GetAllResponse();
        Task AddResponse(int userid, string message);
    }
}
