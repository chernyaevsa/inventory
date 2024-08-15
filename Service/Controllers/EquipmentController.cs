using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Equipment;

namespace Service.Controllers 
{
    [ApiController]
    [Route("equipment")]
    public class EquipmentController : ControllerBase
    {
        private IConfiguration configuration;
        public EquipmentController(IConfiguration configuration){
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
            var equipment = db.Equipment.FirstOrDefault(x => x.Id == request.Id);
            if (equipment == null) 
                return NotFound(new BaseResponseView($"Equipment ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, equipment);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Equipment);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var equipment = db.Equipment.FirstOrDefault(x => x.Id == request.Id);
            if (equipment == null)
                return NotFound(new BaseResponseView($"Equipment ID {request.Id} not found", 404, null));
            db.Equipment.Remove(equipment);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(EquipmentAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var equipment = request.ToObj();
            db.Equipment.Add(equipment);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(EquipmentAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var equipment = db.Equipment.FirstOrDefault(x=> x.Id == request.Id);
            if (equipment == null)
                return NotFound(new BaseResponseView($"Equipment ID {request.Id} not found", 404, null));
            equipment = request.ToObj(true);
            db.Equipment.Update(equipment);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, equipment.Id);
            return Ok(result);
        }

    }
}