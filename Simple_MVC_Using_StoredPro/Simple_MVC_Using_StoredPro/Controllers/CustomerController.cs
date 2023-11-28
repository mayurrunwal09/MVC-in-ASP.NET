using Microsoft.AspNetCore.Mvc;
using Simple_MVC_Using_StoredPro.Models;

namespace Simple_MVC_Using_StoredPro.Controllers
{
    public class CustomerController : Controller
    {
        private readonly MainDBContext _dbContext;
        public CustomerController(MainDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Customer> customers = _dbContext.SearchCustomer("").ToList();
            return View(customers);
        }

        [HttpPost]  
        public IActionResult  Index(string CustomerName)
        {
            List<Customer> customers = _dbContext.SearchCustomer(CustomerName).ToList();
            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var res = await _dbContext.AddCustomer(customer.CustomerID, customer.ContactName, customer.City, customer.Country);

                if (res == "Success")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add the customer. Please check the provided data.");
                }
            }

            return View(customer);
        }

         public IActionResult Update(int? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var employee = _dbContext.Customers.Find(id);
             if (employee == null)
             {
                 return NotFound();
             }

             return View(employee);
         }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,[Bind("CustomerID,ContactName,City,Country")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = await _dbContext.UpdateCustomer(customer.CustomerID, customer.ContactName, customer.City, customer.Country);

                if (result.StartsWith("Error"))
                {
                    ModelState.AddModelError(string.Empty, result);
                    return View(customer);
                }

                return RedirectToAction("Index");
            }
            return View(customer);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _dbContext.Customers.Find(id);
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
            var result = await _dbContext.DeleteCustomer(id);

            if (result.StartsWith("Error"))
            {
                ModelState.AddModelError(string.Empty, result);
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}
