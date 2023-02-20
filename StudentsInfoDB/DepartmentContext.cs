using Azure;
using Microsoft.EntityFrameworkCore;
using StudentsInfoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsInfoDB
{
    public class DepartmentContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost; Database=StudentsDB; TrustServerCertificate=True; MultiSubnetFailover=True");
        }
    }
}

//Impossible to connect to already made DB on Azure 