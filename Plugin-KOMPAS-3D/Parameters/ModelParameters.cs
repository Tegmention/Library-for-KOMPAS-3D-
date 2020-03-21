using System.Collections.Generic;
using System;

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
        private Dictionary<ParametersName, Parameter<double>> _parameters = new Dictionary<ParametersName, Parameter<double>>();

        /// <summary>
        /// Хранит ключ для формы динамика
        /// </summary>
        private bool[] _keyForm = new bool[4];

        public Parameter<double> Parameter(ParametersName name)
        {
            return _parameters[name];
        }

        /// <summary>
        /// Метод высчитывает максимальное значение параметра
        /// высоты динамика(HS)
        /// исходя из значений высоты корпуса(H)
        /// и диаметра реле регулировки(D)
        /// по формуле HS=H-5-(D+10)
        /// </summary>
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
        public void CalculateMaxLenghtDinamic()
        {
            Parameter(ParametersName.WS).MaxValue = 
                Parameter(ParametersName.W).Value - 5;
        }

        public double CalculationMaxH()
        {
            var HS = 0.0;
            var HS1 = 0.0; 
            var HS2 = 0.0;
            var HS3 = 0.0;
            if (_parameters.ContainsKey(ParametersName.HS))
                HS = Parameter(ParametersName.HS).Value;
            if (_parameters.ContainsKey(ParametersName.HS1))
                HS1 = Parameter(ParametersName.HS1).Value;
            if (_parameters.ContainsKey(ParametersName.HS2))
                HS2 = Parameter(ParametersName.HS2).Value;
            if (_parameters.ContainsKey(ParametersName.HS3))
                HS3 = Parameter(ParametersName.HS3).Value;
            var maxH = Parameter(ParametersName.H).Value - 5
                - (Parameter(ParametersName.D).Value + 10)
                - HS - HS1 - HS2 - HS3;
            return maxH;
        }

        public void CalculationMax()
        {
            var maxH = CalculationMaxH();
            if (_parameters.ContainsKey(ParametersName.HS))
            { 
                Parameter(ParametersName.HS).MaxValue = maxH; 
            }
            if (_parameters.ContainsKey(ParametersName.HS1))
            {
                Parameter(ParametersName.HS1).MaxValue = maxH;
            }
            if (_parameters.ContainsKey(ParametersName.HS2))
            {
                Parameter(ParametersName.HS1).MaxValue = maxH;
            }
            if (_parameters.ContainsKey(ParametersName.HS3))
            {
                Parameter(ParametersName.HS1).MaxValue = maxH;
            }
        }

        public void AddParametersCap()
        {
            var minH = 10.0;
            var maxW = 0.0;
            var maxT = 20;
            var minT = 5;
            var minW = 150;
            _keyForm[0] = false;
            _keyForm[1] = false;
            _keyForm[2] = false;
            _keyForm[3] = false;
            //Добавление 2 динамика
            if (!_parameters.ContainsKey(ParametersName.HS1))
            {
                var maxH = CalculationMaxH();
                if (_keyForm[1] == true)
                {
                    maxW = maxH;
                    minW = 50;
                }
                else
                {
                    maxW = Parameter(ParametersName.W).Value - 5;
                }
                var parameter = new Parameter<double>(minH, maxH, minH, ParametersName.HS1.ToString());
                _parameters.Add(ParametersName.HS1, parameter);
                parameter = new Parameter<double>(minT, maxT, minT, ParametersName.TS1.ToString());
                _parameters.Add(ParametersName.TS1, parameter);
                parameter = new Parameter<double>(minW, maxW, minW, ParametersName.WS1.ToString());
                _parameters.Add(ParametersName.WS1, parameter);
            }
            else
            {
                //Добавление 3 динамика
                if (!_parameters.ContainsKey(ParametersName.HS2))
                {
                    var maxH = CalculationMaxH();
                    if (_keyForm[2] == true)
                    {
                        maxW = maxH;
                        minW = 50;
                    }
                    else
                    {
                        maxW = Parameter(ParametersName.W).Value - 5;
                    }
                    var parameter = new Parameter<double>(minH, maxH, minH, ParametersName.HS2.ToString());
                    _parameters.Add(ParametersName.HS2, parameter);
                    parameter = new Parameter<double>(minT, maxT, minT, ParametersName.TS2.ToString());
                    _parameters.Add(ParametersName.TS2, parameter);
                    parameter = new Parameter<double>(minW, maxW, minW, ParametersName.WS2.ToString());
                    _parameters.Add(ParametersName.WS2, parameter);
                }
                else
                {
                    //Добавление 4 динамика
                    if (!_parameters.ContainsKey(ParametersName.HS3))
                    {
                        var maxH = CalculationMaxH();
                        if (_keyForm[3] == true)
                        {
                            maxW = maxH;
                            minW = 50;
                        }
                        else
                        {
                            maxW = Parameter(ParametersName.W).Value - 5;
                        }
                        var parameter = new Parameter<double>(minH, maxH, minH, ParametersName.HS3.ToString());
                        _parameters.Add(ParametersName.HS3, parameter);
                        parameter = new Parameter<double>(minT, maxT, minT, ParametersName.TS3.ToString());
                        _parameters.Add(ParametersName.TS3, parameter);
                        parameter = new Parameter<double>(minW, maxW, minW, ParametersName.WS3.ToString());
                        _parameters.Add(ParametersName.WS3, parameter);
                    }
                }
            }
        }

        /// <summary>
        /// Конструктор класса Parameters
        /// </summary>
        public ModelParameters()
        {
            _parameters = new Dictionary<ParametersName, Parameter<double>>();
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L),
                (10, 75, ParametersName.HS),
                (10, 20, ParametersName.D),
                (5, 20, ParametersName.TS),
                (150, 195, ParametersName.WS)
            };

            foreach (var value in values)
            {
                var parameter = 
                    new Parameter<double>(value.min, value.max, value.min, value.name.ToString());
                _parameters.Add(value.name,parameter);
            }
        }   
    }
}
