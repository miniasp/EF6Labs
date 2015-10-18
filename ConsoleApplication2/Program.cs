using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Models;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SchoolContext())
            {
                var std = new Standard() { StandardName = "國小" };

                std.Students.Add(new Student()
                {
                    StudentName = "Will",
                    Weight = 30,
                    Height = 170,
                    DateOfBirth = DateTime.Now
                });

                std.Students.Add(new Student()
                {
                    StudentName = "Will 2",
                    Weight = 50,
                    Height = 170,
                    DateOfBirth = DateTime.Now
                });

                db.Standards.Add(std);

                db.SaveChanges();


                foreach (var item in db.Students)
                {
                    Console.WriteLine(item.StudentName + "\t" + item.Standard.StandardName);
                }

            }
        }
    }
}
