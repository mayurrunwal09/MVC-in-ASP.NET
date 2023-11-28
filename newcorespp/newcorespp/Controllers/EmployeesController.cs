 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newcorespp.Data;
using newcorespp.Models;
using newcorespp.Models.Domain;

namespace newcorespp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDemoContext mvcDemoContext;
         
        public EmployeesController(MVCDemoContext mvcDemoContext)
        {
            this.mvcDemoContext = mvcDemoContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDemoContext.Employees.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };
            await mvcDemoContext.Employees.AddAsync(employee);
            await mvcDemoContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var employee = await mvcDemoContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee != null)
            {
                var viewmodel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                 
               return await Task.Run(() => View("View",viewmodel));
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoContext.Employees.FindAsync(model.Id);

            if (employee != null)
                        {

                mvcDemoContext.Employees.Remove(employee);
                await mvcDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");
                        }   
            return RedirectToAction("Index");
        }


    }
}

