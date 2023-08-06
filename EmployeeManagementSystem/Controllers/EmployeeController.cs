using EmployeeManagementSystem.BL;
using EmployeeManagementSystem.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeBL _bl;

        public EmployeeController(EmployeeBL bl) {
            _bl = bl;
        }

        [HttpGet]
        [Route("GetAll")]
        
        public List<Employee> GetEmployees()
        {
            return _bl.GetAllEmployee();
        }

        [HttpGet]
        public Employee? GetEmployee(Guid id)
        {
            return _bl.GetEmployee(new Employee() { Id = id });
        }

        [HttpPost]
        public Employee? AddEmployee(Employee employee)
        {
            return _bl.AddEmployee(employee);   
        }
        [HttpPut]
        public Employee? Update(Employee employee)
        {
            return _bl.Update(employee);
        }
        [HttpDelete]
        public bool Delete(Guid id)
        {
            return _bl.Delete(id);
        }
    }
}
