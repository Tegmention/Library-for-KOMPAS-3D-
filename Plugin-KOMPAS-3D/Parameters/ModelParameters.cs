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
    public class ModelParameters
    {
        /// <summary>
        /// Хранит словарь параметров модели
        /// </summary>
        private Dictionary<string, Parameter<double>> _parameters = new Dictionary<string, Parameter<double>>();

        /// <summary>
        /// Метод высчитывает максимальное значение параметра
        /// высоты динамика(HS)
        /// исходя из значений высоты корпуса(H)
        /// и диаметра реле регулировки(D)
        /// по формуле HS=H-5-(D+10)
        /// </summary>
        public void CalculateMaxHeightDinamic()
        {
            Parameters[ParametersName.HS.ToString()].MaxValue = 
                Parameters[ParametersName.H.ToString()].Value - 5
                - (Parameters[ParametersName.D.ToString()].Value + 10);
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
            Parameters[ParametersName.LS.ToString()].MaxValue = 
                Parameters[ParametersName.L.ToString()].Value - 5;
        }

        /// <summary>
        /// Устанавливает и возвращает словарь параметров модели
        /// </summary>
        public Dictionary<string, Parameter<double>> Parameters { get; set; }

        /// <summary>
        /// Конструктор класса Parameters
        /// </summary>
        public ModelParameters()
        {
            Parameters = new Dictionary<string, Parameter<double>>();
            var values = new List<(double min, double max, string name)>
            {
                (100, 500, ParametersName.H.ToString()),
                (200, 300, ParametersName.L.ToString()),
                (150, 200, ParametersName.W.ToString()),
                (60, 75, ParametersName.HS.ToString()),
                (10, 20, ParametersName.D.ToString()),
                (5, 20, ParametersName.WS.ToString()),
                (150, 195, ParametersName.LS.ToString())
            };

            foreach (var value in values)
            {
                var parameter = 
                    new Parameter<double>(value.min, value.max, value.min, value.name);
                Parameters.Add(value.name,parameter);
            }
        }   
    }
}
