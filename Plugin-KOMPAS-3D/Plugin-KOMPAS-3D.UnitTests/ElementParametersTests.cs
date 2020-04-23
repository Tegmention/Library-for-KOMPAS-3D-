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

        [SetUp]
        public void CreateParameters()
        {
            _values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (200, 600, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
        }

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var elementParameters = new ElementParameters(_values);
            var result = true;
            string message = "";
            if (elementParameters.Parameter(ParametersName.Height).MinValue != 100)
            {
                result = false;
                message = "Ошибка при создании минимальной " +
                   "высоты элемента";
            }
            if (elementParameters.Parameter(ParametersName.Height).MaxValue != 500)
            {
                result = false;
                message = "Ошибка при создании максимальной " +
                    "высоты элемента";
            }
            if (elementParameters.Parameter(ParametersName.Height).Value != 100)
            {
                result = false;
                message = "Ошибка при создании текущей " +
                    "высоты элемента";
            }
            Assert.IsTrue(result, message);
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
            var elementParameters = new ElementParameters(_values);
            elementParameters.CalculationCircleParameter();
            var expected = 500;
            var actual = elementParameters.Parameter(ParametersName.Width).MaxValue;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = 
            "Позитивный тест метода CalculationCircleParameter при maxHeight > maxWidth")]
        public void Test_CalculationCircleParameterHeightMoreWidth()
        {
            var elementParameters = new ElementParameters(_values);
            elementParameters.Parameter(ParametersName.Height).MaxValue = 700;
            elementParameters.CalculationCircleParameter();
            var expected = 600;
            var actual = 
                elementParameters.Parameter(ParametersName.Height).MaxValue;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест конструктора ElementParameters")]
        public void Test_ElementParameters()
        {
            var elementParameters = new ElementParameters(_values);
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (200, 600, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
            Assert.AreEqual(_values, values, 
                "Конструктор ElementParameters не создает корректный экземпляр класса");
        }
    }
}
