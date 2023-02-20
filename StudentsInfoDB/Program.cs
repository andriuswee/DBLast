using System;
using Azure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StudentsInfoDB.Entities;
using StudentsInfoDB;

//Functions:
//1. Create dept / add students and lectures.
//2. Add students and lectures to existing dept.
//3. Create newlecture to department.
//4. Create newstudent , add to existing dept and add existing lectures.
//5. Transfer student to another dept.
//6. Show all students of dept.
//7. Show all lectures of dept.
//8. Show lectures of student.

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Students Database");

        var dbContext = new DepartmentContext();

        //1.

        void CreateDepartment()
        {
            Console.WriteLine("1. Create department:");
            Console.WriteLine("Enter name:");
            string DepartmentName = Console.ReadLine();
            Console.WriteLine("Enter departament city");
            string DepartmentCity = Console.ReadLine();
            Console.WriteLine("Enter address");
            string DepartmentAddress = Console.ReadLine();

            var department = new Department { Name = DepartmentName, City = DepartmentCity, Address = DepartmentAddress };
            department.Lectures = new List<Lecture>();
            department.Students = new List<Student>();

            Console.WriteLine("2. Create lecture:");
            Console.WriteLine("Enter lecture name:");
            string LectureName = Console.ReadLine();
            department.Lectures.Add(new Lecture { Name = LectureName });

            Console.WriteLine("3. Create student");
            Console.WriteLine("Enter name:");
            string StudentName = Console.ReadLine();
            Console.WriteLine("Enter surname:");
            string StudentSurname = Console.ReadLine();
            Console.WriteLine("Enter date of birth):");
            string StudentDate = Console.ReadLine();
            department.Students.Add(new Student() { Name = StudentName, Surname = StudentSurname, DateOfBirth = new DateTime(2000, 01, 01) });

            dbContext.Departments.Add(department);
            dbContext.SaveChanges();

        }

        //2.

        void CreateStudentInExistingDepartament()
        {
            Console.WriteLine("Show Departament list");
            var consoleresult = dbContext.Departments;
            Console.WriteLine("* Department * Department Name * City * Address");
            foreach (var item in consoleresult)
            {
                Console.Write(item.Id + ", ");
                Console.Write(item.Name + ", ");
                Console.Write(item.City + ", ");
                Console.Write(item.Address);
                Console.WriteLine();
            }
            Console.WriteLine("Choose Department ID:");
            int DepartmentId = int.Parse(Console.ReadLine());

            Console.WriteLine("Create a student to departament exists");
            Console.WriteLine("Enter name:");
            string StudentName = Console.ReadLine();
            Console.WriteLine("Enter surname:");
            string StudentSurname = Console.ReadLine();
            Console.WriteLine("Enter date of birth:");
            string StudentDate = Console.ReadLine();

            dbContext.AddRange
                (
                  new Student() { Name = StudentName, Surname = StudentSurname, DateOfBirth = new DateTime(2000, 01, 01), DepartmentId = DepartmentId }
                );
            dbContext.SaveChanges();
        }

        //3.

        void CreateLecturesInAddedDepartment()
        {
            Console.WriteLine("Create new department:");
            Console.WriteLine("Enter name:");
            string DepartmentName = Console.ReadLine();
            Console.WriteLine("Enter city:");
            string DepartmentCity = Console.ReadLine();
            Console.WriteLine("Enter address:");
            string DepartmentAddress = Console.ReadLine();

            var department = new Department { Name = DepartmentName, City = DepartmentCity, Address = DepartmentAddress };

            Console.WriteLine("Enter lecture name:");
            string LectureName = Console.ReadLine();
            dbContext.AddRange
                        (
                            new Lecture { Name = LectureName, Departments = new List<Department> { department } }
                        );
            dbContext.SaveChanges();
        }

        //6.

        void CreateLecturesInDepartment()
        {
            Console.WriteLine("List Departments");
            var consoleResult = dbContext.Departments;
            Console.WriteLine("* ID * Department Name * City * Address");
            foreach (var item in consoleResult)
            {
                Console.Write(item.Id + ",");
                Console.Write(item.Name + ",");
                Console.Write(item.City + ",");
                Console.Write(item.Address);
                Console.WriteLine();
            }

            Console.WriteLine("Choose department");
            int DepartmentId = int.Parse(Console.ReadLine());

            var resultDepartmentId = dbContext.Departments.Include(x => x.Lectures).Where(d => d.Id == DepartmentId).FirstOrDefault();
            Console.WriteLine("Enter lecture Name");
            string LectureName = Console.ReadLine();
            dbContext.AddRange
                       (
                            new Lecture { Name = LectureName, Departments = new List<Department> { resultDepartmentId } }
                       );
            dbContext.SaveChanges();
        }

        //5.

        void TransferStudent()
        {

            Console.WriteLine("List departments:");
            var consoleresult = dbContext.Departments;
            Console.WriteLine("* ID * Name * City * Address");

            foreach (var item in consoleresult)
            {
                Console.Write(item.Id + ",");
                Console.Write(item.Name + ",");
                Console.Write(item.City + ",");
                Console.Write(item.Address);
                Console.WriteLine();
            }

            Console.WriteLine("List student in department, choose student");
            int DepartmentId = int.Parse(Console.ReadLine());

            var result = dbContext.Departments.Include(d => d.Students).Where(x => x.Id == DepartmentId).FirstOrDefault();

            var Students = result.Students;
            Console.WriteLine("* ID * Name * Surname * Date of Birth *");

            foreach (var item in Students)
            {
                Console.Write(item.Id + ", ");
                Console.Write(item.Name + ", ");
                Console.Write(item.Surname + " ");
                Console.WriteLine(item.DateOfBirth);
                Console.WriteLine();
            }
            Console.WriteLine("-----/////-----/////-----");

            Console.WriteLine("Transfer student to department choose student");
            int StudentId = int.Parse(Console.ReadLine());

            var Student = dbContext.Students.Where(n => n.DepartmentId == DepartmentId).Where(na => na.Id == StudentId).Single();

            Console.Write(Student.Id + ", ");
            Console.Write(Student.Name + " ");
            Console.Write(Student.Surname + ", ");
            Console.Write(Student.DateOfBirth);
            Console.WriteLine();

            Console.WriteLine("Add departament for student");
            int NewDepartmentId = int.Parse(Console.ReadLine());

            dbContext.AddRange
               (
                 new Student() { Name = Student.Name, Surname = Student.Surname, DateOfBirth = new DateTime(2000, 01, 16), DepartmentId = NewDepartmentId }
               );

            var studentToRemove = dbContext.Students.Where(n => n.DepartmentId == DepartmentId).Where(na => na.Id == StudentId).Single();
            if (studentToRemove != null)
            {
                dbContext.Students.Remove(studentToRemove);
            }

            dbContext.SaveChanges();

            Console.WriteLine("List students department number:  " + NewDepartmentId);

            var resultNew = dbContext.Departments.Include(d => d.Students).Where(x => x.Id == DepartmentId).FirstOrDefault();

            var NewStudent = result.Students;
            Console.WriteLine("* ID * Name * Surname * Date of Birth *");

            foreach (var item in NewStudent)
            {
                Console.Write(item.Id + ", ");
                Console.Write(item.Name + " ");
                Console.Write(item.Surname + ", ");
                Console.WriteLine(item.DateOfBirth);
                Console.WriteLine();
            }
            Console.WriteLine("-----/////-----/////-----");
        }

        void ShowStudentsOfDepartament()
        {
            Console.WriteLine("List of departments");
            var consoleresult = dbContext.Departments;
            Console.WriteLine("* No * ID * Name * City * Address");

            foreach (var item in consoleresult)
            {
                Console.Write(item.Id + ", ");
                Console.Write(item.Name + ", ");
                Console.Write(item.City + ", ");
                Console.Write(item.Address);
                Console.WriteLine();
            }

            Console.WriteLine("List students in department");
            int DepartmentId = int.Parse(Console.ReadLine());

            var result = dbContext.Departments.Include(d => d.Students).Where(x => x.Id == DepartmentId).FirstOrDefault();
            var students = result.Students;

            Console.WriteLine("* ID * Name * Surname * Date of Birth *");

            foreach (var item in students)
            {
                Console.Write(item.Name + " ");
                Console.Write(item.Surname + ", ");
                Console.WriteLine(item.DateOfBirth);
                Console.WriteLine();
            }
            Console.WriteLine("-----/////-----/////-----");
        }



        void DepartmentLectures()
        {
            Console.WriteLine("List lectures in department");
            int Department = int.Parse(Console.ReadLine());
            var consoleresult = dbContext.Departments.Include(x => x.Lectures).Where(d => d.Id == Department).FirstOrDefault();
            var DepartmentLectures = consoleresult.Lectures;

            foreach (var item in DepartmentLectures)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("-----/////-----/////-----");
        }

        void StudentLectures()
        {
            Console.WriteLine("Student List:");
            var consoleresult = dbContext.Students;
            Console.WriteLine("| ID | Name | Surname |");
            foreach (var item in consoleresult)
            {
                Console.Write(item.Id + " | ");
                Console.Write(item.Name + " | ");
                Console.Write(item.Surname + " |  ");
                Console.WriteLine();
            }

            Console.WriteLine("Choose student to show lectures:");
            int StudentId = int.Parse(Console.ReadLine());
            var StudentDepartment = dbContext.Students.Where(x => x.Id == StudentId).Select(x => x.DepartmentId).FirstOrDefault();
            var result = dbContext.Departments.Include(d => d.Lectures).Include(d => d.Students).Where(x => x.Id == StudentDepartment).FirstOrDefault();

            var lectures = result.Lectures;

            foreach (var item in lectures)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("-----/////-----/////-----");

        }
    }
}