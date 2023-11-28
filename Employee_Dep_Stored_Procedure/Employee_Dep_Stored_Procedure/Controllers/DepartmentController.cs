using Employee_Dep_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Employee_Dep_Stored_Procedure.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MainDBContext _context;
        public DepartmentController(MainDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Department> customers = this._context.SearchDepartment("").ToList();
            return View(customers);
        }
        [HttpPost]
        public IActionResult Index(string DepartmentName)
        {
            if(DepartmentName!= null)
            {
                List<Department> departments = _context.SearchDepartment(DepartmentName).ToList();
                return View(departments);
            }
          return NotFound("No data is Inserted");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department customer)
        {

            var result = await _context.AddDepartment(customer.DepName);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
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

            var department = _context.Department.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Department  students)
        {
            if (id != students.DepId)
            {
                return NotFound();
            }
           // ViewData["DepId"] = new SelectList(_dbContext.Department, "DepId", "DepName");
            var result = await _context.UpdateDepartment(students.DepId, students.DepName);

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

            var department = _context.Department.Find(id);

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
            var result = await _context.DeleteDepartment(id);

            if (result == "Success")
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Failed to delete department.");
            return View(_context.Department.Find(id));
        }
    }
}
