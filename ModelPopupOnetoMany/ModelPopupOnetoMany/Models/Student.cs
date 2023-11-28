using System.Text.Json.Serialization;

namespace ModelPopupOnetoMany.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }

        public string Address { get; set;}
        public DateTime DOB { get; set;}

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
