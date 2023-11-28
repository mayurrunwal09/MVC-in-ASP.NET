using Employee_Stored_Procedure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Stored_Procedure.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MainDBContext _dbContext;
        public EmployeeController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
