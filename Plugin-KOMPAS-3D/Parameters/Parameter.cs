﻿using System;

namespace Parameters
{
    /// <summary>
    /// Хранит данные одного параметра модели
    /// Хранит название, текущее значение
    /// и граничные значения параметра модели
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Parameter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Поле хранит название параметра
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле хранит максимальное значение параметра
        /// </summary>
        private T _maxValue;

        /// <summary>
        /// Поле хранит минимальное значение параметра
        /// </summary>
        private T _minValue;

        /// <summary>
        /// Поле хранит текущее значение параметра
        /// </summary>
        private T _value;

        /// <summary>
        /// Устанавливает и возвращает максимальное значение параметра
        /// </summary>
        public T MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает минимальное значение параметра
        /// </summary>
        public T MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
            }
        }

        /// <summary>
        /// Устанавливает и возвращает текущее значение параметра
        /// Текущее значение устанавливается если оно входит в диапозон параметров
        /// от _minValue до _maxValue
        /// </summary>
        public T Value
        { 
            get
            {
                return _value;
            }
            set
            {
                if (value.CompareTo(_minValue) < 0 || value.CompareTo(_maxValue) > 0)
                {
                    throw new ArgumentException("Значение параметра " + _name + " должно находиться в диапозоне от " +
                        _minValue + " до " + _maxValue);
                }
                else
                {
                    _value = value;
                }
            }
        }

        /// <summary>
        /// Коструктор класса Parameter
        /// </summary>
        /// <param name="minValue">Минимальное значение параметра</param>
        /// <param name="maxValue">Максимальное значение параметра</param>
        /// <param name="value">Текущее значение параметра</param>
        /// <param name="name">Название параметра</param>
        public Parameter(T minValue, T maxValue, T value, string name)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _value = value;
            _name = name;
        }
    }
}
