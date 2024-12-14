using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Model;
using DataAccessLayer;

namespace BusinessLogical
{
    public class Logic : ILogic
    {
        public event Action<string> DataChanged;
        public Logic(IRepository<Student> students)
        {
            this.students = students;
        }
        IRepository<Student> students { get; set; }
        
        public int AddStudent(string name, string group, string speciality)
        {
            Student student = new Student()
            {
                Name = name,
                Group = group,
                Speciality = speciality
            };
            students.Create(student);
            InvokeDataChanged();
            return student.ID;
        }
        public void RemoveStudent(int id)
        {
            Student student = students.ReadById(id);
            if (student != null)
            {
                students.Delete(student);
            }
            InvokeDataChanged();
        }
        public string GetAll()
        {
            string s = string.Empty;
            foreach(var stud in students.ReadAll())
            {
                s += stud.ID.ToString() + "|" + stud.Name + "|" + stud.Group + "|" + stud.Speciality + "\n";
            }
            return s;
        }
        public void EditStudent(int id, string name, string group, string speciality)
        {
            Student student = new Student
            {
                ID = id,
                Name = name,
                Group = group,
                Speciality = speciality
            };
            students.Update(student);
            InvokeDataChanged();
        }
        public Dictionary<string, int> GetKeyValuePairs()
        {
            int firstId, lastId;
            if (students.ReadAll().ToList().Count == 0)
            {
                firstId = lastId = 0;
            }
            else
            {
                firstId = students.ReadAll().ToList()[0].ID;
                lastId = students.ReadAll().ToList()[students.ReadAll().Count() - 1].ID;
            }
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            for(int i = firstId; i <= lastId; i++)
            {
                if (students.ReadById(i) != null)
                {
                    if (dictionary.ContainsKey(students.ReadById(i).Speciality))
                    {
                        dictionary[students.ReadById(i).Speciality] += 1;
                    }
                    else
                    {
                        dictionary.Add(students.ReadById(i).Speciality, 1);
                    }
                }
            }
            return dictionary;
        }
        private void InvokeDataChanged()
        {
            DataChanged?.Invoke(GetAll());
        }
    }
}
