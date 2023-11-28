using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studcrudoperation1.Models;

namespace studcrudoperation1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MainDbContext _context;
        public StudentsController(MainDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Students != null ?
                View(await _context.Students.ToListAsync()) :
                Problem("Entity set 'MainDBContext.Students' is null");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int?id)
        {
            if(id != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(x=>x.Id == id);
                return View(student);
            }
            return View("Index");
        }
        [HttpGet]   
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Student stud,int id)
        {
            if(id!=null)
            {
                _context.Add(stud);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stud);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var stud = await _context.Students.FindAsync(id);
            if(id==null)
            {
                return NotFound();
            }
            return View(stud);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("id,Name,Description,Age")]Student student)
        {
            if (id == null)
            {
                return NotFound();
            }
           if(ModelState.IsValid)
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
           return View(student);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
                return View(student);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if(student != null)
            {
                _context.Students.Remove(student);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
