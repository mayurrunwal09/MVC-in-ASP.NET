using System.ComponentModel.DataAnnotations;

namespace Simple_MVC_Employee.Models
{
    public class Employee
    {
        [Key] 
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public DateTime DOB { get; set; }
    }
}
