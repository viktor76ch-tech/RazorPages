using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Фамилия - это обязательное поле")]
        [StringLength(50, ErrorMessage = "Превышено максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Фамилия может включать себя только символы Русского и Латинского алфавита, а также должно быть с заглавной буквы")]
        [Display(Name ="Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имя - это обязательное поле")]
        [StringLength(50,ErrorMessage= "Превышено максимальное количество символов")]
        [RegularExpression(@"^[A-ZА-Я][a-zа-я]*$", ErrorMessage = "Имя может включать себя только символы Русского и Латинского алфавита, а также должно быть с заглавной буквы")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата поступления")]
        public DateTime EnrollmentDate { get; set; }

        //Colculate properties
        [Display(Name = "Студент")]
        public string FullName 
        {
            get => $"{LastName} {FirstName}";
        }
        //Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
