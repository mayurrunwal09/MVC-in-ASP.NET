using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model_Popup_Using_StoredProcedure.Models;

namespace Model_Popup_Using_StoredProcedure.Controllers
{
    public class EmployeeController : Controller
    {
       
        private  MainDBContext _context { get; }
        public EmployeeController(MainDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dep = await _context.Employees.Include(d=>d.Department).ToListAsync();
            return View(dep);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {

                ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName");
                return View(new Employee());
            }
            else
            {
                var emp = _context.Employees.Find(id)
;
                if (emp == null)
                {
                    return NotFound();
                }
                ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName");
                return View(emp);
            }
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("EmpId,EmpName,City,MobileNo,DepId,DepName")]Employee employee)
        {
            if (id == 0)
            {
                ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName",employee.DepId);
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                var result = await _context.InsertOrUpdateEmployee(employee.EmpId,employee.EmpName,employee.City,employee.MobileNo,employee.DepId);
            }
            else
            {
                ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName",employee.DepId);
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                var result = await _context.InsertOrUpdateEmployee(employee.EmpId,employee.EmpName,employee.City,employee.MobileNo,employee.DepId);
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _context.Employees.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", employee) });

        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees.Find(id)
;
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.DeleteEmployee(id)
;

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View();
            }

            return Json(new { html = Helper.viewtostring(this, "_ViewAll", _context.Employees.ToListAsync()) });
        }

    }
}
