using System;

namespace Parameters
{
    /// <summary>
    /// Хранит данные одного параметра модели
    /// Хранит название, текущее значение
    /// и граничные значения параметра модели
    /// </summary>
    /// <typeparam name="T">Тип хранимых данных</typeparam>
    public class Parameter<T> where T : IComparable<T>
    {
        /// <summary>
        /// Поле хранит название параметра
        /// </summary>
        private string _name;

        /// <summary>
        /// Поле хранит текущее значение параметра
        /// </summary>
        private T _value;

        /// <summary>
        /// Устанавливает и возвращает минимальное значение параметра
        /// </summary>
        public T _maxValue;

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
                if(value.CompareTo(MinValue) < 0)
                {
                    _maxValue = MinValue;
                }
                else
                {
                    _maxValue = value;
                }
            }
        }

        /// <summary>
        /// Устанавливает и возвращает минимальное значение параметра
        /// </summary>
        public T MinValue { get; set; }

        /// <summary>
        /// Устанавливает и возвращает текущее значение параметра
        /// Текущее значение устанавливается если оно входит в диапозон параметров
        /// от MinValue до _maxValue
        /// </summary>
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value.CompareTo(MinValue) < 0 
                    || value.CompareTo(MaxValue) > 0)
                {
                    throw new ArgumentException("Значение параметра " + 
                        _name + " должно находиться в диапозоне от " +
                        MinValue + " до " + _maxValue);
                }
                else
                {
                    _value = value;
                }
            }
        }

        /// <summary>
        /// Метод сравнивает
        /// полученный параметер
        /// с текущим
        /// </summary>
        /// <param name="obj">Объект сравнения</param>
        /// <returns>
        /// Результат сравнения
        /// true - объекты различны 
        /// false - объекты аналогичны
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Parameter<double> parameter = obj as Parameter<double>;
            if (parameter as Parameter<double> == null)
                return false;
            return parameter.MaxValue == Convert.ToDouble(this.MaxValue) 
                && parameter.MinValue == Convert.ToDouble(this.MinValue)
                && parameter.Value == Convert.ToDouble(this.Value); 
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
            MinValue = minValue;
            MaxValue = maxValue;
            _value = value;
            _name = name;
        }
    }
}
