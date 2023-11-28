using AspNetCore;
using Concurrency_Handle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Concurrency_Handle.Controllers
{
    public class DepartmentController : Controller
    {
        private MainDBContext _dbContext;

        public DepartmentController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var department = await _dbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,Department department)
        {
            if (id != null)
            {
                var dep = _dbContext.Departments.Add(department);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            

            return View(department);
        }



        /*   [HttpGet]
           public async Task<IActionResult> Edit(int id)
           {
               if (id == 0)
               {
                   return NotFound();
               }

               var department = await _dbContext.Departments.FindAsync(id);

               if (department == null)
               {
                   return NotFound();
               }

               return View(department);
           }

           [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> Edit(int id, Department department)
           {
               if (id != null)
               {
                   var dep = _dbContext.Departments.Update(department);
                   await _dbContext.SaveChangesAsync();
                   return RedirectToAction("Index");
               } 
               return View(department);
           }*/

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Department  product = _dbContext.Departments .Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department product)
        {
            try
            {
                _dbContext.Entry(product).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var databaseValues = (Department)entry.GetDatabaseValues().ToObject();


                entry.OriginalValues.SetValues(databaseValues);


                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, byte[] Timestamp)
        {
            var department = await _dbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            if (!Timestamp.SequenceEqual(department.Timestamp))
            {
                ModelState.AddModelError(string.Empty, "The record has been modified by another user.");
                return View(department);
            }

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }


    }
}
