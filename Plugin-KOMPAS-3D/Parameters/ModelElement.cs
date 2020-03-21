﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
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

        public bool FormKey()
        {
            return _formKey;
        }

        public void ChangeParameter(ParametersName name, double value)
        {
            //_elementParameters;
        }

        public ModelElement(List<(double min, double max, ParametersName name)> parameters,
            bool formKey)
        {
            _elementParameters = new ElementParameters(parameters);
            _formKey = formKey;
        }

        ///Метод для изменения ключа формы
    }
}