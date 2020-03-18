using NUnit.Framework;
using Parameters;
using System.Collections.Generic;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelParametersTests
    {

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var modelParameters = new ModelParameters();
            var result = true;
            if(modelParameters.Parameter(ParametersName.H).MinValue != 100
                || modelParameters.Parameter(ParametersName.H).MaxValue != 500
                || modelParameters.Parameter(ParametersName.H).Value != 100)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод Parameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CalculateMaxHeightDinamic")]
        public void Test_CalculateMaxHeightDinamic()
        {
            var modelParameters = new ModelParameters();
            var H = 500;
            var D = 20;
            var expected = H - 5 - (D + 10);
            modelParameters.Parameter(ParametersName.H).Value = H;
            modelParameters.Parameter(ParametersName.D).Value = D;
            modelParameters.CalculateMaxHeightDinamic();
            var actual = modelParameters.Parameter(ParametersName.HS).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CalculateMaxHeightDinamic работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CalculateMaxLenghtDinamic")]
        public void Test_CalculateMaxLenghtDinamic()
        {
            var modelParameters = new ModelParameters();
            var L = 300;
            var expected = L - 5;
            modelParameters.Parameter(ParametersName.W).Value = L;
            modelParameters.CalculateMaxLenghtDinamic();
            var actual = modelParameters.Parameter(ParametersName.WS).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CalculateMaxLenghtDinamic работает некорректно");
        }

        [Test(Description = "Позитивный тест метода перечисления ToString")]
        public void Test_ToString()
        {
            var expected = "H";
            var actual = ParametersName.H.ToString();
            Assert.AreEqual(expected, actual, "Метод перечисления ToString работает некорректно");
        }

        [Test(Description = "Позитивный тест конструктора ModelParameters")]
        public void Test_ModelParameters()
        {
            var modelParameters = new ModelParameters();
            var result = true;
            var values = new List<(double min, double max, ParametersName name)>
            {
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
                if (modelParameters.Parameter(value.name).MaxValue != value.max ||
                    modelParameters.Parameter(value.name).MinValue != value.min ||
                    modelParameters.Parameter(value.name).Value != value.min)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Конструктор ModelParameters не создает корректный экземпляр класса");
        }
    }
}
