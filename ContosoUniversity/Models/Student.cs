using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }

        //Colculate properties
        public string FullName 
        {
            get => $"{LastName} {FirstName}";
        }
        //Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
