using System.Collections.Generic;

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
        /// Устанавливает и возвращает словарь параметров модели
        /// </summary>
        //public Dictionary<string, Parameter<double>> Parameters { get;}

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

        /// <summary>
        /// Конструктор класса Parameters
        /// </summary>
        public ModelParameters()
        {
            _parameters = new Dictionary<ParametersName, Parameter<double>>();
            var values = new List<(double min, double max, ParametersName name)>
            {
                //+1 тест проверь функцию ToString!
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L),
                (60, 75, ParametersName.HS),
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
