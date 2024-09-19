using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices.Tasks
{
    [Serializable]
    public class TextPublicatorTask : BaseCustomerTask
    {
        private string _containedData = "";

        public TextPublicatorTask(string Name, IPublicator publicator) : base(Name, publicator)
        {
        }

        /// <summary>
        /// Можно редактировать: изменять сохранённый и публикуемый текст.
        /// </summary>
        public override bool Edit()
        {
            Publicator().Publicate($"Объект {this.Name} запрашивает текст для редактирования.");
            Publicator().Publicate($"Предыдущее значение: {_containedData}.");
            Publicator().Publicate($"Для отказа от изменения значения нажмите <Enter> без ввода строки.");
            string buf = Console.ReadLine().Trim();
            if (buf.Length == 0)
            {
                return false;
            }
            else
            {
                _containedData = buf;
                return true;
            }
        }

        /// <summary>
        ///  Исполнение задачи заключается в публикации хранимого текста.
        /// </summary>
        public override bool Execute()
        {
            Publicator().Publicate($"Цель исполнения объекта {this.Name}: публикация текста.");
            Publicator().Publicate(_containedData);
            return true;
        }
    }
}
