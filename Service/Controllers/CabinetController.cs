using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Cabinet;

namespace Service.Controllers 
{
    [ApiController]
    [Route("cabinet")]
    public class CabinetController : ControllerBase
    {
        private IConfiguration configuration;
        public CabinetController(IConfiguration configuration){
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
            var cabinet = db.Cabinets.FirstOrDefault(x => x.Id == request.Id);
            if (cabinet == null) 
                return NotFound(new BaseResponseView($"Cabinet ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, cabinet);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Cabinets);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var cabinet = db.Cabinets.FirstOrDefault(x => x.Id == request.Id);
            if (cabinet == null)
                return NotFound(new BaseResponseView($"Cabinet ID {request.Id} not found", 404, null));
            db.Cabinets.Remove(cabinet);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(CabinetAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var cabinet = request.ToObj();
            db.Cabinets.Add(cabinet);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(CabinetAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var cabinet = db.Cabinets.FirstOrDefault(x=> x.Id == request.Id);
            if (cabinet == null)
                return NotFound(new BaseResponseView($"Cabinet ID {request.Id} not found", 404, null));
            request.Edit(ref cabinet);
            db.Cabinets.Update(cabinet);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, cabinet.Id);
            return Ok(result);
        }

    }
}