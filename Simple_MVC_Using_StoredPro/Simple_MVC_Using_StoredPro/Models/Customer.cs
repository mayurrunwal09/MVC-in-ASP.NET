using System.ComponentModel.DataAnnotations;

namespace Simple_MVC_Using_StoredPro.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string ContactName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
