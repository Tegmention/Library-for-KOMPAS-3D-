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
        /// </summary>
        private ElementFormKey _formKey;

        /// <summary>
        /// Возвращает параметер
        /// с запрошенным названием
        /// </summary>
        /// <param name="name">Название параметра</param>
        /// <returns></returns>
        public Parameter<double> Parameter(ParametersName name)
        {
            return _elementParameters.Parameter(name);
        }

        /// <summary>
        /// Возвращает ключ формы 
        /// элемента модели
        /// </summary>
        /// <returns></returns>
        public ElementFormKey FormKey()
        {
            return _formKey;
        }

        /// <summary>
        /// Изменяет параметры элемента
        /// в зависимости от текущего ключа формы
        /// </summary>
        public void CircleParameter()
        {
            if (_formKey == ElementFormKey.Circle)
            {
                _elementParameters.CalculationCircleParameter();
            }
        }

        /// <summary>
        /// Метод для изменения ключа формы
        /// </summary>
        public void ChangeForm()
        {
            if(_formKey == ElementFormKey.Circle)
            {
                _formKey = ElementFormKey.Rectangle;
            }
            else
            {
                _formKey = ElementFormKey.Circle;
                CircleParameter();
            }
        }

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="parameters">Параметра элемента</param>
        /// <param name="formKey">Ключ формы</param>
        public ModelElement(List<(double min, double max, ParametersName name)> parameters,
            ElementFormKey formKey)
        {
            _elementParameters = new ElementParameters(parameters);
            _formKey = formKey;
        }
    }
}
