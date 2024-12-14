using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Репозиторий Entity
    /// </summary>
    public class EntityRepository : IRepository<Student>
    {
        DBContext _context;
        public EntityRepository(DBContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Команда создания объекта
        /// </summary>
        /// <param name="obj">Объект, который пихаем в базу данных</param>
        public void Create(Student obj)
        {
            _context.Set<Student>().Add(obj);
            _context.SaveChanges();
        }
        /// <summary>
        /// Команда удаления объекта
        /// </summary>
        /// <param name="obj">Объект, который сносим из базы данных</param>
        public void Delete(Student obj)
        {
            _context.Set<Student>().Remove(obj);
            _context.SaveChanges();
        }
        /// <summary>
        /// Команда изменения объекта
        /// </summary>
        /// <param name="obj">Объект изменения</param>
        public void Update(Student obj)
        {
            Student origin = (from c in _context.students
                          where c.ID == obj.ID
                          select c).FirstOrDefault();
            origin.Name = obj.Name;
            origin.Group = obj.Group;
            origin.Speciality = obj.Speciality;
            _context.SaveChanges();
        }
        /// <summary>
        /// Команда вывода всех объектов
        /// </summary>
        /// <returns>Метод возвращает список IEnumerable объектов</returns>
        public IEnumerable<Student> ReadAll()
        {
            return new List<Student>(_context.Set<Student>());
        }
        /// <summary>
        /// Команда прочтения объекта по ID
        /// </summary>
        /// <param name="id">ID Объекта, которого нужно вывести</param>
        /// <returns>Метод возвращает объект, которому принадлежит ID</returns>
        public Student ReadById(int id)
        {
            var query = from c in _context.students
                        where c.ID == id
                        select c;
            return query.FirstOrDefault();
        }
    }
}
