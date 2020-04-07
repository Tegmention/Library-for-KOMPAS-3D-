using System.Collections.Generic;
using NUnit.Framework;
using Parameters;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelElementTests
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
                (100, 600, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
        }

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
            var result = true;
            string message = "";
            if (modelElement.Parameter(ParametersName.Height).MinValue != 100)
            {
                result = false;
                message = "Ошибка при создании минимальной " +
                    "высоты элемента";
            }
            if(modelElement.Parameter(ParametersName.Height).MaxValue != 500)
            {
                result = false;
                message = "Ошибка при создании минимальной " +
                    "высоты элемента";
            }
            if(modelElement.Parameter(ParametersName.Height).Value != 100)
            {
                result = false;
                message = "Ошибка при создании текущей " +
                    "высоты элемента";
            }
            Assert.IsTrue(result, message);
        }

        [Test(Description = "Запрос параметра отстутствующего в словаре")]
        public void TestParameter_KeyNotFoundException()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
                Assert.Throws<KeyNotFoundException>(() => { modelElement.Parameter(ParametersName.Diameter); },
                    "Должно возникать исключение если, в словаре нет параметра с запрашиваемым именем");
        }

        [Test(Description = "Позитивный тест метода FormKey")]
        public void Test_FormKey()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
            var expected = ElementFormKey.Rectangle;
            var actual = modelElement.FormKey();
            Assert.AreEqual(expected, actual, "Метод FormKey работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при прямоугольной форме")]
        public void Test_CircleParameterRectangleForm()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
            modelElement.CircleParameter();
            var expected = 600;
            var actual = modelElement.Parameter(ParametersName.Width).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при круглой форме")]
        public void Test_CircleParameterCircleForm()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Circle);
            modelElement.CircleParameter();
            var expected = 500;
            var actual = modelElement.Parameter(ParametersName.Width).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода ChangeForm при прямоугольной форме")]
        public void Test_ChangeFormRectangleForm()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
            modelElement.ChangeForm();
            var result = true;
            string message = "";
            if (modelElement.FormKey() != ElementFormKey.Circle)
            {
                result = false;
                message = "Ошибка при изменении ключа формы";
            }
            if (modelElement.Parameter(ParametersName.Width).MaxValue != 500)
            {
                result = false;
                message = "Ошибка при перерасчете параметров " +
                    "элемента";
            }
            Assert.IsTrue(result, message);
        }

        [Test(Description = "Позитивный тест метода ChangeForm при круглой форме")]
        public void Test_ChangeFormCirckleForm()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Circle);
            modelElement.ChangeForm();
            var expected = ElementFormKey.Rectangle;
            var actual = modelElement.FormKey();
            Assert.AreEqual(expected, actual, "Метод ChangeForm работает некорректно");
        }

        [Test(Description = "Позитивный тест конструктора ModelElement")]
        public void Test_ModelParameters()
        {
            var modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
            var values = new List<(double min, double max, ParametersName name)>
            {
                (modelElement.Parameter(ParametersName.Height).MinValue,
                modelElement.Parameter(ParametersName.Height).MaxValue,
                ParametersName.Height),
                (modelElement.Parameter(ParametersName.Width).MinValue,
                modelElement.Parameter(ParametersName.Width).MaxValue,
                ParametersName.Width),
                (modelElement.Parameter(ParametersName.Length).MinValue,
                modelElement.Parameter(ParametersName.Length).MaxValue, 
                ParametersName.Length)
            };
            CollectionAssert.AreEqual(_values, values);
        }
    }
}
