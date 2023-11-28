using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Demo.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {



        private readonly MainDBContext _dbContext;
        public EmployeeController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var emp = await _dbContext.Employees.Include(d => d.Department).Include(d=>d.Country).Include(d => d.State).Include(c => c.City).ToListAsync();
            return View(emp);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                ViewBag.Countries = _dbContext.Countries.ToList();
                ViewBag.Statements = _dbContext.Statements .ToList();

                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                return View(new Employee());
            }
            else
            {
                var emp = await _dbContext.Employees.FirstOrDefaultAsync(d => d.EmpId == id);
                if (emp == null)
                {
                    return NotFound();
                }

                ViewBag.Countries = _dbContext.Countries.ToList();
                ViewBag.Statements = _dbContext.Statements.ToList();

                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                return View(emp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("EmpId", "EmpName", "Mobno", "Gender", "Salary", "DepId", "CountryId", "StateId", "CityId")] Employee employee)
        {
            if (id == 0)
            {
                ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName", employee.CountryId);
                ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName", employee.StateId);
                ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName", employee.CityId);
                ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName", employee.DepId);
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                try
                {

                    ViewData["CountryId"] = new SelectList(_dbContext.Countries, "CountryId", "CountryName");
                    ViewData["StateId"] = new SelectList(_dbContext.Statements, "StateId", "StateName");
                    ViewData["CityId"] = new SelectList(_dbContext.Cities, "CityId", "CityName");
                    ViewData["DepId"] = new SelectList(_dbContext.Departments, "DepId", "DepName");
                    _dbContext.Update(employee);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExist(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _dbContext.Employees.ToList()) });

            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", employee) });

        }

        private bool EmployeeExist(int empId)
        {
            throw new NotImplementedException();
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var emp = await _dbContext.Employees.Include(d => d.Department).Include(d => d.Country).Include(c => c.State).Include(c => c.City).FirstOrDefaultAsync(x => x.EmpId == id);
            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", emp) });


        }

        [HttpGet]
        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = _dbContext.Cities.Where(c => c.StateId == stateId).ToList();
            return Json(cities);
        }

        [HttpGet]
        public JsonResult GetStatesByCountry(int countryId)
        {
            var states = _dbContext.Statements.Where(s => s.CountryId == countryId).ToList();
            return Json(states);
        }

       

    }
}
