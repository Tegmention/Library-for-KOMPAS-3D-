using System.Collections.Generic;

namespace Parameters
{
    public class ElementParameters
    {
        /// <summary>
        /// Хранит словарь параметров элемента
        /// </summary>
        private Dictionary<ParametersName, Parameter<double>> _parameters = new Dictionary<ParametersName, Parameter<double>>();

        /// <summary>
        /// Возвращает параметер элемента
        /// </summary>
        /// <param name="name">Имя возвращаемого параметра</param>
        /// <returns></returns>
        public Parameter<double> Parameter(ParametersName name)
        {
            return _parameters[name];
        }

        //Перерасчет параметров для хранения знечения круга
        public void CalculationCircleParameter()
        {
            if (Parameter(ParametersName.H).MaxValue >
                          Parameter(ParametersName.W).MaxValue)
            {
                Parameter(ParametersName.H).MaxValue =
                Parameter(ParametersName.W).MaxValue;
            }
            if (Parameter(ParametersName.W).MaxValue >
                Parameter(ParametersName.H).MaxValue)
            {
                Parameter(ParametersName.W).MaxValue =
                Parameter(ParametersName.H).MaxValue;
            }
        }

        /// <summary>
        /// Конструктор класса ElementParameters
        /// </summary>
        /// <param name="values">Картеж значений параметро</param>
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
