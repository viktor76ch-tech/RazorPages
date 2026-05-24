using System.ComponentModel.DataAnnotations;
namespace ContosoUniversity.Models

{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Без фамилии никак...")]
        [StringLength(50)]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Введены недопустимые символы")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Без имени никак...")]
        [StringLength(50)]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Введены недопустимые символы")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата трудоустройства")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Преподаватель")]
        public string FullName { get => $"{LastName}{FirstName}"; }

        // Navigation properties:
        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
