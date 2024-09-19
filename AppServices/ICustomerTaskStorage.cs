using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    /// <summary>
    /// Обобщённый интерфейс хранилища
    /// </summary>
    public interface ICustomerTaskStorage<T> where T : BaseCustomerTask
    {
        bool Store(T customerTask);
        T Read(Guid Id);
        bool Check(Guid Id);
        bool Delete(Guid Id);
        Guid[] GetAllIds();
    }
}
