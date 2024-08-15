using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Status;

namespace Service.Controllers 
{
    [ApiController]
    [Route("status")]
    public class StatusController : ControllerBase
    {
        private IConfiguration configuration;
        public StatusController(IConfiguration configuration){
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
            var status = db.Statuses.FirstOrDefault(x => x.Id == request.Id);
            if (status == null) 
                return NotFound(new BaseResponseView($"Status ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, status);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Statuses);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var status = db.Statuses.FirstOrDefault(x => x.Id == request.Id);
            if (status == null)
                return NotFound(new BaseResponseView($"Status ID {request.Id} not found", 404, null));
            db.Statuses.Remove(status);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(StatusAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var status = request.ToObj();
            db.Statuses.Add(status);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(StatusAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var status = db.Statuses.FirstOrDefault(x=> x.Id == request.Id);
            if (status == null)
                return NotFound(new BaseResponseView($"Status ID {request.Id} not found", 404, null));
            status = request.ToObj(true);
            db.Statuses.Update(status);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, status.Id);
            return Ok(result);
        }

    }
}