using Service.Models;
using Service.Security;
using Service.Views.BaseViews;
using Microsoft.AspNetCore.Mvc;
using Service.Views.Employee;

namespace Service.Controllers 
{
    [ApiController]
    [Route("employee")]
    public class EmployeeController : ControllerBase
    {
        private IConfiguration configuration;
        public EmployeeController(IConfiguration configuration){
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
            var employee = db.Employees.FirstOrDefault(x => x.Id == request.Id);
            if (employee == null) 
                return NotFound(new BaseResponseView($"Employee ID {request.Id} not found", 404, null));
            var result = new BaseResponseView("Ok", 200, employee);
            return Ok(result);
        }

        [HttpPost]
        [Route("all")]
        public async Task<IActionResult> GetAll(BaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var result = new BaseResponseView("Ok", 200, db.Employees);
            return Ok(result);
        }

        [HttpPost]
        [Route("del")]
        public async Task<IActionResult> Remove(IdBaseRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var employee = db.Employees.FirstOrDefault(x => x.Id == request.Id);
            if (employee == null)
                return NotFound(new BaseResponseView($"Employee ID {request.Id} not found", 404, null));
            db.Employees.Remove(employee);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, request.Id);
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(EmployeeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var employee = request.ToObj();
            db.Employees.Add(employee);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, "DDD");
            return Ok(result);
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(EmployeeAddRequestView request){
            if (!await tokenValidation.IsValidToken(request.Token)) 
                return Unauthorized(new BaseResponseView(Constants.TokenErrorMessage, 401, null));
            var db = new InventoryContext(configuration);
            var employee = db.Employees.FirstOrDefault(x=> x.Id == request.Id);
            if (employee == null)
                return NotFound(new BaseResponseView($"Employee ID {request.Id} not found", 404, null));
            request.Edit(ref employee);
            db.Employees.Update(employee);
            db.SaveChanges();
            var result = new BaseResponseView("Ok", 200, employee.Id);
            return Ok(result);
        }

    }
}