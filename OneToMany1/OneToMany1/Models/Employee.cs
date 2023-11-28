using System.ComponentModel.DataAnnotations;

namespace OneToMany1.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }

        //foreign Key
        public int Deptid { get; set; }
        public Department Department { get; set; }
    }
}
