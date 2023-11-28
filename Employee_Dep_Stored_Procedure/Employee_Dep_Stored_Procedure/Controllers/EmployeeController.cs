using Employee_Dep_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Dep_Stored_Procedure.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MainDBContext _dbContext;

        public EmployeeController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
      
        public IActionResult Index()
        {
            var employees = _dbContext.Employees.Include(d=>d.Department).ToList();
            return View(employees);
        }

        [HttpPost]
        public IActionResult Search(string EmployeeName)
        {
            if(EmployeeName!= null)
            {
                List<Employee> employees = _dbContext.SearchEmployee(EmployeeName).ToList();
                return View(employees);
            }
            else
            {
                return NotFound("No data Inserted");
            }
            
        }
       
        
        public IActionResult Create()
        {
            ViewData["DepId"] = new SelectList(_dbContext.Department, "DepId", "DepName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("EmpId,EmpName,Age,MobileNo,DOB,DepId,DepName")] Employee customer)
        {

                //var departments = _dbContext.Department.ToList();
                ViewData["DepId"] = new SelectList(_dbContext.Department, "DepId", "DepName", customer.DepId);
                var result = await _dbContext.AddEmployee(customer.EmpName, customer.Age, customer.MobileNo, customer.DOB, customer.DepId);

                if (result == "Error")
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while inserting employee.");
                    return View(customer);
                }

                return RedirectToAction("Index");
        



        }
     



        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _dbContext.Employees.Find(id);

            if (department == null)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_dbContext.Department, "DepId", "DepName");

            return View(department);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("EmpId,EmpName,Age,MobileNo,DOB,DepId,DepName")] Employee  students)
        {
            if (id != students.EmpId)
            {
                return NotFound();
            }
            ViewData["DepId"] = new SelectList(_dbContext.Department , "DepId", "DepName");
            var result = await _dbContext.UpdateEmployee(students.EmpId, students.EmpName, students.Age, students.MobileNo, students.DOB,students.DepId);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View(students);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _dbContext.Employees.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _dbContext.DeleteEmployee(id);

            if (result == "Success")
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to delete department.");
            return View(_dbContext.Employees.Find(id));
        }
    }
}
