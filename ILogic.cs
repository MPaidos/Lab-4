using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogical
{
    public interface ILogic
    {
        event Action<string> DataChanged;

        /// <summary>
        /// Метод добавления студента
        /// </summary>
        /// <param name="name">Имя студента</param>
        /// <param name="group">Группа студента</param>
        /// <param name="speciality">Специальность</param>
        /// <returns>Метод возвращает айди студента, который был создан</returns>
        int AddStudent(string name, string group, string speciality);
        /// <summary>
        /// Метод удаления студента
        /// </summary>
        /// <param name="id">Индекс студента, которого нужно отчислить</param>
        void RemoveStudent(int id);
        /// <summary>
        /// Вернуть всех студентов
        /// </summary>
        /// <returns>Метод возвращает весь список студентов в форме string</returns>
        string GetAll();
        /// <summary>
        /// Изменение студента
        /// </summary>
        /// <param name="id">Индекс студента, которого нужно изменить</param>
        /// <param name="name">Новое Имя студента</param>
        /// <param name="group">Новая группа студента</param>
        /// <param name="speciality">Новая спкциальность студента</param>
        void EditStudent(int id, string name, string group, string speciality);
        /// <summary>
        /// Метод для преобразования набора студентов и специальностей в словарь
        /// </summary>
        /// <returns>Метод возвращает Словарь студентов, где ключ - название специальности,
        /// значение - число студентов на специальности</returns>
        Dictionary<string, int> GetKeyValuePairs();
    }
}
