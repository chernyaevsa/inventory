using AuthService.Models;
using AuthService.Security;
using AuthService.Views.BaseViews;
using AuthService.Views.UserControllerViews;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers 
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        public UserController(IConfiguration configuration){
            apiKeyValidation = new ApiKeyValidation(configuration);
        }
        private IApiKeyValidation apiKeyValidation;

        [HttpPost]
        public IActionResult GetById(GetUserByIdRequestView request)
        {
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView("ApiKey is not valid", 401, null));
            AuthDbContext db = new AuthDbContext();
            var user = db.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null) 
                return NotFound(new BaseResponseView($"User can not be found by id = {request.UserId}", 404, null));
            var result = new BaseResponseView("Ok", 200, user);
            return Ok(result);
        }

    }
}