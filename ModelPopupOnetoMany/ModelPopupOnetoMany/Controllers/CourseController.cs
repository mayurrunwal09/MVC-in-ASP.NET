using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelPopupOnetoMany.Models;

namespace ModelPopupOnetoMany.Controllers
{
    public class CourseController : Controller
    {
        private readonly MainDBContext _dbContext;
        public CourseController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var course = await _dbContext.Courses.ToListAsync();
            return View(course);
        }
        [HttpGet]
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                return View(new Course());
            }
            else
            {
                var course = await _dbContext.Courses.FindAsync(id);
                if(course == null)
                {
                    return NotFound();
                }
                return View(course);
            }
        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id, Course course)
        {
            if (id == 0)
            {
                _dbContext.Courses.Add(course);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {

                    _dbContext.Update(course);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExist(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _dbContext.Courses.ToListAsync()) });
            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", course) });


        }

        private bool CourseExist(int courseId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var cr = await _dbContext.Courses.FindAsync(id);
            if (cr == null)
            {
                return NotFound();
            }
            else
            {
                _dbContext.Remove(cr);
                await _dbContext.SaveChangesAsync();
            }
            return Json(new { html = Helper.viewtostring(this, "_ViewAll", _dbContext.Students.ToListAsync()) });
        }
        public IActionResult CourseByFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {

                return View("CoursesByFirstName", new List<Course>());
            }

            firstName = firstName.ToLower();

            var enrolledCourses = _dbContext.Enrollments
                .Where(e => e.Student != null && e.Student.FirstName != null && e.Student.FirstName.ToLower().Contains(firstName))
                .Select(e => e.Course)
                .ToList();

            return View("CoursesByFirstName", enrolledCourses);
        }

    }
}
