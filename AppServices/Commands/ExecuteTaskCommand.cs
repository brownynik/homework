using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices.Commands
{
    public class ExecuteTaskCommand : BaseCustomerCommand
    {
        public ExecuteTaskCommand(CustomerTaskManager<BaseCustomerTask> taskmanager, IPublicator publicator) : base("exec", taskmanager, publicator)
        {
        }

        public override bool ExecuteCommand()
        {
            Publicator().Publicate("Это команда запуска задачи на выполнение. Внизу она распечатает перечень задач. Введите GUID задачи, которую Вам надо исполнить.");
            Publicator().Publicate("Нажмите Enter, чтобы отказаться от исполнения.");
            TaskManager().PublicateAllTasks();
            string buf = Console.ReadLine().Trim();
            if (buf == string.Empty)
            {
                return false;
            }
            else
            {
                Guid taskId;
                try
                {
                    taskId = new Guid(buf);
                }
                catch
                {
                    taskId = Guid.Empty;
                }

                BaseCustomerTask task = TaskManager().GetCustomerTaskById(taskId);
                if (task != null)
                {
                    Publicator().Publicate($"Найдена задача с идентификатором {taskId}. Команда попытается запустить выполнение задачи.");
                    return task.Execute();
                }
                else
                {
                    Publicator().Publicate($"Задача с идентификатором {taskId} не найдена. Команда завершена.");
                    return false;
                }
            }
        }
    }
}
