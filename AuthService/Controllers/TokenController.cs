using System.Data.Common;
using AuthService.Models;
using AuthService.Security;
using AuthService.Views.BaseViews;
using AuthService.Views.UserControllerViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Controllers 
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private IConfiguration configuration;

        public TokenController(IConfiguration configuration){
            this.configuration = configuration;
            apiKeyValidation = new ApiKeyValidation(configuration);
            adminKeyValidation = new AdminKeyValidation(configuration);
        }
        private IApiKeyValidation apiKeyValidation;
        private IApiKeyValidation adminKeyValidation;

        [HttpPost]
        [Route("issue")]
        public IActionResult Issue(IssueTokenRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new AuthDbContext(configuration);
            var user = db.Users.FirstOrDefault(x => x.Login == request.Login && x.Password == request.Password);
            if (user == null)
                return NotFound(new BaseResponseView($"User not found", 404, null));
            var token = new Token() 
            {
                UserId = user.Id,
                Token1 = Guid.NewGuid().ToString(),
                ExpireDate = DateTime.Now.AddDays(2)
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
            var db = new AuthDbContext(configuration);
            db.Users.Load();
            var token = db.Tokens.FirstOrDefault(x => x.Token1 == request.Token);
            if (token == null)
                return Unauthorized(new BaseResponseView($"This token not valid", 401, null));
            token.ExpireDate = DateTime.Now.AddDays(2);
            db.Tokens.Update(token);
            if (token.User == null) 
                return NotFound(new BaseResponseView($"User for this token can't be found", 404, null));
            if (token.User.IsBlocked == 1) 
                return Ok(new BaseResponseView($"User is blocked", 403, null));
            if (token.ExpireDate < DateTime.Now)
                return Unauthorized(new BaseResponseView($"This token expired", 401, null));
            return Ok(new BaseResponseView($"Ok", 200, token.User));
        }

        [HttpPost]
        [Route("get")]
        public IActionResult Get(GetTokenRequestView request){
            if (!adminKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            var db = new AuthDbContext(configuration);
            db.Users.Load();
            if (request.Token == "")
                return Ok(new BaseResponseView("Ok", 200, db.Tokens));
            var token = db.Tokens.FirstOrDefault(x => x.Token1 == request.Token);
            if (token == null)
                return NotFound(new BaseResponseView("This token not found", 404, null));
            return Ok(new BaseResponseView("Ok", 200, token));
            
        }

        [HttpPost]
        [Route("expire")]
        public IActionResult Expire(ExpireTokenRequestView request){
            if (!adminKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.AdminKeyErrorMessage, 401, null));
            var db = new AuthDbContext(configuration);
            var token = db.Tokens.FirstOrDefault(x => x.Token1 == request.Token);
            if (token == null)
                return NotFound(new BaseResponseView("This token not found", 404, null));
            token.ExpireDate = DateTime.Now;
            db.SaveChanges();
            return Ok(new BaseResponseView("Ok", 200, token));
        }

    }
}