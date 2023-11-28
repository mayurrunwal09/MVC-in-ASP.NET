using System.ComponentModel.DataAnnotations;

namespace Model_Popup_Using_StoredProcedure.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string City { get; set; }
        public string MobileNo { get; set; }
        public int DepId { get; set; }
        public Department Department { get; set; }
    }
}
