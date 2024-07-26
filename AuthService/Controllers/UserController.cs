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
            apiKeyValidation = new AdminKeyValidation(configuration);
        }
        private IApiKeyValidation apiKeyValidation;

        [HttpPost]
        public IActionResult GetById(UserRequestView request)
        {
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            AuthDbContext db = new AuthDbContext();
            var user = db.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null) 
                return NotFound(new BaseResponseView($"User ID {request.UserId} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, user);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public IActionResult GetAll(BaseRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            AuthDbContext db = new AuthDbContext();
            var result = new BaseResponseView("Ok", 200, db.Users);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public IActionResult Remove(UserRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            AuthDbContext db = new AuthDbContext();
            var user = db.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
                return NotFound(new BaseResponseView($"User ID {request.UserId} not found", 404, null));
            db.Users.Remove(user);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.UserId);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(UserAddRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            AuthDbContext db = new AuthDbContext();
            var user = new User
            {
                Name = request.Name,
                Login = request.Login,
                Password = request.Password,
                Email = request.Email
            };
            db.Users.Add(user);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, user.Id);
            return Ok(result);
        }

    }
}