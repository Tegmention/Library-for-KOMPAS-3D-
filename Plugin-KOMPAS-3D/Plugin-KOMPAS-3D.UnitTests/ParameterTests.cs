using NUnit.Framework;
using Parameters;
using System;

namespace Plugin_KOMPAS_3D.UnitTests
{
    public class ParameterTests
    {
        [Test(Description = "Позитивный тест сеттера MaxValue")]
        public void Test_MaxValue_Set_CorrectValue()
        {
            var parameter = new Parameter<double>(10,10,10,"name");
            var expected = 20;
            parameter.MaxValue = expected;
            Assert.AreEqual(expected, parameter.MaxValue, "Сеттер MaxValue некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест сеттера MaxValue")]
        public void Test_MaxValue_Set_CorrectValue_LessMinValue()
        {
            var parameter = new Parameter<double>(10, 10, 5, "name");
            var expected = 10;
            Assert.AreEqual(expected, parameter.MaxValue, "Сеттер MaxValue некорректно записывает значение " +
                "меньшее минимально возможного");
        }

        [Test(Description = "Позитивный тест геттера MaxValue")]
        public void Test_MaxValue_Get_CorrectValue()
        {
            var parameter = new Parameter<double>(10, 10, 10, "name");
            var expected = 20;
            parameter.MaxValue = expected;
            var actual = parameter.MaxValue;
            Assert.AreEqual(expected, actual, "Геттер MaxValue некорректно возвращащает значение");
        }

        [Test(Description = "Позитивный тест сеттера MinValue")]
        public void Test_MinValue_Set_CorrectValue()
        {
            var parameter = new Parameter<double>(10, 10, 10, "name");
            var expected = 20;
            parameter.MinValue = expected;
            Assert.AreEqual(expected, parameter.MinValue, "Сеттер MinValue некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест геттера MinValue")]
        public void Test_MinValue_Get_CorrectValue()
        {
            var parameter = new Parameter<double>(10, 10, 10, "name");
            var expected = 20;
            parameter.MinValue = expected;
            var actual = parameter.MinValue;
            Assert.AreEqual(expected, actual, "Геттер MinValue некорректно возвращает значение");
        }

        [Test(Description = "Позитивный тест сеттера Value")]
        public void Test_Value_Set_CorrectValue()
        {
            var parameter = new Parameter<double>(10, 30, 10, "name");
            var expected = 20;
            parameter.Value = expected;
            Assert.AreEqual(expected, parameter.Value, "Сеттер Value некорректно записывает значение");
        }

        [Test(Description = "Позитивный тест геттера Value")]
        public void Test_Value_Get_CorrectValue()
        {
            var parameter = new Parameter<double>(10, 30, 10, "name");
            var expected = 20;
            parameter.Value = expected;
            var actual = parameter.Value;
            Assert.AreEqual(expected, actual, "Геттер Value некорректно возвращает значение");
        }

        [Test(Description = "Тест конструктора Parameter")]
        public void Test_Parameter_Designer()
        {
            double[] expected = { 10, 20, 15};
            var parameter = new Parameter<double>(
                expected[0],
                expected[1],
                expected[2],
                "name"
                );
            object[] actual = {
                parameter.MinValue,
                parameter.MaxValue,
                parameter.Value
                };
            Assert.AreEqual(expected, actual, "Конструктор Parameter создает некорректный экземпляр");
        }

        [TestCase("-100", "Должно возникать исключение если, записываемое значение меньше минимиального",
           TestName = "Присвоение значения меньше минимального")]
        [TestCase("100", "Должно возникать исключение если, записываемое значение больше максимального",
           TestName = "Присвоение значения больше максимального")]
        public void TestLastModTimeSet_ArgumentException(string wrongLastModTime, string messege)
        {
            var parameter = new Parameter<double>(10, 30, 10, "name");
            Assert.Throws<ArgumentException>(() => { parameter.Value = double.Parse(wrongLastModTime); }, messege);
        }
    }
}