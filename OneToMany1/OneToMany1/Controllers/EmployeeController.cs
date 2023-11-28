using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneToMany1.Data;
using OneToMany1.Models;

namespace OneToMany1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly OneToManyDBContext _context;

        public EmployeeController(OneToManyDBContext context)
        {
           this._context = context;
        }

        public IActionResult Index()
        {
            var employees =  _context.Employees.Include(e => e.Department).ToList();
            return View(employees);
        }

        public IActionResult Details(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Department)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName");
            return View(employee);
        }

        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Find(id)
    ;
            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.Deptid);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(UpdateEmployee employee)
        {
            if (ModelState.IsValid)
            {
                _context.UpdateEmployees.Update(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "DepartmentName", employee.Id);
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id)
    ;
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id)
    ;
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
