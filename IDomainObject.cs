using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Интерфейс, который присваивает объекту ID
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Как раз-таки ID
        /// </summary>
        int ID { get; set; }
    }
}
