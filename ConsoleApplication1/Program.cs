using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {
                var data = from p in db.Department
                           select p;

                foreach (var department in data)
                {
                    Console.WriteLine(department.DepartmentID + "\t" + department.Name);

                    //var courses = from q in db.Course
                    //              where q.DepartmentID == department.DepartmentID
                    //              select q;

                    //var courses = department.Course;

                    foreach (var course in department.Course)
                    {
                        Console.WriteLine("\t" + course.CourseID + "\t" + course.Title);
                    }
                }

                Console.WriteLine("--------------------------------------------------------");

                foreach (var c in db.Course)
                {
                    Console.WriteLine(c.CourseID + "\t" + c.Title + "\t" + c.Department.Name);
                }

                Console.WriteLine("--------------------------------------------------------");

                var c1 = new Course()
                {
                    Title = "Test",
                    Credits = 5,
                    DepartmentID=4
                };

                db.Course.Add(c1);
                db.SaveChanges();

                foreach (var c in db.Course)
                {
                    Console.WriteLine(c.CourseID + "\t" + c.Title + "\t" + c.Department.Name);
                }





            }
        }
    }
}
