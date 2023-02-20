using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsInfoDB.Entities;

namespace StudentsInfoDB.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<Student> Students { get; set; }
        public List<Lecture> Lectures { get; set; }

        public Department()
        {
        }

        public Department(int id, List<Student> students)
        {
            this.Id = Id;
            this.Students = new List<Student>();
        }

        public Department(string name, string city, string address)
        {
            this.Id = Id;
            this.Name = name;
            this.City = city;
            this.Address = address;
            this.Students = new List<Student>();
            this.Lectures = new List<Lecture>();
        }

    }
}
