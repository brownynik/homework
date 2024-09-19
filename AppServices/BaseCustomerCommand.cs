using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    /// <summary>
    /// Абстрактная команда.
    /// </summary>
    public abstract class BaseCustomerCommand
    {
        private CustomerTaskManager<BaseCustomerTask> _taskmanager;
        private string _shortName;
        private IPublicator _publicator;

        protected BaseCustomerCommand(string shortName, CustomerTaskManager<BaseCustomerTask> taskmanager, IPublicator publicator)
        {
            _shortName = shortName;
            _taskmanager = taskmanager;
            _publicator = publicator;
        }

        protected CustomerTaskManager<BaseCustomerTask> TaskManager()
        {
            return _taskmanager;
        }

        protected IPublicator Publicator()
        {
            if (_publicator == null)
            { 
                throw new ArgumentNullException(nameof(_publicator)); 
            }

            return _publicator;
        }

        public string ShortName()
        {
            return _shortName;
        }
        
        abstract public bool ExecuteCommand();

        public void PublicateName()
        {
            Publicator().Publicate(this.ShortName());
        }
    }
}
