using System.Collections.Generic;
using NUnit.Framework;
using Parameters;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelElementsTests
    {
        /// <summary>
        /// Поле хранит
        /// элементы модели
        /// </summary>
        private ModelElements _modelElements;

        [SetUp]
        public void CreateParameters()
        {
            _modelElements = new ModelElements();
        }

        [Test(Description = "Позитивный тест метода Element")]
        public void Test_Element()
        {
            var expected = new ModelElement(
                new List<(double min, double max, ParametersName name)>
                {
                    (10, 20, ParametersName.Diameter),
                    (12, 12, ParametersName.Length)
                },
                ElementFormKey.Circle
                );
            Assert.IsTrue(_modelElements.Element(ElementName.Rele)
                .Equals(expected),
                "Метод возвратил несоответствующий элемент");
        }

        [Test(Description = 
            "Негативный тест метода Element при запросе несуществующего элементе")]
        public void Test_Element_NotElement()
        {
            Assert.Throws<KeyNotFoundException>(() =>
            { _modelElements.Element(ElementName.SpeakerCover4); },
                    "Должно возникать исключение если, " +
                    "в словаре нет элемента с запрашиваемым именем");
        }

        [Test(Description = 
            "Позитивный тест метода IsElement для существующего элемента")]
        public void Test_IsElement_IsElement()
        {
            var expected = true;
            var actual = _modelElements.IsElement(ElementName.Case);
            Assert.AreEqual(expected, actual, "Метод IsElement работает некорректно");
        }

        [Test(Description = "Позитивный тест метода IsElement для несуществующего элемента")]
        public void Test_IsElement_NotElement()
        {
            var expected = false;
            var actual = _modelElements.IsElement(ElementName.SpeakerCover4);
            Assert.AreEqual(expected, actual, "Метод IsElement работает некорректно");
        }

        [Test(Description = "Позитивный тест метода AddElement")]
        public void Test_AddElement()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (200, 300, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
            _modelElements.AddElement(values, ElementName.SpeakerCover4, ElementFormKey.Rectangle);
            var expected = new ModelElement(values, ElementFormKey.Rectangle);
            Assert.IsTrue(_modelElements.Element(ElementName.SpeakerCover4)
                .Equals(expected),
                "Добавлен некорректный элемент");
        }

        [TestCase(2, TestName = "Добавление 2 динамика")]
        [TestCase(3, TestName = "Добавление 3 динамика")]
        [TestCase(4, TestName = "Добавление 4 динамика")]
        public void Test_Test_AddDynamic(int numberDynamic)
        {
            var expected = new ModelElements();
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic();
                expected.AddDynamic();
            }
            Assert.IsTrue(_modelElements.Equals(expected),
                "Ошибка при добавлении динамика");
        }

        [Test(Description = 
            "Позитивный тест метода AddDynamic при существовании 4 динамиков")]
        public void Test_AddDynamic_More4Dynamic()
        {
            while(_modelElements.NumberDynamics() != 4)
                _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            var expected = 4;
            var actual = _modelElements.NumberDynamics();
            Assert.AreEqual(expected, actual, 
                "Метод AddDynamic работает некорректно при существовании 4 динамиков");
        }

        [TestCase(2, TestName = "Удаление 2 динамика")]
        [TestCase(3, TestName = "Удаление 3 динамика")]
        [TestCase(4, TestName = "Удаление 4 динамика")]
        public void Test_DeleteDynamic(int numberDynamic)
        {
            var expected = new ModelElements();
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic();
                expected.AddDynamic();
            }
            _modelElements.DeleteDynamic();
            expected.DeleteDynamic();
            Assert.IsTrue(_modelElements.Equals(expected),
                "Ошибка при удалении динамика");
        }

        //
        [Test(Description = 
            "Позитивный тест метода DeleteDynamic при существовании 1 динамика")]
        public void Test_DeleteDynamic_Dynamic1()
        {
            _modelElements.DeleteDynamic();
            var expected = 1;
            var actual = _modelElements.NumberDynamics();
            Assert.AreEqual(expected, actual, 
                "Метод AddDynamic работает некорректно, при существовании 1 динамика");
        }

        [Test(Description = "Позитивный тест метода DeleteElement")]
        public void Test_DeleteElement()
        { 
            _modelElements.DeleteElement(ElementName.Case);
            var expected = false;
            var actual = _modelElements.IsElement(ElementName.Case);
            Assert.AreEqual(expected, actual, "Метод AddElement не удаляет элемент");
        }

        [TestCase(ElementName.SpeakerCover1, 1, 75,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 1 динамика")]
        [TestCase(ElementName.SpeakerCover1, 2, 65,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 2 динамика")]
        [TestCase(ElementName.SpeakerCover2, 2, 65,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 2 динамика")]
        [TestCase(ElementName.SpeakerCover1, 3, 55,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover2, 3, 55,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover3, 3, 55,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 3 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover1, 4, 45,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover2, 4, 45,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3, 4, 45,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 3 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3, 4, 45,
           TestName = "Позитивный тест " +
            "метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 4 при существовании 4 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic4(ElementName name,
            int numberDynamic, double maxValue)
        {
            while (_modelElements.NumberDynamics() != numberDynamic)
                _modelElements.AddDynamic();
            _modelElements.CalculationMaxHeightDynamic(name);
            var actual = _modelElements.Element(name).
                Parameter(ParametersName.Height).MaxValue;
            var expected = maxValue;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxHeightDynamic " +
                "не расчитывает максимальное значение высоты" +
                "для "+ numberDynamic +" динамиков");
        }

        [TestCase(1, 75, ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 1 динамика")]
        [TestCase(2, 65, ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 2 динамиков")]
        [TestCase(3, 55, ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 3 динамиков")] 
        [TestCase(4, 45, ElementName.SpeakerCover4,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 4 динамиков")]
        public void Test_CalculationMaxHeightDynamic(int numberDynamic, double maxValue,
            ElementName elementName)
        {
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic(); 
            }
            var expected = new Parameter<double>(10, maxValue, 10, "name");
            _modelElements.CalculationMaxDynamics();
            Assert.IsTrue(_modelElements.Element(elementName).
                Parameter(ParametersName.Height).Equals(expected), 
                "Метод AddDynamic некорректно расчитывает значение высоты при " +
                +numberDynamic + " динамике(ах)");
        }

        [Test(Description = "Позитивный тест метода ChangeForm")]
        public void Test_ChangeForm()
        {
            _modelElements.ChangeForm(ElementName.SpeakerCover1);
            var actual = _modelElements
                .Element(ElementName.SpeakerCover1).FormKey();
            var expected = ElementFormKey.Circle;
            Assert.AreEqual(actual, expected,
                "Метод ChangeForm некорректно изменяет параметры элемента");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxWidthDynamic")]
        public void Test_CalculationMaxWidthDinamic()
        {
            var expected = new ModelElements();
            while(_modelElements.NumberDynamics() != 4)
            {
                _modelElements.AddDynamic();
                expected.AddDynamic();
            }
            _modelElements.CalculationMaxWidthDynamic();
            Assert.IsTrue(_modelElements.Equals(expected), 
                "Метод ChangeForm некорректно расчитывает ширину динамиков");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxDinamics")]
        public void Test_CalculationMaxDynamics()
        {
            var expected = 75;
            var actual = _modelElements.CalculationMaxDynamics();
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxDinamics некорректно " +
                "расчитывает сумму высот динамиков");
        }

        [Test(Description = "Позитивный тест метода NumberDynamics")]
        public void Test_NumberDynamics()
        {
            var expected = 1;
            var actual = _modelElements.NumberDynamics();
            Assert.AreEqual(expected, actual, 
                "Метод NumberDinamics некорректно расчитывает число динамиков");
        }

        [Test(Description = "Позитивный тест метода Equal")]
        public void Test_Equal()
        {
            var expected = new ModelElements();
            Assert.IsTrue(_modelElements.Equals(expected),
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Эквивалентность разных элементов")]
        public void Test_Equal_NotEqual()
        {
            _modelElements.AddDynamic();
            var expected = new ModelElements();
            Assert.IsFalse(_modelElements.Equals(expected),
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Позитивный тест конструктора ModelElements")]
        public void Test_ModelElements()
        {
            var expected = new ModelElements();
            Assert.IsTrue(_modelElements.Equals(expected), 
                "Ошибка при создании нового элемента");
        }
    }
}
