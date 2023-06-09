﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    internal class Group
    {
        public string GroupNO { get; set; }
        public string Category { get; set; }
        public bool IsOnline { get; set; }
        public int Limit { get; set; }
        public List<Student> Students { get; set; }

        public Group(string groupno, string category,
            bool isonline)
        {
            GroupNO = groupno;
            Category = category;
            IsOnline = isonline;

            if (IsOnline == true)
            {
                Limit = 15;
            }
            else
            {
                Limit = 10;
            }

            Students = new List<Student>(); //initialize
        }

        public string GetStringIsOnline()
        {
            if (IsOnline == true)
            {
                return "Online";
            }
            return "Offline";
        }
    }
}
