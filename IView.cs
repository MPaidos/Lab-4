using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Интерфейс слоя представления
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Событие на добавление новой записи
        /// </summary>
        event Action<EventArgs> AddDataEvent;
        /// <summary>
        /// Событие на удаление записи
        /// </summary>
        event Action<int> DeleteDataEvent;
        /// <summary>
        /// Событие на изменение записи
        /// </summary>
        event Action<int, string, string, string> EditDataEvent;
        /// <summary>
        /// Метод обновления содержимого в слое модели
        /// </summary>
        /// <param name="data">Список новых записей</param>
        void RedrawView(IEnumerable<StudentEventArgs> data);
    }
}
