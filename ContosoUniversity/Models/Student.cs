using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FistName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //Navigation properties

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
