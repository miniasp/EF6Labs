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
                var data = db.GetCourse("%Git%");

                foreach (var item in data)
                {
                    Console.WriteLine(item.DepartmentName + "\t" + item.Title);
                }
            }
        }

        private static void 離線模式範例()
        {
            Course c;

            using (var db = new ContosoUniversityEntities())
            {
                c = db.Course.Find(1);

                c.Credits = 40;

                Console.WriteLine(db.Entry(c).State);
            }

            using (var db = new ContosoUniversityEntities())
            {
                db.Course.Attach(c);
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;

                Console.WriteLine(db.Entry(c).State);

                db.SaveChanges();
            }

            Console.WriteLine(c.Credits);

            using (var db = new ContosoUniversityEntities())
            {
                c = db.Course.Find(1);

                Console.WriteLine(c.Credits);


                //db.Course.Remove(db.Course.Find(22));

                //db.Entry(new Course() { CourseID = 22 }).State = System.Data.Entity.EntityState.Deleted;
                //db.SaveChanges();
            }
        }

        private static void Day2_DbEntityEntry()
        {
            using (var db = new ContosoUniversityEntities())
            {
                //Day1_Practics(db);

                var cc = db.Course.Find(1);
                //var cc = db.Course.Create();
                //db.Course.AsNoTracking()

                cc.Credits++;

                var ce = db.Entry(cc);

                Console.WriteLine(ce.State);
                if (ce.State == System.Data.Entity.EntityState.Modified)
                {
                    Console.WriteLine("Curr: " + cc.Credits);
                    Console.WriteLine("Orig: " + ce.OriginalValues.GetValue<int>("Credits"));

                    //cc.ModifiedOn = DateTime.Now;
                    //ce.CurrentValues.SetValues(new { ModifiedOn = DateTime.Now });
                }

                //db.SaveChanges();

                //db.Course.Remove(cc);
                ////ce.State = System.Data.Entity.EntityState.Deleted;

                //cc.Credits = 50;

                //Console.WriteLine(ce.State);
                //if (ce.State == System.Data.Entity.EntityState.Deleted)
                //{
                //    Console.WriteLine("Curr: " + cc.Credits);
                //    Console.WriteLine("Orig: " + ce.OriginalValues.GetValue<int>("Credits"));
                //}

                //ce.Reload();

                var ccc = new Course()
                {
                    Title = "EF6",
                    Credits = 1,
                    Department = db.Department.Find(1)
                };

                db.Course.Add(ccc);

                Console.WriteLine("Course ID: {0}", ccc.CourseID);

                db.SaveChanges();

                Console.WriteLine("Course ID: {0}", ccc.CourseID);

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
