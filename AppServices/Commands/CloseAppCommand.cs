using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices.Commands
{
    public class CloseAppCommand : BaseCustomerCommand
    {
        public CloseAppCommand(CustomerTaskManager<BaseCustomerTask> taskmanager, IPublicator publicator) : base("close", taskmanager, publicator)
        {
        }

        public override bool ExecuteCommand()
        {
            Publicator().Publicate("Это команда завершения сеанса работы с менеджером задач. Наберите Y или y для выхода.");
            Publicator().Publicate("Нажмите Enter, чтобы отказаться от завершения сеанса.");
            string buf = Console.ReadLine().Trim();

            return (buf.Length > 0 && buf.Substring(0, 1).ToUpper() == "Y");
        }
    }
}
