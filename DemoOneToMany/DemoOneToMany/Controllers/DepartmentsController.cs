using DemoOneToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DemoOneToMany.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly MainDBContext _context;
        public DepartmentsController(MainDBContext context)  
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Departments != null ?
                View(await _context.Departments.ToListAsync()) :
                Problem("null");
        }
        public async Task<IActionResult >Add()
        {
            return View();   
        }
        [HttpPost]
        public async Task<IActionResult> Add(int id,Department department)
        {
            if (id != null)

            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                RedirectToAction("Index");
            }  
            return View(department);
        }
        public async Task<IActionResult>Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dep = await _context.Departments.FindAsync(id);
            return View(dep);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id,Department department)
        {
            if (id != null)
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
               return  RedirectToAction("Index");
            }
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (emp != null)
            {
                return View(emp);
            }
            return RedirectToAction(nameof(Index));
        }
       
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = await _context.Departments .FindAsync(id);
            if (emp != null)
            {
                _context.Departments .Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }
    }
}
