using System.Collections.Generic;

namespace Parameters
{
    /// <summary>
    /// Класс элемента модели
    /// </summary>
    public class ModelElement
    {
        /// <summary>
        /// Хранит параметры элемента
        /// </summary>
        private ElementParameters _elementParameters;

        /// <summary>
        /// Хранит ключ формы модели
        /// true - круг
        /// false - прямоугольник
        /// </summary>
        private bool _formKey;

        public Parameter<double> Parameter(ParametersName name)
        {
            return _elementParameters.Parameter(name);
        }

        /// <summary>
        /// Возвращает ключ формы 
        /// элемента модели
        /// </summary>
        /// <returns></returns>
        public bool FormKey()
        {
            return _formKey;
        }

        /// <summary>
        /// Изменяет параметры элемента
        /// в зависимости от текущего ключа формы
        /// </summary>
        public void CircleParameter()
        {
            if (_formKey == true)
            {
                _elementParameters.CalculationCircleParameter();
            }
        }

        /// <summary>
        /// Метод для изменения ключа формы
        /// </summary>
        public void ChangeForm()
        {
            if(_formKey == true)
            {
                _formKey = false;
            }
            else
            {
                CircleParameter();
                _formKey = true;
            }
        }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="formKey"></param>
        public ModelElement(List<(double min, double max, ParametersName name)> parameters,
            bool formKey)
        {
            _elementParameters = new ElementParameters(parameters);
            _formKey = formKey;
        }
    }
}
