using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices.Commands
{
    public class CreateTaskCommand : BaseCustomerCommand
    {
        public CreateTaskCommand(CustomerTaskManager<BaseCustomerTask> taskmanager, IPublicator publicator) : base("create", taskmanager, publicator)
        {
        }

        public override bool ExecuteCommand()
        {
            Publicator().Publicate("Это команда создания нового экземпляра из списка зарегистрированных классов. Внизу она распечатает перечень классов. Введите полное наименование класса (чувствителен к регистру!), который Вам надо создать.");
            Publicator().Publicate("Нажмите Enter, чтобы отказаться от создания экземпляра.");
            TaskManager().PublicateAllClasses();
            string buf = Console.ReadLine().Trim();
            
            if (!TaskManager().IsClassRegistered(buf))
            {
                Publicator().Publicate($"Класс {buf} не зарегистрирован.");
            }
            else
            {
                Publicator().Publicate("А теперь введите наименование экземпляра:");
                string InstanceName = Console.ReadLine();

                var task = TaskManager().CreateTaskInstanceByClassName(buf, InstanceName, Publicator());
                if (task != null)
                {
                    Publicator().Publicate($"Экземпляр класса {buf} создан. Регистрируем в менеджере: ");
                    var resultTask = TaskManager().RegisterNewCustomerTask(task);
                    if (resultTask != null)
                    {
                        Publicator().Publicate($"Экземпляр класса {buf} зарегистрирован в менеджере.");
                        return true;
                    }
                    else
                    {
                        Publicator().Publicate($"Не смогли зарегистрировать экземпляр класса {buf}.");
                    }
                }
                else
                {
                    Publicator().Publicate($"Экземпляр класса {buf} не создан.");
                }
            }

           return false;
        }
    }
}
