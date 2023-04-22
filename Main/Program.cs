using CourseManagement.Management;
using CourseManagement.Utility;

namespace CourseManagement.Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tWelcome to Course Management System\n");
            run();
            Console.WriteLine("\n\tProgram ended...");
        }

        public static void run()
        {
            MenuUtil.MainMenu();
            int menu = CommonMethods.SelectMenuAbove();

            if (CommonMethods.CheckMenuRange(menu, 1, 3))
            {
                switch (menu)
                {
                    case 1:
                        RunGroup();
                        run();
                        break;
                    case 2:
                        RunStudent();
                        run();
                        break;
                    case 3:
                        Console.WriteLine("\nExiting the system...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPlease select the menu between 1 and 3");
                run();
                return;
            }
        }

        public static void RunGroup()
        {
            MenuUtil.GroupMenu();
            int menu = CommonMethods.SelectMenuAbove();

            if (CommonMethods.CheckMenuRange(menu, 1, 5))
            {
                switch (menu)
                {
                    case 1:
                        GroupManagement.CreateGroup();
                        RunGroup();
                        break;
                    case 2:
                        GroupManagement.ShowAllGroups();
                        RunGroup();
                        break;
                    case 3:
                        GroupManagement.EditGroup();
                        RunGroup();
                        break;
                    case 4:
                        GroupManagement.ShowStudentCountInSelectedGroup();
                        RunGroup();
                        break;
                    case 5:
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPlease select menu between 1 and 5");
                RunGroup();
                return;
            }
        }

        public static void RunStudent()
        {
            MenuUtil.StudentMenu();
            int menu = CommonMethods.SelectMenuAbove();

            if (CommonMethods.CheckMenuRange(menu, 1, 5))
            {
                switch (menu)
                {
                    case 1:
                        StudentManagement.ShowAllStudents();
                        RunStudent();
                        break;
                    case 2:
                        StudentManagement.AddNewStudent();
                        RunStudent();
                        break;
                    case 3:
                        StudentManagement.EditStudent();
                        RunStudent();
                        break;
                    case 4:
                        StudentManagement.DeleteStudent();
                        RunStudent();
                        break;
                    case 5:
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nPlease select menu between 1 and 5");
                RunStudent();
                return;
            }
        }
    }
}