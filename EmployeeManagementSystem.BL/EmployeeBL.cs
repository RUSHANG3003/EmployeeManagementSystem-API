using EmployeeManagementSystem.Core;
using EmployeeManagementSystem.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagementSystem.BL
{
    public class EmployeeBL
    {
        private readonly EmployeeRepository _repository;

        public EmployeeBL(EmployeeRepository repository)
        {
            _repository = repository;
        }

        public List<Employee> GetAllEmployee()
        {
            return _repository.GetAllEmployee();
        }

        public Employee? AddEmployee(Employee employee)
        {
            var existingemployee = GetEmployee(new Employee() { Email = employee.Email });
            if(existingemployee != null)
            {
                throw new Exception("Email Already Exists");
            }

            var id = _repository.AddEmployee(employee);
            if(id != null)
            {
                employee.Id = id;
            }

            return employee;
        }
        
        public Employee? GetEmployee(Employee employee)
        {
            return _repository.GetEmployee(employee);
        }
        public Employee? Search(Employee employee)
        {
            return _repository.GetEmployee(employee);
        }
        public Employee? Update(Employee employee)
        {
            var existingCompany = GetEmployee(new Employee() { Id = employee.Id });
            if (existingCompany == null)
            {
                throw new Exception("please enter correct Id");
            }

            return _repository.UpdateEmployee(employee);
        }
        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}