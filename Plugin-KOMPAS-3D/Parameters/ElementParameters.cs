using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    class ElementParameters
    {
        /// <summary>
        /// Хранит словарь параметров элемента
        /// </summary>
        private Dictionary<ParametersName, Parameter<double>> _parameters = new Dictionary<ParametersName, Parameter<double>>();

        public Parameter<double> Parameter(ParametersName name)
        {
            return _parameters[name];
        }

        public void ChangeParameter(ParametersName name, double value)
            {
                //Parameter(name)
            }


        /// <summary>
        /// Метод высчитывает максимальное значение параметра
        /// высоты динамика(HS)
        /// исходя из значений высоты корпуса(H)
        /// и диаметра реле регулировки(D)
        /// по формуле HS=H-5-(D+10)
        /// </summary>
        /// !!!!
        public void CalculateMaxHeightDinamic()
        {
            Parameter(ParametersName.HS).MaxValue =
                Parameter(ParametersName.H).Value - 5
                - (Parameter(ParametersName.D).Value + 10);
        }

        ///Сделать общее перечисление, чтобы хранить одни и те же названия
        /// <summary>
        /// Метод высчитывает максимальное значение параметра 
        /// длинна динамика(LS)
        /// исходя из значения длинны колонки(L)
        /// по формуле LS=L-5
        /// </summary>
        /// !!!!
        public void CalculateMaxLenghtDinamic()
        {
            Parameter(ParametersName.WS).MaxValue =
                Parameter(ParametersName.W).Value - 5;
        }

        /// <summary>
        /// Конструктор класса ElementParameters
        /// </summary>
        public ElementParameters(List<(double min, double max, ParametersName name)> values)
        {
            _parameters = new Dictionary<ParametersName, Parameter<double>>();
            foreach (var value in values)
            {
                var parameter =
                    new Parameter<double>(value.min, value.max, value.min, value.name.ToString());
                _parameters.Add(value.name, parameter);
            }
        }
    }
}
