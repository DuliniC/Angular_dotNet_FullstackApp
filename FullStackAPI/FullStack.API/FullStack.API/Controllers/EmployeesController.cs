using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStackDbContext fullStackDbContext;
        public EmployeesController(FullStackDbContext fullStackDbContext)
        {
            this.fullStackDbContext = fullStackDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await fullStackDbContext.Employees.AddAsync(employeeRequest);
            await fullStackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]  Guid id)
        {
            var employee = await fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id.ToString() == id.ToString());
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee employeeRequest)
        {
            var employee = await fullStackDbContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            employee.Name = employeeRequest.Name;
            employee.Email = employeeRequest.Email;
            employee.Phone = employeeRequest.Phone;
            employee.Salary = employeeRequest.Salary;
            employee.Department = employeeRequest.Department;

            await fullStackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await fullStackDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            fullStackDbContext.Employees.Remove(employee);
            await fullStackDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
