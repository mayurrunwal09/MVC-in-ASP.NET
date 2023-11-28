using System.ComponentModel.DataAnnotations;

namespace studcrudoperation1.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
   
        public string Name { get; set; }
   
        public string Description { get; set; }

        public int Age { get; set; }
    }
}
