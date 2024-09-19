using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_manager.AppServices;
using task_manager.AppServices.Commands;
using task_manager.AppServices.Tasks;

namespace task_manager
{

    class Program
    {


        static void Main(string[] args)
        {
            IPublicator consolePublicator = new ConsolePublicator();
            ICustomerTaskStorage<BaseCustomerTask> storage = new CustomerTaskMemoryStorage();
            CustomerTaskManager<BaseCustomerTask> tm = new CustomerTaskManager<BaseCustomerTask>(storage);

            tm.RegisterCustomerTaskClass(typeof(TextPublicatorTask).FullName);
            tm.RegisterCustomerTaskClass(typeof(CircleCalculatorTask).FullName);

            tm.RegisterNewCustomerTask(new TextPublicatorTask("Sample Text Task 1", consolePublicator));
            tm.RegisterNewCustomerTask(new TextPublicatorTask("Sample Text Task 2", consolePublicator));
            tm.RegisterNewCustomerTask(new CircleCalculatorTask("Sample Circle Calc 3", consolePublicator));
            
            tm.RegisterNewCustomerCommand(new EditCommand(tm, consolePublicator));
            tm.RegisterNewCustomerCommand(new CloseAppCommand(tm, consolePublicator));
            tm.RegisterNewCustomerCommand(new ExecuteTaskCommand(tm, consolePublicator));
            tm.RegisterNewCustomerCommand(new CreateTaskCommand(tm, consolePublicator));

            // var t = new CircleCalculatorTask("Sample Circle Calc 3", consolePublicator);
            // tm.InspectType(t);
            // CircleCalculatorTask t = (CircleCalculatorTask)tm.CreateTaskInstanceByClassName("task_manager.AppServices.Tasks.CircleCalculatorTask", "new name", consolePublicator);
            // Console.ReadLine();

            // TODO:
            // Добавить CustomerTaskFileStorage как пример реализации.
            // Не успел.
            // Как планировал:
            // Текстовый файл, через сепаратор ";" идёт GUID и сериализованный объект задачи
            // (да, альтернативно можно сделать виртуальный метод в базовом классе и научить "сохранять себя" - то есть, сохранять свои внутренние поля).
            // При удалении из файла не стирать, просто замещать GUID на строку определённого символа, например, #32.

            BaseCustomerCommand cmd;
            string cmdName;
            bool executeResult;
            do
            {
                executeResult = false;
                cmdName = "";
                Console.Clear();
                tm.PublicateAllTasks();
                tm.PublicateAllCommands();
                Console.WriteLine("Введите команду из списка: ");
                cmdName = Console.ReadLine().Trim();
                cmd = tm.GetCommandByName(cmdName);
                if (cmd == null)
                {
                    Console.WriteLine($"Команда {cmdName} не найдена. Нажмите Enter.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Команда {cmdName} найдена и будет запущена.");
                    executeResult = cmd.ExecuteCommand();
                    Console.WriteLine($"Команда {cmdName} " + (executeResult ? "выполнена" : "не выполнена") + ". Нажмите Enter.");
                    Console.ReadLine();
                }

            } while (!(cmd!= null && cmd is CloseAppCommand && executeResult));
        }
    }
}
