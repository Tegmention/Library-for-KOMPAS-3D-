using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    /// <summary>
    /// Хранит словарь параметров модели
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// Хранит словарь параметров модели
        /// </summary>
        private Dictionary<string, Parameter<double>> _modelParameters;

        /// <summary>
        /// Метод высчитывает максимальное значение параметра
        /// высоты динамика(HS)
        /// исходя из значений высоты корпуса(H)
        /// и диаметра реле регулировки(D)
        /// по формуле HS=H-5-(D+10)
        /// </summary>
        public void CalculateMaxHeightDinamic()
        {
            _modelParameters["HS"].MaxValue = _modelParameters["H"].Value - 5
                - (_modelParameters["D"].Value + 10);
        }
        ///Сделать общее перечисление, чтобы хранить одни и те же названия
        /// <summary>
        /// Метод высчитывает максимальное значение параметра 
        /// длинна динамика(LS)
        /// исходя из значения длинны колонки(L)
        /// по формуле LS=L-5
        /// </summary>
        public void CalculateMaxLenghtDinamic()
        {
            _modelParameters["LS"].MaxValue = _modelParameters["L"].Value - 5;
        }

        /// <summary>
        /// Конструктор класса Parameters
        /// </summary>
        public Parameters()
        {
            var values = new List<(double min, double max, string name)>
            {
                (100, 500, "H"),
                (200, 300, "L"),
                (150, 200, "W"),
                (60, 75, "HS"),
                (10, 20, "D"),
                (5, 20, "WS"),
                (150, 195, "LS")
            };

            foreach (var value in values)
            {
                _modelParameters[value.name] = 
                    new Parameter<double>(value.min, value.max, value.min, value.name);
            }
        }   
    }
}
