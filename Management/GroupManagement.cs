using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using CourseManagement.Models;
using CourseManagement.Utility;

namespace CourseManagement.Management
{
    internal class GroupManagement
    {
        public static void CreateGroup()
        {
            Console.WriteLine("\nCreate a group...\n");

            string groupno = CommonMethods.getString("Group NO: ");
            string category = CommonMethods.getString("Category: ");
            string isonlinestr = CommonMethods.getString("Online/Offline (On/Off): ");

            bool isonline = false;
            if (isonlinestr == "On") isonline = true;

            Group newgroup = new Group(groupno, category, isonline);
            Storage.groups.Add(newgroup);

            Console.WriteLine("\nGroup registration completed...\n");
        }

        public static void ShowAllGroups()
        {
            if (Storage.groups.Count > 0)
            {
                Console.WriteLine("\nAll Groups...");

                for (int i = 0; i < Storage.groups.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. group");
                    Console.WriteLine(
                        $"\n - Group NO: {Storage.groups[i].GroupNO}" +
                        $"\n - Category: {Storage.groups[i].Category}" +
                        $"\n - Online/Offline: {Storage.groups[i].GetStringIsOnline()}" +
                        $"\n - Limit: {Storage.groups[i].Limit}" +
                        $"\n - Current student count: {Storage.groups[i].Students.Count}");
                }
            }
            else
            {
                Console.WriteLine("\nThere is not any registered group in the system...\n");
                return;
            }
        }

