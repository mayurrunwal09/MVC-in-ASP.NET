using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ModelPopupOnetoMany.Models;

namespace ModelPopupOnetoMany.Controllers
{
    public class StudentController : Controller
    {
        private readonly MainDBContext _dbContext;
        public StudentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var stud = await _dbContext.Students.ToListAsync();
            return View(stud);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Student());
            }
            else
            {
                var stud = await _dbContext.Students.FindAsync(id);
                if (stud == null)
                {
                    return NotFound();
                }
                return View(stud);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, Student student)
        {
            if (id == 0)
            {
                _dbContext.Add(student);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    _dbContext.Update(student);
                    await _dbContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!StudentExist(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _dbContext.Students.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", student) });


        }

        private bool StudentExist(int studentId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            var stud = await _dbContext.Students.FindAsync(id);
            if (stud == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Remove(stud);
                await _dbContext.SaveChangesAsync();
            }
            return Json(new { html = Helper.viewtostring(this, "_ViewAll", _dbContext.Students.ToListAsync()) });
        }
      
    }
}
