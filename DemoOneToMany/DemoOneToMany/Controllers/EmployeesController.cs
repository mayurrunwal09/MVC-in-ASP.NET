using DemoOneToMany.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DemoOneToMany.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MainDBContext _context;
        public EmployeesController(MainDBContext context) 
        { 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var details = _context.Employees.Include(e => e.department);
           return View(await details.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.Include(e=>e.department).FirstOrDefaultAsync(e=>e.Id==id);
            if(emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        [HttpGet]
      public IActionResult Add()
        {
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, [Bind("Name,Location,DepId")]Employee employee)
        {
            if(id != null)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["DepId"] = new SelectList(_context.Departments, "DepId", "DepName", employee.DepId);
            return View(employee);
        }
        [HttpGet]
        
        public async Task<IActionResult> Edit(int? id)
        {
            if(id==null || _context.Employees == null)
            {
                return NotFound();
            }
            var emp = await _context.Employees.Include(e => e.department).FirstOrDefaultAsync(e => e.Id == id);
            if (emp == null) return NotFound();
            ViewData["DepId"] = new SelectList(_context.Departments,"DepId","DepName", emp.Id);
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Employee employee)
        {
            if(id != null)
            {
                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepId"] = new SelectList(_context.Departments,"DepId","DepName",employee.Id);
            return View(employee) ;
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var emp = _context.Employees.Include(e => e.department).FirstOrDefaultAsync(e => e.Id == id);
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
            var emp = await _context.Employees.FindAsync(id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); 

        }





    }
   
  
}
