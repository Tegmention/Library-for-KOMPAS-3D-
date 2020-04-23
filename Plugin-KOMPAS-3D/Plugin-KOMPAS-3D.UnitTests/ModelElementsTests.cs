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
            var result = true;
            var element = _modelElements.Element(ElementName.Rele);
            var message = "";
            if (element.Parameter(ParametersName.Diameter).Value != 10)
            {
                result = false;
                message = "Метод некорректно возвращает " +
                    "значение Diameter";
            }
            if (element.Parameter(ParametersName.Length).Value != 12)
            {
                result = false;
                message = "Метод некорректно возвращает " +
                    "значение Length";
            }
            Assert.IsTrue(result, message);
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
            var result = true;
            var element = _modelElements.Element(ElementName.SpeakerCover4);
            string message = "";
            if (element.Parameter(ParametersName.Height).Value != 100)
            {
                result = false;
                message = "Ошибка при создании параметра Height" +
                    "нового элемента";
            }
            if (element.Parameter(ParametersName.Length).Value != 150)
            {
                result = false;
                message = "Ошибка при создании параметра Length" +
                    "нового элемента";
            }
            if (element.Parameter(ParametersName.Width).Value != 200)
            {
                result = false;
                message = "Ошибка при создании параметра Width" +
                    "нового элемента";
            }
            Assert.IsTrue(result, message);
        }

        [TestCase(2, 65, TestName = "Добавление 2 динамика")]
        [TestCase(3, 55, TestName = "Добавление 3 динамика")]
        [TestCase(4, 45, TestName = "Добавление 4 динамика")]
        public void Test_Test_AddDynamic(int numberDynamic, double maxHeight)
        {
            var elements = new List<ModelElement> { };
            elements.Add(_modelElements.Element(ElementName.SpeakerCover1));
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic();
                if (_modelElements.NumberDynamics() == 2)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover2));
                if (_modelElements.NumberDynamics() == 3)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover3));
                if (_modelElements.NumberDynamics() == 4)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover4));
            }
            var result = true;
            string message = "";
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.Height).MinValue != 10)
                {
                    result = false;
                    message = "Ошибка при создании минимального значения" +
                        "параметра Height";
                }
                if (element.Parameter(ParametersName.Length).MinValue != 5)
                {
                    result = false;
                    message = "Ошибка при создании минимального значения" +
                        "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).MinValue != 150)
                {
                    result = false;
                    message = "Ошибка при создании минимального значения" +
                        "параметра Width";
                }
                if (element.Parameter(ParametersName.Height).Value != 10)
                {
                    result = false;
                    message = "Ошибка при создании текущего значения" +
                        "параметра Height";
                }
                if (element.Parameter(ParametersName.Length).Value != 5)
                {
                    result = false;
                    message = "Ошибка при создании текущего значения" +
                         "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).Value != 150)
                {
                    result = false;
                    message = "Ошибка при создании текущего значения" +
                        "параметра Width";
                }
                if (element.Parameter(ParametersName.Height).MaxValue != maxHeight)
                {
                    result = false;
                    message = "Ошибка при создании максимального значения" +
                        "параметра Height";
                }
                if (element.Parameter(ParametersName.Length).MaxValue != 20)
                {
                    result = false;
                    message = "Ошибка при создании максимального значения" +
                        "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).MaxValue != 195)
                {
                    result = false;
                    message = "Ошибка при создании максимального значения" +
                        "параметра Width";
                }
            }
            Assert.IsTrue(result, message);
        }

        [Test(Description = 
            "Позитивный тест метода AddDynamic при существовании 4 динамиков")]
        public void Test_AddDynamic_More4Dynamic()
        {
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            var expected = 4;
            var actual = _modelElements.NumberDynamics();
            Assert.AreEqual(expected, actual, 
                "Метод AddDynamic работает некорректно при существовании 4 динамиков");
        }

        [TestCase(2, 75, TestName = "Удаление 2 динамика")]
        [TestCase(3, 65, TestName = "Удаление 3 динамика")]
        [TestCase(4, 55, TestName = "Удаление 4 динамика")]
        public void Test_DeleteDynamic(int numberDynamic, double maxHeight)
        {
            var elements = new List<ModelElement> { };
            elements.Add(_modelElements.Element(ElementName.SpeakerCover1));
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic();
                if (_modelElements.NumberDynamics() == 3)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover2));
                if (_modelElements.NumberDynamics() == 4)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover3));
            }
            _modelElements.DeleteDynamic();
            var result = true;
            string message = "";
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.Height).MinValue != 10)
                {
                    result = false;
                    message = "Ошибка при перерасчете минимального значения " +
                        "параметра Height";
                }
                if (element.Parameter(ParametersName.Length).MinValue != 5)
                {
                    result = false;
                    message = "Ошибка при перерасчете минимального значения " +
                        "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).MinValue != 150)
                {
                    result = false;
                    message = "Ошибка при перерасчете минимального значения " +
                        "параметра Width";
                }
                if (element.Parameter(ParametersName.Height).Value != 10)
                {
                    result = false;
                    message = "Ошибка при перерасчете текущего значения " +
                        "параметра Height";
                }
                if (element.Parameter(ParametersName.Length).Value != 5)
                {
                    result = false;
                    message = "Ошибка при перерасчете текущего значения " +
                         "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).Value != 150)
                {
                    result = false;
                    message = "Ошибка при перерасчете текущего значения " +
                        "параметра Width";
                }
                if (element.Parameter(ParametersName.Height).MaxValue != maxHeight)
                {
                    result = false;
                    message = "Ошибка при перерасчете максимального значения " +
                        "параметра Height" + element.Parameter(ParametersName.Height).MaxValue;
                }
                if (element.Parameter(ParametersName.Length).MaxValue != 20)
                {
                    result = false;
                    message = "Ошибка при перерасчете максимального значения " +
                        "параметра Length";
                }
                if (element.Parameter(ParametersName.Width).MaxValue != 195)
                {
                    result = false;
                    message = "Ошибка при перерасчете максимального значения " +
                        "параметра Width";
                }
            }
            Assert.IsTrue(result, message);
        }

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

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 1 динамика")]
        public void Test_CalculationMaxHeightDynamic_Dynamic1(ElementName name)
        {
            _modelElements.CalculationMaxHeightDynamic(name);
            var actual = _modelElements.Element(name).
                Parameter(ParametersName.Height).MaxValue;
            var expected = 75;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxHeightDynamic не расчитывает максимальное значение высоты" +
                "для 1 динамика");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 2 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 2 динамика")]
        public void Test_CalculationMaxHeightDinamic_Dynamic2(ElementName name)
        {
            _modelElements.AddDynamic();
            _modelElements.CalculationMaxHeightDynamic(name);
            var actual = _modelElements.Element(name).
                Parameter(ParametersName.Height).MaxValue;
            var expected = 65;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxHeightDynamic не расчитывает максимальное значение высоты" +
                "для двух динамиков");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 3 при существовании 3 динамика")]
        public void Test_CalculationMaxHeightDynamic_Dynamic3(ElementName name)
        {
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.CalculationMaxHeightDynamic(name);
            var actual = _modelElements.Element(name).
                Parameter(ParametersName.Height).MaxValue;
            var expected = 55;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxHeightDynamic не расчитывает максимальное значение высоты" +
                "для трех динамиков");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 1 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 2 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 3 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHeightDynamic для расчета " +
            "высоты динамика 4 при существовании 4 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic4(ElementName name)
        {
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.CalculationMaxHeightDynamic(name);
            var actual = _modelElements.Element(name).
                Parameter(ParametersName.Height).MaxValue;
            var expected = 45;
            Assert.AreEqual(expected, actual, 
                "Метод CalculationMaxHeightDynamic не расчитывает максимальное значение высоты" +
                "для четырех динамиков");
        }

        [TestCase(1, 75,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 1 динамика")]
        [TestCase(2, 65,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 2 динамиков")]
        [TestCase(3, 55,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 3 динамиков")]
        [TestCase(4, 45,
           TestName = "Позитивный тест метода " +
            "CalculationMaxDynamic при существовании 4 динамиков")]
        public void Test_CalculationMaxHeightDynamic(int numberDynamic, double maxValue)
        {
            var elements = new List<ModelElement> { };
            while (_modelElements.NumberDynamics() != numberDynamic)
            {
                _modelElements.AddDynamic();
                if (_modelElements.NumberDynamics() == 2)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover2));
                if (_modelElements.NumberDynamics() == 3)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover3));
                if (_modelElements.NumberDynamics() == 4)
                    elements.Add(_modelElements.Element(ElementName.SpeakerCover4));
            }
            _modelElements.CalculationMaxDynamics();
            var result = true;
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.Height).MaxValue != maxValue)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, 
                "Метод AddDynamic некорректно расчитывает значение высоты при " +
                +numberDynamic + " динамике(ах)");
        }

        [Test(Description = "Позитивный тест метода ChangeForm")]
        public void Test_ChangeForm()
        {
            _modelElements.ChangeForm(ElementName.SpeakerCover1);
            var element = _modelElements.Element(ElementName.SpeakerCover1);
            var result = true;
            if( element.FormKey() != ElementFormKey.Circle)
            {
                result = false;
            }
            Assert.IsTrue(result, 
                "Метод ChangeForm некорректно изменяет параметры элемента");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxWidthDynamic")]
        public void Test_CalculationMaxWidthDinamic()
        {
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.AddDynamic();
            _modelElements.CalculationMaxWidthDynamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                _modelElements.Element(ElementName.SpeakerCover1),
                _modelElements.Element(ElementName.SpeakerCover2),
                _modelElements.Element(ElementName.SpeakerCover3),
                _modelElements.Element(ElementName.SpeakerCover4)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.Width).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, 
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

        [Test(Description = "Позитивный тест конструктора ModelElements")]
        public void Test_ModelElements()
        {
            var result = true;
            var message = "";
            var elementCase = _modelElements.Element(ElementName.Case);
            if (elementCase.Parameter(ParametersName.Height).MinValue != 100)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения высоты" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Length).MinValue != 150)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения длины" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Width).MinValue != 200)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения ширины" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Height).Value != 100)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения высоты" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Length).Value != 150)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения длины" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Width).Value != 200)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения ширины" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Height).MaxValue != 500)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения высоты" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Length).MaxValue != 200)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения длины" +
                    "корпуса";
            }
            if (elementCase.Parameter(ParametersName.Width).MaxValue != 300)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения ширины" +
                    "корпуса";
            }

            var elementRele = _modelElements.Element(ElementName.Rele);
            if (elementRele.Parameter(ParametersName.Diameter).MinValue != 10)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения диаметра" +
                    "реле регулировки";
            }
            if (elementRele.Parameter(ParametersName.Length).MinValue != 12)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения длины" +
                    "реле регулировки";
            }
            if (elementRele.Parameter(ParametersName.Diameter).Value != 10)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения диаметра" +
                    "реле регулировки";
            }
            if (elementRele.Parameter(ParametersName.Length).Value != 12)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения длины" +
                    "реле регулировки";
            }
            if (elementRele.Parameter(ParametersName.Diameter).MaxValue != 20)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения диаметра" +
                    "реле регулировки";
            }
            if (elementRele.Parameter(ParametersName.Length).MaxValue != 12)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения длины" +
                    "реле регулировки";
            }

            var elementSpeakerCover1 = 
                _modelElements.Element(ElementName.SpeakerCover1);
            if (elementSpeakerCover1.Parameter(ParametersName.Height).MinValue != 10)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения высоты" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Length).MinValue != 5)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения длины" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Width).MinValue != 150)
            {
                result = false;
                message = 
                    "Ошибка при создании минимального значения ширины" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Height).Value != 10)
            {
                result = false; message = 
                    "Ошибка при создании текущего значения высоты" +
                     "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Length).Value != 5)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения длины" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Width).Value != 150)
            {
                result = false;
                message = 
                    "Ошибка при создании текущего значения ширины" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Height).MaxValue != 75)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения высоты" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Length).MaxValue != 20)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения длины" +
                    "динамика";
            }
            if (elementSpeakerCover1.Parameter(ParametersName.Width).MaxValue != 195)
            {
                result = false;
                message = 
                    "Ошибка при создании максимального значения ширины" +
                    "динамика";
            }
            Assert.IsTrue(result, message);
        }
    }
}
