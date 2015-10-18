using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2.Models
{
    public class Standard
    {
        public Standard()
        {
            this.Students = new HashSet<Student>();
        }

        public int StandardId { get; set; }
        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }

    }
}
