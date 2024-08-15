using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Type;

namespace Service.Controllers 
{
    [ApiController]
    [Route("type")]
    public class TypeController : ControllerBase
    {
        private IConfiguration configuration;
        public TypeController(IConfiguration configuration){
            tokenValidation = new TokenValidation(configuration);
            this.configuration = configuration;
        }
        private ITokenValidation tokenValidation;

        [HttpPost]
        public async Task<IActionResult> GetById(IdBaseRequestView request)
        {
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var type = db.Types.FirstOrDefault(x => x.Id == request.Id);
            if (type == null) 
                return NotFound(new BaseResponseView($"Type ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, type);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Types);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var type = db.Types.FirstOrDefault(x => x.Id == request.Id);
            if (type == null)
                return NotFound(new BaseResponseView($"Type ID {request.Id} not found", 404, null));
            db.Types.Remove(type);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(TypeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var type = request.ToObj();
            db.Types.Add(type);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(TypeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var type = db.Types.FirstOrDefault(x=> x.Id == request.Id);
            if (type == null)
                return NotFound(new BaseResponseView($"Type ID {request.Id} not found", 404, null));
            type = request.ToObj(true);
            db.Types.Update(type);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, type.Id);
            return Ok(result);
        }

    }
}