using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    public class ConsolePublicator : IPublicator
    {
        public void Publicate(string Message)
        {
            Console.WriteLine(Message);
        }
    }
}
