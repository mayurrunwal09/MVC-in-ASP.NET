using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelPopupOnetoMany.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }   
        public string CourseName { get; set; }
        public string Instructor { get; set; }  
        public double Credit { get; set; }
        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
