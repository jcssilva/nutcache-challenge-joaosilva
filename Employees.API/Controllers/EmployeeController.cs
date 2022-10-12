using Employees.API.Data;
using Employees.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        private async Task<Employee> FindEmployeeByCPF(string cpf)
        {
            return await employeeDbContext.Employees.FirstOrDefaultAsync(x => x.CPF == cpf);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{cpf}")]
        public async Task<IActionResult> GetEmployee([FromRoute] string cpf)
        {
            var employee = await FindEmployeeByCPF(cpf);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound("Employee not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingEmployee = await FindEmployeeByCPF(employee.CPF);
            if (existingEmployee == null)
            {
                employee.Id = new Guid();

                await employeeDbContext.Employees.AddAsync(employee);
                await employeeDbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetEmployee), new { cpf = employee.CPF}, employee);
            }

            return BadRequest("Employee alredy created.");
        }

        [HttpPut]
        [Route("{cpf}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] string cpf, [FromBody] Employee employee)
        {
            var existingEmployee = await FindEmployeeByCPF(cpf);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.Email = employee.Email;
                existingEmployee.StartDate = employee.StartDate;
                existingEmployee.DateBirth = employee.DateBirth;
                existingEmployee.Gender = employee.Gender;
                existingEmployee.Team = employee.Team;
                await employeeDbContext.SaveChangesAsync();
                return Ok(existingEmployee);
            }
            return NotFound("Employee not found");
        }

        

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string cpf)
        {
            var existingEmployee = await FindEmployeeByCPF(cpf);
            if (existingEmployee != null)
            {
                employeeDbContext.Remove(existingEmployee);
                await employeeDbContext.SaveChangesAsync();
                return Ok(existingEmployee);
            }
            return NotFound("Employee not found");
        }
    }
}
