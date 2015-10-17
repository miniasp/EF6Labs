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
                var data = from p in db.Course
                           where p.Title.Contains("Git")
                           select p;

                foreach (var item in data)
                {
                    Console.WriteLine(item.CourseID + "\t" + item.Title);
                }
            }
        }
    }
}
