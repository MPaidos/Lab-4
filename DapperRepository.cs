using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий Dapper
    /// </summary>
    public class DapperRepository : IRepository<Student>
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=C:\\Users\\FLEX2022\\source\\repos\\DataAccessLayer\\Database1.mdf;" +
            "Integrated Security=True";
        /// <summary>
        /// Команда создания Объекта
        /// </summary>
        /// <param name="obj">Объект, который пихаем в БД</param>
        public void Create(Student obj)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string script = "INSERT INTO students (Name, [Group], Speciality) VALUES('" + obj.Name + "', '" + obj.Group + "', '" + obj.Speciality + "'); SELECT CAST(SCOPE_IDENTITY() as int)";
                int ID = db.Query<int>(script, obj).FirstOrDefault();
                obj.ID = ID;
                UseScript(script);
            }
        }
        /// <summary>
        /// Команда удаления объекта
        /// </summary>
        /// <param name="obj">Объект, который выпулиавем из БД</param>
        public void Delete(Student obj)
        {
            string script = "DELETE FROM students WHERE Id = " + obj.ID;
            UseScript(script);
        }
        /// <summary>
        /// Команда изменения студента
        /// </summary>
        /// <param name="obj">Объект, который изменям в БД</param>
        public void Update(Student obj)
        {
                string script = "UPDATE students SET Name = '" + obj.Name + "', [Group] = '" + obj.Group + "', Speciality = '" + obj.Speciality + "' WHERE Id = " + obj.ID;
                UseScript(script);
        }
        /// <summary>
        /// Команда вывода всех объектов
        /// </summary>
        /// <returns>Метод возвращает список IEnumerable объектов</returns>
        public IEnumerable<Student> ReadAll()
        {
            List<Student> students;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                students = db.Query<Student>("SELECT * FROM students").ToList();
            }
            return students;
        }
        /// <summary>
        /// Команда прочтения объекта по ID
        /// </summary>
        /// <param name="id">ID Объекта, которого нужно вывести</param>
        /// <returns>Метод возвращает объект, которому принадлежит ID</returns>
        public Student ReadById(int id)
        {
            Student student;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                student = db.Query<Student>("SELECT * FROM students WHERE Id = " + id).FirstOrDefault();
            }
            return student;
        }
        /// <summary>
        /// Команда применения скрипта
        /// </summary>
        /// <param name="script">По сути SQL-команда</param>
        private void UseScript(string script)
        {
            if (!string.IsNullOrEmpty(script))
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute(script);
                }
            }
        }
    }
}
