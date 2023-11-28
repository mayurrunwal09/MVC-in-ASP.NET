using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Model_Popup_Using_StoredProcedure.Models;

namespace Model_Popup_Using_StoredProcedure.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MainDBContext _dbContext;
        public DepartmentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
       public async Task<IActionResult> Index()
        {
            var dep = await _dbContext.Departments.ToListAsync();
            return View(dep);
        }
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Department());
            }
            else
            {
                var user = await _dbContext.Departments.FindAsync(id);
                if (user == null)
                {
                    return NotFound();

                }
                return View(user);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Department department)
        {
            if (id == 0)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
                var result = await _dbContext.InsertOrUpdateDepartment(department.DepId, department.DepName);
            }
            else
            {
                _dbContext.Departments.Update(department);
                await _dbContext.SaveChangesAsync();
                var result = await _dbContext.InsertOrUpdateDepartment(department.DepId, department.DepName);
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _dbContext.Departments.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", department) });

        }




        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _dbContext.Departments.Find(id)
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
            var result = await _dbContext.DeleteDepartment(id)
;

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View();
            }

            return Json(new { html = Helper.viewtostring(this, "_ViewAll", _dbContext.Departments.ToListAsync()) });
        }
    }
}
