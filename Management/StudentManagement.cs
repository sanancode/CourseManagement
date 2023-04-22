using CourseManagement.Main;
using CourseManagement.Models;
using CourseManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CourseManagement.Management
{
    internal class StudentManagement
    {
        public static void ShowAllStudents()
        {
            //her hansi bir qrupda telebe olub olmadigini yoxlayir
            bool flag = false;
            for (int i = 0; i < Storage.groups.Count; i++)
            {
                if (Storage.groups[i].Students.Count > 0) flag = true;
            }

            //esas proses
            if (flag)
            {
                Console.WriteLine("\nAll students in the system...\n");
                int row = 1; //sira nomresi

                for (int i = 0; i < Storage.groups.Count; i++)
                {
                    for (int j = 0; j < Storage.groups[i].Students.Count; j++)
                    {
                        //Telebenin melumatlarini capa verir
                        Console.WriteLine($"\n{row}. {Storage.groups[i].Students[j].Fullname}");
                        Console.WriteLine($" - ID: {Storage.groups[i].Students[j].Id}");
                        Console.WriteLine($" - GroupNo: {Storage.groups[i].Students[j].GroupNo}");
                        Console.WriteLine($" - Type: {Storage.groups[i].Students[j].Type}");
                        Console.WriteLine($" - IsOnline: {Storage.groups[i].GetStringIsOnline()}");
                        Console.WriteLine($" - Category: {Storage.groups[i].Category}");

                        row++;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nThere is not any registered students in the system...\n");
                return;
            }
        }

        public static void AddNewStudent()
        {

            Console.WriteLine("\n\tStudent Registration...\n");
            bool flag = true;
            bool flag2 = false; //group indexini iki defe capa vermemek ucun

            //registrate olunmus grup varmi deye yoxla
            if (Storage.groups.Count > 0)
            {
                //get all infos from console
                string studentid = CommonMethods.getString("Student id: ");
                string fullname = CommonMethods.getString("Student fullname: ");
                string type = CommonMethods.getString("Type: ");

                //groupn no
                GroupManagement.ShowAllGroups(); //-ilk once butun grouplari gosterir
                int groupindex = CommonMethods.getInteger("\nPlease select the group index: ");

                if (CommonMethods.CheckMenuRange(groupindex, 1, Storage.groups.Count))
                {
                    do
                    {
                        if (flag2)
                        {
                            groupindex = CommonMethods.getInteger("\nPlease select the group index: ");
                        }

                        string groupno = Storage.groups[groupindex - 1].GroupNO;

                        //grupda yer varmi deye yoxlamalidir
                        if (Storage.groups[groupindex - 1].Students.Count < Storage.groups[groupindex - 1].Limit)
                        {
                            Storage.groups[groupindex - 1].Students.Add(new Models.Student(studentid, fullname, groupno, type));
                            Console.WriteLine("\nStudent registration completed...");
                            Console.WriteLine($"{fullname} added to {groupno}\n");
                            flag = false;
                        }
                        else
                        {
                            Console.WriteLine("\nGroup is full...Please try another one...\n");
                            flag2 = true;
                        }
                    } while (flag);
                }
                else
                {
                    Console.WriteLine($"\nPlease select the group number between 1 and {Storage.groups.Count}\n");
                    AddNewStudent();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nThere is not any registered group in the system\n");
                return;
            }
        }

        public static void EditStudent()
        {
            Console.WriteLine("\nAll students...\n");

            if (Storage.groups.Count > 0)
            {
                //Telebenin adresi teyin edilir
                var (groupindex, studentindex) = CommonMethods.DetermineStudentAddress();

                //menudan bolme secilir
                MenuUtil.EditStudentGroup();
                int menu = CommonMethods.SelectMenuAbove();

                //edit menyusunun alt menyusu secilir
                if (CommonMethods.CheckMenuRange(menu, 1, 4))
                {
                    switch (menu)
                    {
                        case 1:
                            EditStudent1(groupindex, studentindex);
                            break;
                        case 2:
                            EditStudent2(groupindex, studentindex);
                            break;
                        case 3:
                            EditStudent3(groupindex, studentindex);
                            break;
                        case 4:
                            EditStudent4(groupindex, studentindex);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nPlease select the menu between 1 and 4\n");
                    EditStudent();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nThere is not any registered students in the system...\n");
                return;
            }
        }

        #region Edit Group Menu methods
        static void EditStudent1(int groupindex, int studentindex)
        {
            //edit id number

            Console.WriteLine($"\nCurrent ID: {Storage.groups[groupindex].Students[studentindex].Id}");
            string newstudentid = CommonMethods.getString("Please enter the new ID: ");

            Storage.groups[groupindex].Students[studentindex].Id = newstudentid;
            Console.WriteLine($"\nPrevious ID number changed to {Storage.groups[groupindex].Students[studentindex].Id}");
        }
        static void EditStudent2(int groupindex, int studentindex)
        {
            //edit fullname

            Console.WriteLine($"Current fullname: {Storage.groups[groupindex].Students[studentindex].Fullname}");
            string newfullname = CommonMethods.getString("Please enter the new name: ");

            Storage.groups[groupindex].Students[studentindex].Fullname = newfullname;
            Console.WriteLine($"\nFullname changed to {Storage.groups[groupindex].Students[studentindex].Fullname}");
        }
        static void EditStudent3(int groupindex, int studentindex)
        {
            //edit type

            Console.WriteLine($"Current type: {Storage.groups[groupindex].Students[studentindex].Type}");
            string newtype = CommonMethods.getString("Please enter the new type: ");

            Storage.groups[groupindex].Students[studentindex].Type = newtype;
            Console.WriteLine($"\nType changed to {Storage.groups[groupindex].Students[studentindex].Type}");
        }
        static void EditStudent4(int groupindex, int studentindex)
        {
            //group no deyismesi demek groupunun deyismesi demekdir

            //ilk once grouplar gosterilir
            GroupManagement.ShowAllGroups();

            //grouplar arasindan hara elave edileceyini secilir
            int group = CommonMethods.getInteger("\nPlease select the group above: ");

            if (Storage.groups[group - 1].IsOnline == true && Storage.groups[group - 1].Students.Count < 15)
            {
                Storage.groups[groupindex].Students[studentindex].GroupNo = Storage.groups[group - 1].GroupNO;
                Console.WriteLine($"\n{Storage.groups[groupindex].Students[studentindex].Fullname}'s group changed to {Storage.groups[group - 1].GroupNO}");
            }
            else if (Storage.groups[group - 1].IsOnline == false && Storage.groups[group - 1].Students.Count < 10)
            {
                Storage.groups[groupindex].Students[studentindex].GroupNo = Storage.groups[group - 1].GroupNO;
                Console.WriteLine($"\n{Storage.groups[groupindex].Students[studentindex].Fullname}'s group changed to {Storage.groups[group - 1].GroupNO}");
            }
            else
            {
                Console.WriteLine("\nThere is not empty space in the selected group..Try another one...\n");
                EditStudent4(groupindex, studentindex);
                return;
            }
        }
        #endregion

        public static void DeleteStudent()
        {
            if (Storage.groups.Count > 0)
            {
                //Telebenin adresi teyin edilir
                var (groupindex, studentindex) = CommonMethods.DetermineStudentAddress();

                Console.WriteLine($"\nStudent: {Storage.groups[groupindex].Students[studentindex].Fullname} deleted...\n");
                Storage.groups[groupindex].Students.RemoveAt(studentindex);
            }
            else
            {
                Console.WriteLine("\nThere is not any registered students in the system...\n");
                return;
            }
        }
    }
}
