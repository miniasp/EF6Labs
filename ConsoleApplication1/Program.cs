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
                //Day1_Practics(db);

                var cc = db.Course.Find(1);
                //var cc = db.Course.Create();
                //db.Course.AsNoTracking()

                cc.Credits = 100;

                var ce = db.Entry(cc);

                Console.WriteLine(ce.State);
                if (ce.State == System.Data.Entity.EntityState.Modified)
                {
                    Console.WriteLine("Curr: " + cc.Credits);
                    Console.WriteLine("Orig: " + ce.OriginalValues.GetValue<int>("Credits"));
                }


            }
        }

        private static void Day1_Practics(ContosoUniversityEntities db)
        {
            // db.Configuration.ProxyCreationEnabled = false;

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

            //var c1 = new Course()
            //{
            //    Title = "Test",
            //    Credits = 5,
            //    DepartmentID=4
            //};

            //db.Course.Add(c1);
            //db.SaveChanges();

            //foreach (var c in db.Course.AsNoTracking())
            //{
            //    Console.WriteLine(c.CourseID + "\t" + c.Title + "\t" + c.Department.Name);
            //}


            //Console.WriteLine("--------------------------------------------------------");

            var c2 = db.Course.Find(7);
            c2.Instructors.Add(db.Person.Find(5));
            db.SaveChanges();


            // 取得 SQL Server 伺服器時間的方法
            DateTime dt = db.Database.SqlQuery<DateTime>("select getdate()").First();


            var sql = @"SELECT Course.CourseID, Course.Title, Course.Credits, Department.Name AS DepartmentName FROM Course INNER JOIN Department ON Course.DepartmentID = Department.DepartmentID";

            var data2 = db.Database.SqlQuery<Course2>(sql);

            foreach (var item in data2)
            {
                Console.WriteLine(item.CourseID + "\t" + item.Title + "\t" + item.DepartmentName);
            }

            Console.WriteLine("--------------------------------------------------------");
        }
    }
}
