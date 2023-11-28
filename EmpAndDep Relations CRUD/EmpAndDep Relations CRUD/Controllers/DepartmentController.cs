using EmpAndDep_Relations_CRUD.Models;
using EmpAndDep_Relations_CRUD.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpAndDep_Relations_CRUD.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MainDbContext _context;

        public DepartmentController(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Department != null ?
                View(await _context.Department.ToListAsync()) :
                Problem("Department is null");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var department = await _context.Department.FirstOrDefaultAsync(e => e.Department_Id == id);
                return View(department);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department Department, int id)
        {
            if (id != null)
            {
                _context.Add(Department);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Department == null)
            {
                return NotFound();
            }
            var department = await _context.Department.FindAsync(id);
;
            if (department == null)
            
                return NotFound();
            
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if(id != null)
            {
                _context.Department.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);


          /* if(id == null)
           {
                return NotFound();
           }
            var dep = await _context.Department.FirstOrDefaultAsync(e=>e.Department_Id==id);
            if (dep != null)
           {
                _context.Department.Update(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           }
            return View(department);*/

        }

        

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dep = await _context.Department.FirstOrDefaultAsync(d => d.Department_Id == id);
            if (dep == null)
            {
                return NotFound();
            }
            return View(dep);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dep = await _context.Department.FindAsync(id);
            if (dep != null)
            {
                _context.Department.Remove(dep);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
       
    }
}
    

