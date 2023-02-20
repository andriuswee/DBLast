using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsInfoDB.Entities;

namespace StudentsInfoDB.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Departament { get; set; }

        public Student()
        {
        }
        public Student(string name, string surname, DateTime dateOfBirth)
        {
            this.Name = name;
            this.Surname = surname;
            this.DateOfBirth = dateOfBirth;

        }
    }
}
