using Employee_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Stored_Procedure.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly MainDBContext _Context;
        public DepartmentController(MainDBContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            List<Department> departments = _Context.SearchDepartment("").ToList();
            return View(departments);
        }
        [HttpPost]
        public IActionResult Index(string  department)
        {
            List<Department> departments = _Context.SearchDepartment(department).ToList();
            return View(departments);
        }
        public IActionResult create()
        {
            return View();
        }
        /* [HttpPost]
         public async Task<IActionResult> Create(Department department) 
         {
             if (ModelState.IsValid)
             {
                 var res = await _Context.AddDepartment(department.DepId, department.DepName);
                 if(res == "Success")
                 {
                     return RedirectToAction("Index");
                 }
                 else
                 {
                     ModelState.AddModelError(string.Empty, "Failed to add department");
                 }
             }
             return View(department);
         }*/
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = await _Context.AddDepartment(department.DepId, department.DepName);
                    if (res == "Success")
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to add the department");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while adding the department: " + ex.Message);
                }
            }
            return View(department);
        }

        public IActionResult  Update(int ? Id)
        {
            if(Id == null)
            {
                return NotFound();

            }
            var res = _Context.Department.Find(Id);
            if(res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult>Update(int Id,[Bind("DepId,DepName")]Department department)
        {
            if(Id != department.DepId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var res = await _Context.UpdateDepartment(department.DepId, department.DepName);
                if (res.StartsWith("Error"))
                {
                    ModelState.AddModelError(string.Empty, res);
                    return View(department);
                }

                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _Context.Department.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _Context.DeleteDepartment(id);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
