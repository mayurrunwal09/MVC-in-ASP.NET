using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Controllers
{
    [Authorize]
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
            if(id == 0)
            {
                return View(new Department());
            }
            else
            {
                var dep = await _dbContext.Departments.FindAsync(id);
                return View(dep);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>AddOrEdit(int id,Department department)
        {
            if (id == 0)
            {
                _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _dbContext.Departments.Update(department);
                    await _dbContext.SaveChangesAsync();
                }
                catch
                {
                    if (!DepExist(department.DepId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Departments .ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", department ) });

        }

        private bool DepExist(int depId)
        {
            throw new NotImplementedException();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Departments.FindAsync(id);
            _dbContext.Departments .Remove(emp) ;
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }
    }
}
