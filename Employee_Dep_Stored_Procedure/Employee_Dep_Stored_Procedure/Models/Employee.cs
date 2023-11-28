using System.ComponentModel.DataAnnotations;

namespace Employee_Dep_Stored_Procedure.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int Age { get; set; }
        public string MobileNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public int DepId { get; set; }
        public Department Department { get; set; }
    }
}
