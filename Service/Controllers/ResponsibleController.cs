using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Building;

namespace Service.Controllers 
{
    [ApiController]
    [Route("responsible")]
    public class ResponsibleController : ControllerBase
    {
        private IConfiguration configuration;
        public ResponsibleController(IConfiguration configuration){
            apiKeyValidation = new ApiKeyValidation(configuration);
            this.configuration = configuration;
        }
        private IApiKeyValidation apiKeyValidation;

        [HttpPost]
        public IActionResult GetById(IdBaseRequestView request)
        {
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var building = db.Buildings.FirstOrDefault(x => x.Id == request.Id);
            if (building == null) 
                return NotFound(new BaseResponseView($"Building ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, building);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public IActionResult GetAll(BaseRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Buildings);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public IActionResult Remove(IdBaseRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
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
        public IActionResult Add(BuildingAddRequestView request){
            if (!apiKeyValidation.IsValidApiKey(request.ApiKey)) 
                return Unauthorized(new BaseResponseView(Constants.ApiKeyErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            /*var building = new Building
            {
                Name = request.Name,
                Login = request.Login,
                Password = request.Password,
                Email = request.Email
            };
            db.Users.Add(user);
            db.SaveChanges();*/
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

    }
}