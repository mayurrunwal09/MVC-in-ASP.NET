using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ModelPopupOnetoMany.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public DateTime DateOfEnrollment { get; set; }

        public int StudentId { get; set; }  
        public int CourseId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
