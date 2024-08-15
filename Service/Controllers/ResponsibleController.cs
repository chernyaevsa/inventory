using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Responsible;

namespace Service.Controllers 
{
    [ApiController]
    [Route("responsible")]
    public class ResponsibleController : ControllerBase
    {
        private IConfiguration configuration;
        public ResponsibleController(IConfiguration configuration){
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
            var responsible = db.Responsibles.FirstOrDefault(x => x.Id == request.Id);
            if (responsible == null) 
                return NotFound(new BaseResponseView($"Responsible ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, responsible);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Responsibles);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var responsible = db.Responsibles.FirstOrDefault(x => x.Id == request.Id);
            if (responsible == null)
                return NotFound(new BaseResponseView($"Responsible ID {request.Id} not found", 404, null));
            db.Responsibles.Remove(responsible);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(ResponsibleAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var responsible = request.ToObj();
            db.Responsibles.Add(responsible);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(ResponsibleAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var responsible = db.Responsibles.FirstOrDefault(x=> x.Id == request.Id);
            if (responsible == null)
                return NotFound(new BaseResponseView($"Responsible ID {request.Id} not found", 404, null));
            responsible = request.ToObj(true);
            db.Responsibles.Update(responsible);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, responsible.Id);
            return Ok(result);
        }

    }
}