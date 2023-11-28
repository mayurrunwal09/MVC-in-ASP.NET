using System.ComponentModel.DataAnnotations;

namespace Model_Popup_Using_StoredProcedure.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get; set;}
        public ICollection<Employee> Employees { get; set; }                              
    }
}