        public static void EditGroup()
        {
            if (Storage.groups.Count > 0)
            {
                //Grouplarin siyahisi
                ShowAllGroups();

                //grouplar arasinda secim
                int group = CommonMethods.getInteger("Please select the group above: ");

                if (group <= Storage.groups.Count)
                {
                    MenuUtil.EditGroupMenu();
                    int editmenu = CommonMethods.SelectMenuAbove();

                    if (CommonMethods.CheckMenuRange(editmenu, 1, 3))
                    {
                        //edit etme metodlarina yonlendirme
                        switch (editmenu)
                        {
                            case 1:
                                EditGroup1(group - 1);
                                break;
                            case 2:
                                EditGroup2(group - 1);
                                break;
                            case 3:
                                EditGroup3(group - 1);
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nPlease select the menu between 1 and 4...\n");
                        EditGroup();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nThere is not any registered group in the selected range...Try again...\n");
                    EditGroup();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nThere is not any registered student in the system...\n");
                return;
            }

            //Documentation
            /*
             * Edit group prosesi uc esas prop uzerinde edilir
             * 1) GroupNO
             * 2) Category
             * 3) Isonline
             * 
             * Limit uzerinde edit edilmir
             * Cunki limit avtomatik tehsilin online ya offline olmasina gore teyin edilir Group constructorunda
             */
        }

        #region Edit Group Menu methods
        static void EditGroup1(int groupindex)
        {
            //Edit GroupNo

            Console.WriteLine($"Current GroupNO: {Storage.groups[groupindex].GroupNO}");
            string newgroupno = CommonMethods.getString("New GroupNO: ");

            Storage.groups[groupindex].GroupNO = newgroupno;
            Console.WriteLine($"\nGroupNO changed with {newgroupno}\n");
        }
        static void EditGroup2(int groupindex)
        {
            //Edit category

            Console.WriteLine($"Current GroupNO: {Storage.groups[groupindex].Category}");
            string newcategory = CommonMethods.getString("New Category: ");

            Storage.groups[groupindex].Category = newcategory;
            Console.WriteLine($"\nCategory changed with {newcategory}\n");
        }
        static void EditGroup3(int groupindex)
        {
            //Edit isonline

            string flag = "";
            string flag2 = "";

            Console.WriteLine($"Current IsOnline: {Storage.groups[groupindex].GetStringIsOnline()}");
            string newisonlinestr = CommonMethods.getString("New IsOnline (On/Off): ");

            if (newisonlinestr == "On") //online tehsile kecerse
            {
                Storage.groups[groupindex].IsOnline = true;

                //online tehsile kecerse grupun limitini de artir
                Storage.groups[groupindex].Limit = 15;
            }

            else //offline tehsile kecerse
            {
                Storage.groups[groupindex].IsOnline = false;

                //online tehsile kecerse grupun limitini de azaldir
                Storage.groups[groupindex].Limit = 10;

                //off olarsa isonline false olur
                //yoxlama et: offline a kecende eger groupdaki telebe sayi 10dan cox olarsa
                //ya onlari sil ya da basqa groupa kecir

                if (Storage.groups[groupindex].Students.Count > 10) //telebe sayi 10dan cox olarsa
                {
                    do //telebe sayi grupun standartlarina gelene qeder transfer ve delete emeliyyati tekrar edecek (do loop)
                    {
                        Console.WriteLine("\nThere is not enough empty space in the group...Please select the operation below...");
                        Console.WriteLine(
                            "1. Delete" +
                            "\n2. Transfer");
                        int menu = CommonMethods.SelectMenuAbove();

                        if (CommonMethods.CheckMenuRange(menu, 1, 2))
                        {

                            if (menu == 1) //telebeni silir
                            {
                                do
                                {
                                    Console.WriteLine($"\nAll Students in {Storage.groups[groupindex].GroupNO}\n"); //telebelerin adlarini capa verir
                                    for (int i = 0; i < Storage.groups[groupindex].Students.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {Storage.groups[groupindex].Students[i].Fullname}");
                                    }

                                    int studentindex = CommonMethods.getInteger("Select the student to delete: ");
                                    Console.WriteLine($"\nStudent deleted: {Storage.groups[groupindex].Students[studentindex - 1].Fullname}");
                                    Storage.groups[groupindex].Students.RemoveAt(studentindex - 1);

                                    flag = CommonMethods.getString("\nWant to delete another? (Y/N): ");
                                } while (flag == "Y");
                            }

                            else //telebeni transfer edir
                            {
                                do
                                {
                                    Console.WriteLine($"\nAll Students in {Storage.groups[groupindex].GroupNO}\n"); //telebelerin adlarini capa verir
                                    for (int i = 0; i < Storage.groups[groupindex].Students.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {Storage.groups[groupindex].Students[i].Fullname}");
                                    }

                                    int studentindex = CommonMethods.getInteger("Select the student to transfer: ");

                                    //grouplarin adlarin capa verir
                                    for (int i = 0; i < Storage.groups.Count; i++)
                                    {
                                        Console.WriteLine($"{i + 1}. {Storage.groups[i].GroupNO}");
                                    }

                                    bool flag3 = false; //transfer edilen grupda yer olmasa bir basa davam etsin sorusmadan dongunun basina qayitsin
                                    do
                                    {
                                        //transfer edilecek groupu sorusur
                                        int transfergroup = CommonMethods.getInteger("\nWhich group do you want to transfer to: ");

                                        //transfer edilen group secilenden sonra o groupda yer varmi deye yoxlanilir
                                        //eger group online grupdursa 15 uzerinden yoxlama olacaq
                                        if (Storage.groups[transfergroup - 1].IsOnline == true && Storage.groups[transfergroup - 1].Students.Count < 15)
                                        {
                                            //ilk once yeni groupa elave edir
                                            string prestudentid = Storage.groups[groupindex].Students[studentindex - 1].Id;
                                            string prestudentfullname = Storage.groups[groupindex].Students[studentindex - 1].Fullname;
                                            string prestudentgroupno = Storage.groups[groupindex].Students[studentindex - 1].GroupNo;
                                            string prestudenttype = Storage.groups[groupindex].Students[studentindex - 1].Type;

                                            Storage.groups[transfergroup - 1].Students.Add(new Student(prestudentid, prestudentfullname, prestudentgroupno, prestudenttype)); //yeni grupa elave etdi

                                            flag3 = false;
                                            Console.WriteLine($"\nStudent: {Storage.groups[groupindex].Students[studentindex - 1].Fullname} transfered to group: {Storage.groups[transfergroup - 1].GroupNO}"); //transfer olundugu grupu gosterir

                                            //studenti silir evvelki groupundan
                                            Storage.groups[groupindex].Students.RemoveAt(studentindex - 1);
                                        }
                                        //eger group offline grupdursa 10 uzerinden yoxlama olacaq
                                        else if (Storage.groups[transfergroup - 1].IsOnline == false && Storage.groups[transfergroup - 1].Students.Count < 10)
                                        {
                                            //ilk once yeni groupa elave edir
                                            string prestudentid = Storage.groups[groupindex].Students[studentindex - 1].Id;
                                            string prestudentfullname = Storage.groups[groupindex].Students[studentindex - 1].Fullname;
                                            string prestudentgroupno = Storage.groups[groupindex].Students[studentindex - 1].GroupNo;
                                            string prestudenttype = Storage.groups[groupindex].Students[studentindex - 1].Type;

                                            Storage.groups[transfergroup - 1].Students.Add(new Student(prestudentid, prestudentfullname, prestudentgroupno, prestudenttype)); //yeni grupa elave etdi

                                            flag3 = false;
                                            Console.WriteLine($"\nStudent: {Storage.groups[groupindex].Students[studentindex - 1].Fullname} transfered to group: {Storage.groups[transfergroup - 1].GroupNO}"); //transfer olundugu grupu gosterir

                                            //studenti silir evvelki groupundan
                                            Storage.groups[groupindex].Students.RemoveAt(studentindex - 1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nThere is not any empty space in the selected group...Try another one\n");
                                            flag3 = true;
                                        }
                                    } while (flag3);

                                    flag2 = CommonMethods.getString("\nWant to transfer another student (Y/N): ");
                                } while (flag2 == "Y");
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nPlease select the menu between 1 and 2...Try again...");
                        }
                    } while (Storage.groups[groupindex].Students.Count > 10);
                }
            }

            Console.WriteLine($"\nIsOnline changed with {newisonlinestr}line\n");

            //Documentation
            /*
             * Eger edit isonline secilerse secilen grupla elaqedar menyu qarsimizia cixir:
             *  1) grup online kecsin yoxsa offline-a?
             *      1.1. grup online tehsile kecerse
             *          - secilmis grupun isonline-ni true edir ve limiti 15 edir
             *      1.2. grup offline tehsile kecerse
             *          - iki cur secenek qarsimiza cixir: Telebeni sil ve ya transfer et
             *          1.2.1. Delete
             *          1.2.2. Transfer
             *                  - transfer edilerken iki cur yoxlama olur
             *                  1.2.2.1. Online grupa kecirse limit 15 uzerinden yoxlanilir ok-dirse kecid edilir ve evvelki yerinden silinir
             *                  1.2.2.2. Offline grupa kecirse limit 10 uzerinden yoxlanilir okd-dirse kecid edilir ve evvelki yerinden silinir
             */
        }
        #endregion

        public static void ShowStudentCountInSelectedGroup()
        {
            if (Storage.groups.Count > 0)
            {
                Console.WriteLine("\nAll groups in the system...\n");
                ShowAllGroups();

                //hansi grupun telebe sayisini gostermek isteyir onu sorusur
                int groupindex = CommonMethods.getInteger("\nPlease select the group no to show the student number: ");

                //result
                Console.WriteLine(
                    $"Student count in: {Storage.groups[groupindex].GroupNO}: " +
                    $"{Storage.groups[groupindex].Category} is: " +
                    $"{Storage.groups[groupindex].Students.Count}");
            }
            else
            {
                Console.WriteLine("\nThere is not any registered students in the system...\n");
                return;
            }
        }

    }
}
