using Employee_Department.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Department.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetEmployees()
       {
            var employeeList = _context.Employees.ToList();
            return Ok(employeeList);
        }
        [HttpPost]
        public async Task<IActionResult> SaveEmployee([FromBody] List<Employee> employee)
        {
           
                foreach (Employee emp in employee)
            {
                await _context.Employees.AddAsync(emp);
                _context.SaveChanges();

            }
            
            return Ok(new { message = "data Saved" });
        }


        [HttpPut]
        public IActionResult UpdateEmployee([FromBody]Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Ok(new { message = "data updated" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
           
            var employeeInDb = _context.Employees.Find(id);
            _context.Employees.Remove(employeeInDb);
            _context.SaveChanges();
            return Ok(new { message = "data deleted" });
        }
    }
}
