using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Utility
{
    internal class CommonMethods
    {
        public static bool CheckMenuRange(int value, int min, int max)
        {
            if (min <= value && value <= max)
            {
                return true;
            }
            return false;
        }

        public static (int, int) DetermineStudentAddress()
        {
            int row = 1;
            int groupindex = 0;
            int studentindex = 0;
            List<string> students = new List<string>(); //telebeler arasindan secileni axtarib adresini teyin etmek ucun list array

            //butun telebeleri gosterir
            for (int i = 0; i < Storage.groups.Count; i++)
            {
                for (int j = 0; j < Storage.groups[i].Students.Count; j++)
                {
                    Console.WriteLine($"{row}. {Storage.groups[i].Students[j].Fullname}");
                    students.Add(Storage.groups[i].Students[j].Fullname);
                    row++;
                }
            }

            //telebeler arasindan biri secilir
            int student = getInteger("\nSelect the student above: ");

            //secilen telebenin adresini teyin edir
            for (int i = 0; i < Storage.groups.Count; i++)
            {
                for (int j = 0; j < Storage.groups[i].Students.Count; j++)
                {
                    if (Storage.groups[i].Students[j].Fullname == students[student - 1])
                    {
                        groupindex = i;
                        studentindex = j;
                        break;
                    }
                    row++;
                }
            }

            return (groupindex, studentindex);
        }

        public static int SelectMenuAbove()
        {
            Console.Write("Please select the menu above: ");
            return int.Parse(Console.ReadLine());
        }
        public static string getString(string title)
        {
            Console.Write(title);
            return Console.ReadLine();
        }
        public static int getInteger(string title)
        {
            Console.Write(title);
            return int.Parse(Console.ReadLine());
        }
    }
}
