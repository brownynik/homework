using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    /// <summary>
    /// Класс менеджера задач.
    /// </summary>
    public class CustomerTaskManager<T> where T : BaseCustomerTask, new()
    {
        private ICustomerTaskStorage<T> storage;
        private List<BaseCustomerCommand> commands;
        private List<string> tclasses;

        public CustomerTaskManager(ICustomerTaskStorage<T> st)
        {
            storage = st;
            commands = new List<BaseCustomerCommand>();
            tclasses = new List<string>();
        }

        public bool RegisterCustomerTaskClass(string ClassName)
        {
            Console.WriteLine($"Регистрируем класс \"{ClassName}\"");
            tclasses.Add(ClassName);
            return true;
        }

        public T RegisterNewCustomerTask(T task)
        {
            if (storage == null)
                { throw new ArgumentNullException(nameof(storage)); }

            try
            {
                bool IsStore = storage.Store(task);
                if (!IsStore)
                {
                    task = null;
                }
            }
            catch (Exception e)
            {
                task = null;
                Console.WriteLine($"Задача не прошла регистрацию в хранилище. Ошибка размещения: {e.Message}.");
            }
            
            return task;
        }

        public BaseCustomerCommand RegisterNewCustomerCommand(BaseCustomerCommand cmd)
        {
            commands.Add(cmd);
            return cmd;
        }

        public BaseCustomerCommand GetCommandByName(string CommandName)
        {
            return commands.Find(cmd => cmd.ShortName() == CommandName);
        }

        public T GetCustomerTaskById(Guid Id)
        {
            if (storage == null)
            { throw new ArgumentNullException(nameof(storage)); }

            T task = storage.Read(Id);
            if (task == null)
            {
                Console.WriteLine($"Задача с GUID = {Id} не найдена в хранилище. Метод вернёт null, над null нет возможности совершить прочие операции.");
            }

            return task;
        }

        public bool StoreCustomerTask(T task)
        {
            if (storage == null)
            { throw new ArgumentNullException(nameof(storage)); }

            bool f = storage.Check(task.Tag);
            if (f)
            {
                storage.Delete(task.Tag);
            }

            bool r = storage.Store(task);
            if (!r)
            {
                Console.WriteLine($"Задача с GUID = {task.Tag} не сохранена.");
            }

            return r;
        }

        public bool EditTask(T task)
        {
            if (task == null)
            {
                Console.WriteLine($"Задача не существует, редактировать нельзя.");
                return false;
            }

            bool taskChanged = task.Edit();
            if (taskChanged)
            {
                try
                {
                    bool isStore = StoreCustomerTask(task);
                    if (isStore)
                    {
                        Console.WriteLine($"Успешно сохранили задачу с GUID = {task.Tag} после редактирования.");
                    }
                    else 
                    {
                        Console.WriteLine($"Не смогли сохранить задачу с GUID = {task.Tag} после редактирования.");
                    }
                    return isStore;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Исключительная ситуация - не смогли сохранить задачу с GUID = {task.Tag} после редактирования. {e.Message}.");
                }
            }
            return false;
        }

        public bool EditTaskById(Guid Id)
        {
            var task = GetCustomerTaskById(Id);
            if (task == null)
            {
                Console.WriteLine($"Не нашли задачу с GUID = {Id}, редактирование невозможно.");
                return false;
            }
            else return EditTask(task);
        }

        /// <summary>
        /// Публикует теги всех задач в хранилище.
        /// </summary>
        public void PublicateAllTags()
        {
            Console.WriteLine("Полный перечень задач:");

            Guid[] ids = storage.GetAllIds();

            foreach (var id in ids) 
            {
                var task = GetCustomerTaskById(id);
                if (task != null)
                {
                    task.PublicateTag();
                }
            }
        }

        /// <summary>
        /// Публикует список всех зарегистрированных классов.
        /// </summary>
        public void PublicateAllClasses()
        {
            Console.WriteLine("Полный перечень классов:");
            tclasses.ForEach(c => Console.WriteLine(c));
        }

        /// <summary>
        /// Публикует имена и теги всех задач в хранилище.
        /// </summary>
        public void PublicateAllTasks()
        {
            Console.WriteLine("Полный перечень задач с их наименованием и уникальным тегом:");

            Guid[] ids = storage.GetAllIds();

            foreach (var id in ids)
            {
                var task = GetCustomerTaskById(id);
                if (task != null)
                {
                    task.PublicateNameWithTag();
                }
            }
        }

        /// <summary>
        /// Публикует все команды.
        /// </summary>
        public void PublicateAllCommands()
        {
            Console.WriteLine("Полный перечень доступных команд. Команды чувствительны к регистру.");
            commands.ForEach(c => c.PublicateName());
        }

        public bool ExecuteCommand(BaseCustomerCommand cmd)
        {
            return cmd.ExecuteCommand();
        }

        public void InspectType(BaseCustomerTask t)
        {
            Console.WriteLine($"ClassName = {t.GetType().FullName}");
        }

        public BaseCustomerTask CreateTaskInstanceByClassName(string ClassName, string InstanceName, IPublicator publicator)
        {
            Assembly assembly = typeof(BaseCustomerTask).Assembly;
            Type customTaskType = assembly.GetType(ClassName);
            if (customTaskType == null)
            {
                Console.WriteLine($"Не найден зарегистрированный класс с именем {ClassName}.");
                return null;
            }

            try
            {
                ConstructorInfo constructor = customTaskType.GetConstructor(new Type[] { typeof(string), typeof(IPublicator) });
                BaseCustomerTask newTask = (BaseCustomerTask)constructor.Invoke(new object[] { InstanceName, publicator });
                return newTask;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при создании экземпляра класса {ClassName}. {e.Message}.");
                return null;
            }
        }

        public bool IsClassRegistered(string ClassName)
        {
            return (tclasses.IndexOf(ClassName) >= 0);
        }
    }
}
