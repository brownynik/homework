using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    public class CustomerTaskFileStorage : ICustomerTaskStorage<BaseCustomerTask>
    {
        public CustomerTaskFileStorage()
        {
        }

        bool ICustomerTaskStorage<BaseCustomerTask>.Check(Guid Id)
        {
            throw new NotImplementedException();
        }

        bool ICustomerTaskStorage<BaseCustomerTask>.Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        Guid[] ICustomerTaskStorage<BaseCustomerTask>.GetAllIds()
        {
            throw new NotImplementedException();
        }

        BaseCustomerTask ICustomerTaskStorage<BaseCustomerTask>.Read(Guid Id)
        {
            throw new NotImplementedException();
        }

        bool ICustomerTaskStorage<BaseCustomerTask>.Store(BaseCustomerTask customerTask)
        {
            throw new NotImplementedException();
        }

        /*
        public CustomerTaskFileStorage(string fileName)
        {
            _storage = new FileStream(fileName, FileMode.CreateNew);
        }

        public Guid[] GetAllIds()
        {
            if (_storage == null)
            { throw new ArgumentNullException(nameof(_storage)); }

            return _storage.Select(task => task.Tag).ToArray();
        }

        public BaseCustomerTask Read(Guid Id)
        {
            if (_storage == null)
            { throw new ArgumentNullException(nameof(_storage)); }

            BaseCustomerTask task = null;
            foreach (var t in _storage)
            {
                if (t != null && t.Tag == Id)
                {
                    task = t;
                    break;
                }
            }

            return task;
        }

        public bool Check(Guid Id)
        {
            if (_storage == null)
            { throw new ArgumentNullException(nameof(_storage)); }

            foreach (var t in _storage)
            { if (t != null && t.Tag == Id) { return true; } };

            return false;
        }

        public bool Delete(Guid Id)
        {
            if (_storage == null)
            { throw new ArgumentNullException(nameof(_storage)); }

            bool r = false;
            var t = _storage.Find(c => c.Tag == Id);
            while (t != null)
            {
                _storage.Remove(t);
                t = _storage.Find(c => c.Tag == Id);
                r = true;
            }

            return r;
        }
        public bool Store(BaseCustomerTask customerTask)
        {
            if (_storage == null)
            { throw new ArgumentNullException(nameof(_storage)); }

            bool r = !Check(customerTask.Tag);
            if (r)
            {
                try
                {
                    _storage.Add(customerTask);
                }
                catch (Exception e)
                {
                    r = false;
                    Console.WriteLine($"Задача с Id = {customerTask.Tag} не сохранилась. Ошибка: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Задача с Id = {customerTask.Tag} уже записана в хранилище.");
            }

            return r;
        }
        */
    }
}
