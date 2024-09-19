using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices
{
    /// <summary>
    /// Базовый класс задачи.
    /// </summary>
    [Serializable]
    public class BaseCustomerTask
    {
        private readonly string _Name;
        private readonly Guid _Tag;
        private readonly IPublicator _publicator;

        public string Name { get { return _Name; }}
        public Guid Tag { get => _Tag; }

        public BaseCustomerTask(string Name, IPublicator publicator) : this()
        {
            _Name = Name;
            _publicator = publicator;
        }

        public BaseCustomerTask()
        {
            _Tag = Guid.NewGuid();
            _Name = "NoName Task " + _Tag.ToString();
        }

        protected IPublicator Publicator()
        {
            if (_publicator == null)
            { throw new ArgumentNullException(nameof(_publicator)); }

            return _publicator;
        }

        public virtual bool Execute()
        {
            return false;
        }

        public virtual bool Edit()
        {
            return false;
        }

        public void PublicateTag()
        {
            Publicator().Publicate(this.Tag.ToString());
        }

        public void PublicateNameWithTag()
        {
            Publicator().Publicate(this.Name + ": " + this.Tag.ToString());
        }
    }
}
