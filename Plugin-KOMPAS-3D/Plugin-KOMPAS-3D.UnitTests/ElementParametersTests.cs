using NUnit.Framework;
using Parameters;
using System.Collections.Generic;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ElementParametersTests
    {
        /// <summary>
        /// Поле хранит данные
        /// параметров
        /// </summary>
        private List<(double min, double max, ParametersName name)> _values;

        /// <summary>
        /// Поле хранит параметры
        /// элемента модели
        /// </summary>
        private ElementParameters _elementParameters;

        /// <summary>
        /// Хранит данные параметра
        /// </summary>
        private Parameter<double> _parameter;

        [SetUp]
        public void CreateParameters()
        {
            _values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (100, 500, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
            _elementParameters = new ElementParameters(_values);
            _parameter = new Parameter<double>(100, 500, 100, "name");
        }

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            Assert.IsTrue(_elementParameters.
                Parameter(ParametersName.Height).Equals(_parameter),
                "Был возвращен некорректный элемент");
        }

        [Test(Description = "Запрос отсутствующего параметра" +
            "в методе Parameter")]
        public void Test_Parameter_NotParameter()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            { _elementParameters.Parameter(ParametersName.Diameter); },
                    "Должно возникать исключение если, в словаре нет параметра с запрашиваемым именем");
        }

        [Test(Description = "Позитивный тест метода перечисления ToString")]
        public void Test_ToString()
        {
            var expected = "Height";
            var actual = ParametersName.Height.ToString();
            Assert.AreEqual(expected, actual, 
                "Метод перечисления ToString работает некорректно");
        }

        [Test(Description = 
            "Позитивный тест метода CalculationCircleParameter при maxWidth > maxHeight")]
        public void Test_CalculationCircleParameterWidthMoreHeight()
        {
            _elementParameters.CalculationCircleParameter();
            Assert.IsTrue(_elementParameters.
                Parameter(ParametersName.Width).Equals(_parameter),
                "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = 
            "Позитивный тест метода CalculationCircleParameter при maxHeight > maxWidth")]
        public void Test_CalculationCircleParameterHeightMoreWidth()
        {
            _elementParameters.Parameter(ParametersName.Height).MaxValue = 700;
            _elementParameters.CalculationCircleParameter();
            Assert.IsTrue(_elementParameters.
                Parameter(ParametersName.Height).Equals(_parameter),
                "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода Equals")]
        public void Test_Equals()
        {
            var expected = new ElementParameters(_values);
            Assert.IsTrue(expected.Equals(_elementParameters),
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Эквивалентность разных элементов")]
        public void Test_Equals_NotEqual()
        {
            var expected = new ElementParameters(_values);
            _elementParameters.Parameter(ParametersName.Height).MaxValue
                = 800;
            Assert.IsFalse(expected.Equals(_elementParameters),
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Позитивный тест конструктора ElementParameters")]
        public void Test_ElementParameters()
        {
            var expected = new ElementParameters(_values);
            Assert.IsTrue(expected.Equals(_elementParameters), 
                "Конструктор ElementParameters не создает корректный экземпляр класса");
        }
    }
}
