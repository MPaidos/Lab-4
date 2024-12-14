using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BusinessLogical;
using Shared;
using Model;

namespace Presenter
{
    public class Presenter1
    {
        private ILogic logic;
        private IView view;
        public Presenter1(IView view, ILogic logic)
        {
            this.logic = logic;
            this.view = view;
            logic.DataChanged += OnDataChanged;
            view.AddDataEvent += OnAddData;
            view.DeleteDataEvent += OnDeleteData;
            view.EditDataEvent += OnEditData;
            view.RedrawView(GetStudentArgs(logic.GetAll()));
        }
        private void OnDataChanged(string students)
        {
            List<StudentEventArgs> args = GetStudentArgs(students);
            view.RedrawView(args);
        }
        private void OnAddData(EventArgs data)
        {
            StudentEventArgs args = data as StudentEventArgs;
            logic.AddStudent(args.Name, args.Group, args.Speciality);
        }
        void OnDeleteData(int id)
        {
            logic.RemoveStudent(id);
        }
        void OnEditData(int id, string name, string group, string speciality)
        {
            logic.EditStudent(id, name, group, speciality);
        }
        List<StudentEventArgs> GetStudentArgs(string args)
        {
            List<StudentEventArgs> studentEventArgs = new List<StudentEventArgs>();
            string[] studentsArray = args.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in studentsArray)
            {
                string[] data = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                StudentEventArgs studentArgs = new StudentEventArgs
                {
                    ID = int.Parse(data[0]),
                    Name = data[1],
                    Group = data[2],
                    Speciality = data[3]
                };
                studentEventArgs.Add(studentArgs);
            }
            return studentEventArgs;
        }
    }
}
