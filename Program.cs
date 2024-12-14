using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Shared;
using static System.Net.Mime.MediaTypeNames;

namespace _3_1_Console
{
    public class ConsoleView : Shared.IView
    {
        public event Action<EventArgs> AddDataEvent;
        public event Action<int> DeleteDataEvent;
        public event Action<int, string, string, string> EditDataEvent;
        string studentsLine = string.Empty; 
        Dictionary<string, int> dictionaryForGystogram = new Dictionary<string, int>(); 
        public void RedrawView(IEnumerable<StudentEventArgs> data)
        {
            string s = string.Empty;
            foreach (var item in data)
            {
                s += item.ID.ToString() + "|" + item.Name + "|" + item.Group + "|" + item.Speciality + "\n";
            }
            studentsLine = s;

            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            for (int i = 0; i < data.Count(); i++)
            {
                if (keyValuePairs.ContainsKey(data.ToList()[i].Speciality))
                {
                    keyValuePairs[data.ToList()[i].Speciality]++;
                }
                else
                {
                    keyValuePairs.Add(data.ToList()[i].Speciality, 1);
                }
            }
            dictionaryForGystogram = keyValuePairs;
        }
        /// <summary>
        /// Проверка на одинаковое имя студента
        /// </summary>
        /// <param name="name">Имя студента, которого проверяем</param>
        /// <returns>True - если имя студента совпало с другим именем студента. False - если имя студента уникально</returns>
        public bool IsSameName(string name)
        {
            bool result = false;
            string[] students = studentsLine.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string stud in students)
            {
                if (stud == name)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }
        public void Run()
        {
            string control_string = "abcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхчцшщъыьэюя";
            string numbers = "1234567890";
            string control_group = control_string + numbers;

            Console.WriteLine("Привет! Это лайт-версия приложения 'Деканат-Про'!");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Функционал программы:\n" +
                "1. Добавить студента;\n" +
                "2. Удалить студента;\n" +
                "3. Изменить студента;\n" +
                "4. Вывести гисторамму;\n" +
                "5. Показать всех студентов.\n------------------------------");
                string a = Console.ReadLine();
                bool result1 = false;
                bool result2 = false;
                bool result3 = false;
                bool Major_result;
                if (int.TryParse(a, out int number))
                {
                    switch (number)
                    {
                        case 1:
                            Console.WriteLine("Введите имя, фамилию и отчество студента:");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите группу студента:");
                            string group = Console.ReadLine();
                            Console.WriteLine("Введите специальность студента:");
                            string speciality = Console.ReadLine();
                            for (int i = 0; i < control_string.Length; i++)
                            {
                                result1 = result1 || name.ToLower().Contains(control_string[i]);
                            }
                            for (int i = 0; i < control_group.Length; i++)
                            {
                                result2 = result2 || group.ToLower().Contains(control_group[i]);
                            }
                            for (int i = 0; i < control_string.Length; i++)
                            {
                                result3 = result3 || speciality.ToLower().Contains(control_string[i]);
                            }
                            Major_result = result1 && result2 && result3;
                            if (Major_result == true)
                            {
                                if (IsSameName(name))
                                {
                                    Console.WriteLine("Вы пытаетесь добавить студента-клона.\nКлонирование в нашей стране пока не разрешено.");
                                }
                                else
                                {
                                    StudentEventArgs student = new StudentEventArgs
                                    {
                                        Name = name,
                                        Group = group,
                                        Speciality = speciality
                                    };
                                    AddDataEvent.Invoke(student);
                                    Console.WriteLine("Student was added successfuly!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect line!");
                            }
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                        case 2:
                            if (studentsLine == string.Empty)
                            {
                                Console.WriteLine("Удалять некого...");
                                Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Выберите ID студента, которого надо отчислить:");
                            Console.WriteLine(studentsLine);
                            string ID = Console.ReadLine();
                            if (int.TryParse(ID, out int id_remove))
                            {
                                int length1 = studentsLine.Split('\n').Length;
                                DeleteDataEvent?.Invoke(id_remove);
                                int length2 = studentsLine.Split('\n').Length;
                                if (length2 != length1) Console.WriteLine("Student was removed successfuly!");
                                else Console.WriteLine("Некорректный порядковый номер.");
                            }
                            else
                            {
                                Console.WriteLine("Некорректный порядковый номер.");
                            }
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                        case 3:
                            if (studentsLine == string.Empty)
                            {
                                Console.WriteLine("Изменять некого...");
                                Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Напишите ID студента, которого нужно изменить:");
                            Console.WriteLine(studentsLine);
                            ID = Console.ReadLine();
                            if (int.TryParse(ID, out int id_edit))
                            {
                                if (studentsLine.Contains(id_edit.ToString()))
                                {
                                    Console.WriteLine("Введите новые имя, фамилию и отчество студента:");
                                    name = Console.ReadLine();
                                    Console.WriteLine("Введите новую группу студента:");
                                    group = Console.ReadLine();
                                    Console.WriteLine("Введите новую специальность студента:");
                                    speciality = Console.ReadLine();
                                    result1 = false;
                                    result2 = false;
                                    result3 = false;
                                    for (int i = 0; i < control_string.Length; i++)
                                    {
                                        result1 = result1 || name.ToLower().Contains(control_string[i]);
                                    }
                                    for (int i = 0; i < control_group.Length; i++)
                                    {
                                        result2 = result2 || group.ToLower().Contains(control_group[i]);
                                    }
                                    for (int i = 0; i < control_string.Length; i++)
                                    {
                                        result3 = result3 || speciality.ToLower().Contains(control_string[i]);
                                    }
                                    Major_result = result1 && result2 && result3;
                                    if (Major_result == true)
                                    {
                                        EditDataEvent?.Invoke(id_edit, name, group, speciality);
                                        Console.WriteLine("Student was edited successfuly!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect line!");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такого ID нет!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некорректный порядковый номер.");
                            }
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                        case 4:
                            if (dictionaryForGystogram.Count == 0)
                            {
                                Console.WriteLine("Список студентов пуст.");
                                Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                                Console.ReadKey();
                                break;
                            }
                            Console.WriteLine("Гисторамма:");
                            foreach (KeyValuePair<string, int> value in dictionaryForGystogram)
                            {
                                string column = "8";
                                for (int i = 0; i < value.Value; i++)
                                {
                                    column += "=";
                                }
                                column += "D";
                                Console.WriteLine($"{value.Key,-10}: {column,-10} {value.Value}");
                            }
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                        case 5:
                            if (studentsLine == string.Empty)
                            {
                                Console.WriteLine("Нет ни одного студента.");
                            }
                            else
                            {
                                Console.WriteLine("Весь список студентов (порядковый номер | имя | группа | специальность)");
                                Console.WriteLine(studentsLine);
                            }
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("Было введено некорректное число, попробуйте ещё раз.");
                            Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Была введена некорректная строка, попробуйте ещё раз.");
                    Console.WriteLine("Для выхода в главное меню нажмите любую конпку...");
                    Console.ReadKey();
                }
            }
        }
    }
    public class Program
    {
        public static void Main()
        {
            
        }
    }
    public class Starter
    {
        public static void Run(Shared.IView view)
        {
            ConsoleView consoleView = view as ConsoleView;
            consoleView.Run();
        }
    }
}
