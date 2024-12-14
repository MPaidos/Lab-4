using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Model;

namespace DataAccessLayer
{
    /// <summary>
    /// Этот класс помогает ORM-системе понимать, коллекции каких сущностей будут в базе данных 
    /// </summary>
    public class DBContext : DbContext
    {
        /// <summary>
        /// Список студентов
        /// </summary>
        public DbSet<Student> students { get; set; }
        /// <summary>
        /// Конструктор DBContext, где вводим строку подключения
        /// </summary>
        public DBContext() : base("Data Source=(LocalDB)\\MSSQLLocalDB;" +
            "AttachDbFilename=C:\\Users\\FLEX2022\\source\\repos\\DataAccessLayer\\Database1.mdf;" +
            "Integrated Security=True") { }
    }
    
}
