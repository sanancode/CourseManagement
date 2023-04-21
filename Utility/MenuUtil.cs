using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Utility
{
    internal class MenuUtil
    {
        public static void MainMenu()
        {
            Console.WriteLine(
                "\n1. Group Operaions" +
                "\n2. Student Operations" +
                "\n3. Exit the system");
        }

        public static void GroupMenu()
        {
            Console.WriteLine(
                    "\n1. Create a new group" +
                    "\n2. Show all groups" +
                    "\n3. Edit groups" +
                    "\n4. Show students count in seleted group" +
                    "\n5. Back to previous menu");
        }

        public static void EditGroupMenu()
        {
            Console.WriteLine(
                "\n1. GroupNO" +
                "\n2. Category" +
                "\n3. IsOnline");
        }

        public static void StudentMenu()
        {
            Console.WriteLine(
                    "\n1. Show all students" +
                    "\n2. Add new student" +
                    "\n3. Edit student" +
                    "\n4. Delete a student" +
                    "\n5. Back to previous menu");
        }

        public static void EditStudentGroup()
        {
            Console.WriteLine(
                "\n1. ID" +
                "\n2. Fullname" +
                "\n3. Type" +
                "\n4. GroupNo");
        }

    }
}
