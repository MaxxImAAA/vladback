using API.Dtos;
using API.IServices;
using API.ServiceResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly IResponseService _responseService;
        public ResponseController(IResponseService _responseService)
        {
            this._responseService = _responseService;
        }

        [HttpPost("AddResponse")]
        public async Task<ActionResult> AddResponse(int userid, string message)
        {
            await _responseService.AddResponse(userid, message);
            return Ok();
        }

        [HttpGet("GetResponses")]
        public async Task<ActionResult<ServiceResponse<List<ResponseDto>>>> GetAllResponse()
        {
            var request = await _responseService.GetAllResponse();

            return Ok(request);
        }
    }
}
