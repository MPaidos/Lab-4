using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class StudentEventArgs : EventArgs
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Speciality { get; set; }
    }
}
