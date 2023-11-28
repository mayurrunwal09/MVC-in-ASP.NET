using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModelPopupOnetoMany.Models;

namespace ModelPopupOnetoMany.Controllers
{
    public class EnrollmentController : Controller
    {
        #region Property and constructor   
        private readonly MainDBContext _dbContext;
        public EnrollmentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion


        public async Task<IActionResult> Index()
        {
            var enroll = await _dbContext.Enrollments.Include(d=>d.Student).Include(d=>d.Course).ToListAsync(); 
            return View(enroll);
        }
        [HttpGet]
        public async Task<IActionResult>AddOrEdit(int id = 0)
        {
            if(id == 0)
            {
                
                ViewData["StudentId"] = new SelectList(_dbContext.Students, "StudentId", "FirstName", "LastName", "DOB");
                ViewData["CourseId"] = new SelectList(_dbContext.Courses, "CourseId", "CourseName", "Instructor", "Credit");
                return View(new Enrollment());
            }
            else
            {
                var enroll = await _dbContext.Enrollments.Include(d=>d.Student).Include(d=>d.Course).FirstOrDefaultAsync(d=>d.EnrollmentId==id);    
                if(enroll == null)
                {
                    return NotFound();
                }
                return View(enroll);
            }
        }
        [HttpPost]
        public async Task<IActionResult>AddOrEdit(int id, [Bind("EnrollmentId,DateOfEnrollment,StudentId,CourseId")]Enrollment enrollment)
        {
            if (id == 0)
            {
                ViewData["StudId"] = new SelectList(_dbContext.Students, "StudId", "FirstName", "LastName", "DOB");
                ViewData["CourseId"] = new SelectList(_dbContext.Courses, "CourseId", "CourseName", "Instructor", "Credit");
                _dbContext.Add(enrollment);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {
                    ViewData["StudId"] = new SelectList(_dbContext.Students, "StudId", "FirstName", "LastName", "DOB");
                    ViewData["CourseId"] = new SelectList(_dbContext.Courses, "CourseId", "CourseName", "Instructor", "Credit");
                    _dbContext.Update(enrollment);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExist(enrollment.EnrollmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.viewtostring(this, "_ViewAll", _dbContext.Enrollments.ToListAsync()) });

            }
            return Json(new { isValid = false, html = Helper.viewtostring(this, "AddOrEdit", enrollment) });


        }

        private bool EnrollmentExist(int enrollmentId)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var enrol = await _dbContext.Enrollments.Include(d => d.Student).Include(d => d.Course).FirstOrDefaultAsync(d => d.EnrollmentId == id);
            _dbContext.Enrollments.Remove(enrol);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.viewtostring(this, "_ViewAll", enrol) });

        }

        public IActionResult CoursesByFirstName(string firstName)
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
        public IActionResult GetStudentsByEnrollment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetStudentsByEnrollment(int enrollmentId)
        {
            var students = _dbContext.Enrollments
                .Where(e => e.EnrollmentId == enrollmentId)
                .Select(e => e.Student)
                .ToList();

            return View(students);
        }
        public IActionResult GetEnrollmentDates()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetEnrollmentDates(int studentId)
        {
            var enrollmentDates = _dbContext.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.DateOfEnrollment)
                .ToList();

            return View(enrollmentDates);
        }


    }
}
