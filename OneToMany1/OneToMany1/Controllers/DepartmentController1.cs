using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToMany1.Data;
using OneToMany1.Models;

namespace OneToMany1.Controllers
{
    public class DepartmentController1 : Controller
    {
        private readonly OneToManyDBContext _context;

        public DepartmentController1(OneToManyDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = _context.Departments.Find(id)
    ;

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = _context.Departments.Find(id)
    ;

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(UpdateDepartment department)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateDepartments.Update(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = _context.Departments.Find(id)
    ;

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.Find(id)
    ;

            if (department == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
