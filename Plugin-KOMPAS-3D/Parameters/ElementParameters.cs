using System.Collections.Generic;

namespace Parameters
{
    /// <summary>
    /// Класс хранит 
    /// параметры элемента модели
    /// и реализует методы перерасчета
    /// </summary>
    public class ElementParameters
    {
        /// <summary>
        /// Хранит словарь параметров элемента
        /// </summary>
        private Dictionary<ParametersName, Parameter<double>> _parameters =
            new Dictionary<ParametersName, Parameter<double>>();

        /// <summary>
        /// Возвращает параметер элемента
        /// </summary>
        /// <param name="name">Имя возвращаемого параметра</param>
        /// <returns>Параметер элемента</returns>
        public Parameter<double> Parameter(ParametersName name)
        {
            return _parameters[name];
        }

        /// <summary>
        /// Метод перерасчитывает
        /// значения параметров 
        /// для хранения формы круга
        /// </summary>
        public void CalculationCircleParameter()
        {
            if (Parameter(ParametersName.Height).MaxValue >
                          Parameter(ParametersName.Width).MaxValue)
            {
                Parameter(ParametersName.Height).MaxValue =
                Parameter(ParametersName.Width).MaxValue;
            }
            if (Parameter(ParametersName.Width).MaxValue >
                Parameter(ParametersName.Height).MaxValue)
            {
                Parameter(ParametersName.Width).MaxValue =
                Parameter(ParametersName.Height).MaxValue;
            }
        }

        /// <summary>
        /// Конструктор класса ElementParameters
        /// </summary>
        /// <param name="values">
        /// Картеж значений 
        /// параметров элемента</param>
        public ElementParameters(List<(double min, double max, ParametersName name)> values)
        {
            _parameters = new Dictionary<ParametersName, Parameter<double>>();
            foreach (var value in values)
            {
                var parameter =
                    new Parameter<double>(value.min, value.max, 
                    value.min, value.name.ToString());
                _parameters.Add(value.name, parameter);
            }
        }
    }
}
