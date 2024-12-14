using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс IRepository
    /// </summary>
    /// <typeparam name="T">Тип данных, с которым интерфейс будет работать</typeparam>
    public interface IRepository<T> where T : IDomainObject, new()
    {
        /// <summary>
        /// Создать объект
        /// </summary>
        /// <param name="obj">Объект, который пихаем в БД</param>
        void Create(T obj);
        /// <summary>
        /// Вывести все объекты
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> ReadAll();
        /// <summary>
        /// Прочитать по ID
        /// </summary>
        /// <param name="id">ID объекта, которого надо прочесть</param>
        /// <returns>Возвращает Объект, которму принадлежит ID</returns>
        T ReadById(int id);
        /// <summary>
        /// Изменить объект
        /// </summary>
        /// <param name="obj">Объект, который изменяем</param>
        void Update(T obj);
        /// <summary>
        /// Удалить объект
        /// </summary>
        /// <param name="obj">Объект, который выпуливаем из БД</param>
        void Delete(T obj);
    }
}
