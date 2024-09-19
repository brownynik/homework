using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_manager.AppServices.Tasks
{
    [Serializable]
    public class CircleCalculatorTask : BaseCustomerTask
    {
        private double _radius = 0.0;

        public CircleCalculatorTask(string Name, IPublicator publicator) : base(Name, publicator)
        {
        }

        /// <summary>
        /// Можно редактировать: изменять радиус.
        /// </summary>
        public override bool Edit()
        {
            Publicator().Publicate($"Объект {this.Name} запрашивает радиус круга для редактирования.");
            Publicator().Publicate($"Предыдущее значение: {_radius}.");
            Publicator().Publicate("Для отказа от изменения значения нажмите <Enter> без ввода значения.");
            string buf = Console.ReadLine().Trim();
            if (buf.Length == 0)
            {
                return false;
            }
            else
            {
                try
                {
                    _radius = Convert.ToDouble(buf);
                    return true;
                }
                catch
                {
                    Publicator().Publicate("Ошибка конвертирования значения в число. Редактирование отменено.");
                    return false;
                }
            }
        }

        /// <summary>
        ///  Исполнение задачи заключается в расчёте и публикации площади круга (в произвольных единицах оценки).
        /// </summary>
        public override bool Execute()
        {
            Publicator().Publicate($"Цель исполнения объекта {this.Name}: расчёт и публикация площади круга.");
            double s = Math.PI * Math.Pow(_radius, 2);
            Publicator().Publicate($"При радиусе R = {_radius} площадь круга S = {s}");
            return true;
        }
    }
}
