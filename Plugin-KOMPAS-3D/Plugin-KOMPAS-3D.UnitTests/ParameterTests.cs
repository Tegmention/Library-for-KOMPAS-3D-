using NUnit.Framework;
using Parameters;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    public class ParameterTests
    {
        /// <summary>
        /// Поле хранит 
        /// значения параметра
        /// </summary>
        private Parameter<double> _parameter;

        [SetUp]
        public void CreateParameters()
        {
            _parameter = new Parameter<double>(10, 20, 10, "name");
        }

        [Test(Description = "Позитивный тест сеттера MaxValue")]
        public void Test_MaxValue_Set_CorrectValue()
        {
            var expected = 20;
            _parameter.MaxValue = expected;
            Assert.AreEqual(expected, _parameter.MaxValue, 
                "Сеттер MaxValue некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест сеттера MaxValue")]
        public void Test_MaxValue_Set_CorrectValue_LessMinValue()
        {
            var expected = 10;
            _parameter.MaxValue = 5;
            Assert.AreEqual(expected, _parameter.MaxValue, "" +
                "Сеттер MaxValue некорректно записывает значение " +
                "меньшее минимально возможного");
        }

        [Test(Description = "Позитивный тест геттера MaxValue")]
        public void Test_MaxValue_Get_CorrectValue()
        {
            var expected = 20;
            var actual = _parameter.MaxValue;
            Assert.AreEqual(expected, actual, 
                "Геттер MaxValue некорректно возвращащает значение");
        }

        [Test(Description = "Позитивный тест сеттера MinValue")]
        public void Test_MinValue_Set_CorrectValue()
        {
            var expected = 20;
            _parameter.MinValue = expected;
            Assert.AreEqual(expected, _parameter.MinValue, 
                "Сеттер MinValue некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест геттера MinValue")]
        public void Test_MinValue_Get_CorrectValue()
        {
            var expected = 10;
            var actual = _parameter.MinValue;
            Assert.AreEqual(expected, actual, 
                "Геттер MinValue некорректно возвращает значение");
        }

        [Test(Description = "Позитивный тест сеттера Value")]
        public void Test_Value_Set_CorrectValue()
        {
            var expected = 15;
            _parameter.Value = expected;
            Assert.AreEqual(expected, _parameter.Value, 
                "Сеттер Value некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест геттера Value")]
        public void Test_Value_Get_CorrectValue()
        {
            var expected = 10;
            var actual = _parameter.Value;
            Assert.AreEqual(expected, actual, 
                "Геттер Value некорректно возвращает значение");
        }

        [Test(Description = "Тест конструктора Parameter")]
        public void Test_Parameter_Designer()
        {
            string messege = "";
            var result = true;
            if(_parameter.MinValue != 10)
            {
                result = false;
                messege = "Ошибка при создании минимального " +
                    "значение параметра";
            }

            if (_parameter.MaxValue != 20)
            {
                result = false;
                messege = "Ошибка при создании максимального " +
                    "значение параметра";
            }

            if (_parameter.Value != 10)
            {
                result = false;
                messege = "Ошибка при создании текущего " +
                    "значение параметра";
            }
            Assert.IsTrue(result, messege);
        }

        [TestCase("-100", 
            "Должно возникать исключение если, записываемое значение меньше минимиального",
           TestName = "Присвоение значения меньше минимального")]
        [TestCase("100", 
            "Должно возникать исключение если, записываемое значение больше максимального",
           TestName = "Присвоение значения больше максимального")]
        public void TestLastModTimeSet_ArgumentException(string wrongLastModTime, string messege)
        {
            Assert.Throws<ArgumentException>(() => 
            { _parameter.Value = double.Parse(wrongLastModTime); }, messege);
        }
    }
}