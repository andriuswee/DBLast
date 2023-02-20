using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsInfoDB.Entities;


namespace StudentsInfoDB.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
        public Lecture()
        {
        }

        public Lecture(string Name, List<Department> departments)
        {
            this.Id = Id;
            this.Name = Name;
            this.Departments = new List<Department>();

        }


    }
}
