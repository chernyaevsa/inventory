using System.Data.Common;
using AuthService.Models;
using AuthService.Security;
using AuthService.Views.BaseViews;
using AuthService.Views.UserControllerViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers 
{
    [ApiController]
    [Route("user")]
    public class TokenController : ControllerBase
    {
        public TokenController(IConfiguration configuration){
            apiKeyValidation = new ApiKeyValidation(configuration);
        }
        private IApiKeyValidation apiKeyValidation;

        [HttpPost]
        [Route("get")]
        public IActionResult Get(GetTokenRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new AuthDbContext();
            var user = db.Users.FirstOrDefault(x => x.Login == request.Login && x.Password == request.Password);
            if (user == null)
                return NotFound(new BaseResponseView($"User not found", 404, null));
            var token = new Token() 
            {
                UserId = user.Id,
                Token1 = Guid.NewGuid().ToString(),
                ExpairDate = DateTime.Now.AddDays(2)
            };
            db.Tokens.Add(token);
            db.SaveChanges();
            return Ok(new BaseResponseView($"Ok", 200, token.Token1));
        }

        [HttpPost]
        [Route("check")]
        public IActionResult Check(CheckTokenRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new AuthDbContext();
            db.Users.Load();
            var token = db.Tokens.FirstOrDefault(x => x.Token1 == request.Token);
            if (token == null)
                return Unauthorized(new BaseResponseView($"This token is not valid", 401, null));
            token.ExpairDate = DateTime.Now.AddDays(2);
            db.Tokens.Update(token);
            if (token.User == null) 
                return NotFound(new BaseResponseView($"User for this token can't be found", 404, null));
            if (token.User.IsBlocked == 1) 
                return Ok(new BaseResponseView($"User is blocked", 403, null));
            if (token.ExpairDate < DateTime.Now)
                return Unauthorized(new BaseResponseView($"This token is expair", 401, null));
            return Ok(new BaseResponseView($"Ok", 200, token.User));
        }
    }
}