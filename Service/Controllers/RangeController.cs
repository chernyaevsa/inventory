using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Range;

namespace Service.Controllers 
{
    [ApiController]
    [Route("range")]
    public class RangeController : ControllerBase
    {
        private IConfiguration configuration;
        public RangeController(IConfiguration configuration){
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
            var range = db.Ranges.FirstOrDefault(x => x.Id == request.Id);
            if (range == null) 
                return NotFound(new BaseResponseView($"Range ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, range);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Ranges);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var range = db.Ranges.FirstOrDefault(x => x.Id == request.Id);
            if (range == null)
                return NotFound(new BaseResponseView($"Range ID {request.Id} not found", 404, null));
            db.Ranges.Remove(range);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(RangeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var range = request.ToObj();
            db.Ranges.Add(range);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(RangeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var range = db.Ranges.FirstOrDefault(x=> x.Id == request.Id);
            if (range == null)
                return NotFound(new BaseResponseView($"Range ID {request.Id} not found", 404, null));
            request.Edit(ref range);
            db.Ranges.Update(range);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, range.Id);
            return Ok(result);
        }
    }
}