using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Слой модели
    /// </summary>
    public class Student : IDomainObject
    {
        /// <summary>
        /// ID студента
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Имя студента ([Фамилия] [Имя] [Отчество (при наличии)])
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Группа студента
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Направление/специальность студента
        /// </summary>
        public string Speciality { get; set; }
    }
}
