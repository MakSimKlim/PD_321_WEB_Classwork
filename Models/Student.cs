using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required]// обязательное поле (NOT NULL)
        [StringLength(50, ErrorMessage = "Too many symbols for LastName")]
        [RegularExpression(@"^[A-ZА-Я]+[a-zа-я]*$")]
        [Display(Name = "Фамилия")]//Если свойства отличаются от имени поля в таблице базы данных, то их можно сопоставить при помощи атрибута Column
        public string LastName { get; set; }
        [Required]// обязательное поле (NOT NULL)
        [StringLength(50, ErrorMessage = "Too many symbols for FirstName")]
        [RegularExpression(@"^[A-ZА-Я]+[a-zа-я]*$")]
        //[Column("first_name")]//этот атрибут позволяет сопоставить свойство определенному полю в таблице
        [Display(Name = "Имя")]//Если свойства отличаются от имени поля в таблице базы данных, то их можно сопоставить при помощи атрибута Column
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата поступления")]
        public DateTime EnrollmentDate { get; set; }
        [Display(Name = "Полное имя")]
        public string FullName // это поле не хранится в базе (Calculated property)
        {
            get => $"{LastName} {FirstName}";
        }

        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
