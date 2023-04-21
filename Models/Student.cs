using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    internal class Student
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
        public string GroupNo { get; set; }
        public string Type { get; set; }

        public Student(string id, string fullname, string groupno, string type)
        {
            Id = id;
            Fullname = fullname;
            GroupNo = groupno;
            Type = type;
        }
    }
}
