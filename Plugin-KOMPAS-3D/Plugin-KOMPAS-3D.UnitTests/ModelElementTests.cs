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

        private ModelElement _modelElement;

        [SetUp]
        public void CreateParameters()
        {
            _values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (100, 600, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
            _modelElement = new ModelElement(_values, ElementFormKey.Rectangle);
        }

        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var expected = new Parameter<double>(100, 500, 100, "name");
            Assert.IsTrue(expected.
                Equals(_modelElement.Parameter(ParametersName.Height)));
        }

        [Test(Description = "Запрос параметра отстутствующего в словаре")]
        public void TestParameter_KeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => 
            { _modelElement.Parameter(ParametersName.Diameter); },
                    "Должно возникать исключение если, в словаре нет параметра с запрашиваемым именем");
        }

        [Test(Description = "Позитивный тест метода FormKey")]
        public void Test_FormKey()
        {
            var expected = ElementFormKey.Rectangle;
            var actual = _modelElement.FormKey();
            Assert.AreEqual(expected, actual, "Метод FormKey работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при прямоугольной форме")]
        public void Test_CircleParameterRectangleForm()
        {
            _modelElement.CircleParameter();
            var expected = 600;
            var actual = _modelElement.Parameter(ParametersName.Width).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при круглой форме")]
        public void Test_CircleParameterCircleForm()
        {
            _modelElement.ChangeForm();
            _modelElement.CircleParameter();
            var expected = 500;
            var actual = _modelElement.Parameter(ParametersName.Width).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода ChangeForm при прямоугольной форме")]
        public void Test_ChangeFormRectangleForm()
        {
            _modelElement.ChangeForm();
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.Height),
                (100, 500, ParametersName.Width),
                (150, 200, ParametersName.Length)
            };
            var expected = 
                new ModelElement(values, ElementFormKey.Circle);
            Assert.IsTrue(expected.Equals(_modelElement),
                "Ошибка при изменении формы с прямоугольной на круглую");
        }

        [Test(Description = "Позитивный тест метода ChangeForm при круглой форме")]
        public void Test_ChangeFormCirckleForm()
        {
            _modelElement.ChangeForm();
            _modelElement.ChangeForm();
            var expected = ElementFormKey.Rectangle;
            var actual = _modelElement.FormKey();
            Assert.AreEqual(expected, actual, 
                "Метод ChangeForm работает некорректно");
        }

        [Test(Description = "Позитивный тест метода Equal")]
        public void Test_Equal()
        {
            var expected = new ModelElement(_values, ElementFormKey.Rectangle);
            Assert.IsTrue(_modelElement.Equals(expected),
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Эквивалентность разных элементов")]
        public void Test_Equal_NotEqual()
        {
            var expected = new ModelElement(_values, ElementFormKey.Rectangle);
            expected.ChangeForm();
            Assert.IsFalse(_modelElement.Equals(expected), 
                "Метод Equal некорректно сравнивает элементы");
        }

        [Test(Description = "Позитивный тест конструктора ModelElement")]
        public void Test_ModelParameters()
        {
            var expected = new ModelElement(_values, ElementFormKey.Rectangle);
            Assert.IsTrue(_modelElement.Equals(expected),
                "Ошибка присоздании элемента");
        }
    }
}
