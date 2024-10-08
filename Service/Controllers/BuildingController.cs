using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Building;

namespace Service.Controllers 
{
    [ApiController]
    [Route("building")]
    public class BuildingController : ControllerBase
    {
        private IConfiguration configuration;
        public BuildingController(IConfiguration configuration){
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
            var building = db.Buildings.FirstOrDefault(x => x.Id == request.Id);
            if (building == null) 
                return NotFound(new BaseResponseView($"Building ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, building);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (! await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Buildings);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var user = db.Buildings.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
                return NotFound(new BaseResponseView($"Building ID {request.Id} not found", 404, null));
            db.Buildings.Remove(user);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(BuildingAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var building = request.ToObj();
            db.Buildings.Add(building);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, building.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(BuildingAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var building = db.Buildings.FirstOrDefault(x=> x.Id == request.Id);
            if (building == null)
                return NotFound(new BaseResponseView($"Building ID {request.Id} not found", 404, null));
            request.Edit(ref building);
            db.Buildings.Update(building);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, building.Id);
            return Ok(result);
        }

    }
}