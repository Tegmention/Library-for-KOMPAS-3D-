using NUnit.Framework;
using Parameters;
using System.Collections.Generic;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ElementParametersTests
    {

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H)
            };
            var elementParameters = new ElementParameters(values);
            var result = true;
            if (elementParameters.Parameter(ParametersName.H).MinValue != 100
                || elementParameters.Parameter(ParametersName.H).MaxValue != 500
                || elementParameters.Parameter(ParametersName.H).Value != 100)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод Parameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода перечисления ToString")]
        public void Test_ToString()
        {
            var expected = "H";
            var actual = ParametersName.H.ToString();
            Assert.AreEqual(expected, actual, "Метод перечисления ToString работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CalculationCircleParameter при maxW > maxH")]
        public void Test_CalculationCircleParameterWMoreH()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 600, ParametersName.W)
            };
            var elementParameters = new ElementParameters(values);
            elementParameters.CalculationCircleParameter();
            var expected = 500;
            var actual = elementParameters.Parameter(ParametersName.W).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CalculationCircleParameter при maxH > maxW")]
        public void Test_CalculationCircleParameterHMoreW()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 600, ParametersName.H),
                (200, 500, ParametersName.W)
            };
            var elementParameters = new ElementParameters(values);
            elementParameters.CalculationCircleParameter();
            var expected = 500;
            var actual = elementParameters.Parameter(ParametersName.H).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CalculationCircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест конструктора ElementParameters")]
        public void Test_ElementParameters()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };
            var elementParameters = new ElementParameters(values);
            var result = true;
            var valuesOne = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };

            foreach (var value in valuesOne)
            {
                if (elementParameters.Parameter(value.name).MaxValue != value.max ||
                    elementParameters.Parameter(value.name).MinValue != value.min ||
                    elementParameters.Parameter(value.name).Value != value.min)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Конструктор ElementParameters не создает корректный экземпляр класса");
        }
    }
}
