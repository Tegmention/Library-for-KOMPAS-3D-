using NUnit.Framework;
using Parameters;
using System.Collections.Generic;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelParametersTests
    {
        [Test(Description = "Позитивный тест сеттера Parameters")]
        public void Test_Parameters_Set_CorrectValue()
        {
            var modelParameters = new ModelParameters();
            var expected = new Dictionary<string, Parameter<double>>();
            var parameter = new Parameter<double>(100, 500, 100, "name");
            expected.Add("name", parameter);
            modelParameters.Parameters = expected;
            Assert.AreEqual(expected, modelParameters.Parameters, "Сеттер Parameters некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест геттера Parameters")]
        public void Test_Parameters_Get_CorrectValue()
        {
            var modelParameters = new ModelParameters();
            var expected = new Dictionary<string, Parameter<double>>();
            var parameter = new Parameter<double>(100, 500, 100, "name");
            expected.Add("name", parameter);
            modelParameters.Parameters = expected;
            var actual = modelParameters.Parameters;
            Assert.AreEqual(expected, actual, "Геттер Parameters некорректно возвращащает значение");
        }

        [Test(Description = "Позитивный тест метода CalculateMaxHeightDinamic")]
        public void Test_CalculateMaxHeightDinamic()
        {
            var modelParameters = new ModelParameters();
            var H = 500;
            var D = 20;
            var expected = H - 5 - (D + 10);
            modelParameters.Parameters[ParametersName.H.ToString()].Value = H;
            modelParameters.Parameters[ParametersName.D.ToString()].Value = D;
            modelParameters.CalculateMaxHeightDinamic();
            var actual = modelParameters.Parameters[ParametersName.HS.ToString()].MaxValue;
            Assert.AreEqual(expected, actual, "Метод CalculateMaxHeightDinamic работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CalculateMaxLenghtDinamic")]
        public void Test_CalculateMaxLenghtDinamic()
        {
            var modelParameters = new ModelParameters();
            var L = 300;
            var expected = L - 5;
            modelParameters.Parameters[ParametersName.L.ToString()].Value = L;
            modelParameters.CalculateMaxLenghtDinamic();
            var actual = modelParameters.Parameters[ParametersName.LS.ToString()].MaxValue;
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
            var parameters = modelParameters.Parameters;
            var result = true;
            var values = new List<(double min, double max, string name)>
            {
                (100, 500, ParametersName.H.ToString()),
                (200, 300, ParametersName.L.ToString()),
                (150, 200, ParametersName.W.ToString()),
                (60, 75, ParametersName.HS.ToString()),
                (10, 20, ParametersName.D.ToString()),
                (5, 20, ParametersName.WS.ToString()),
                (150, 195, ParametersName.LS.ToString())
            };

            foreach (var value in values)
            {
                if (parameters[value.name].MaxValue != value.max ||
                    parameters[value.name].MinValue != value.min ||
                    parameters[value.name].Value != value.min)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Конструктор ModelParameters не создает корректный экземпляр класса");
        }
    }
}
