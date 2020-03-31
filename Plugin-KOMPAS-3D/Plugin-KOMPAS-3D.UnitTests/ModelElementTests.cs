using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Parameters;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelElementTests
    {
        [Test(Description = "Позитивный тест метода Parameter")]
        public void Test_Parameter()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H)
            };
            var modelElement = new ModelElement(values, false);
            var result = true;
            if (modelElement.Parameter(ParametersName.H).MinValue != 100
                || modelElement.Parameter(ParametersName.H).MaxValue != 500
                || modelElement.Parameter(ParametersName.H).Value != 100)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод Parameter работает некорректно");
        }

        [Test(Description = "Запрос параметра отстутствующего в словаре")]
        public void TestParameter_KeyNotFoundException()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H)
            };
            var modelElement = new ModelElement(values, false);
                Assert.Throws<KeyNotFoundException>(() => { modelElement.Parameter(ParametersName.W); },
                    "Должно возникать исключение если, в словаре нет параметра с запрашиваемым именем");
        }

        [Test(Description = "Позитивный тест метода FormKey")]
        public void Test_FormKey()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H)
            };
            var modelElement = new ModelElement(values, false);
            var expected = false;
            var actual = modelElement.FormKey();
            Assert.AreEqual(expected, actual, "Метод FormKey работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при прямоугольной форме")]
        public void Test_CircleParameterRectangleForm()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (100, 600, ParametersName.W)
            };
            var modelElement = new ModelElement(values, false);
            modelElement.CircleParameter();
            var expected = 600;
            var actual = modelElement.Parameter(ParametersName.W).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода CircleParameter при круглой форме")]
        public void Test_CircleParameterCircleForm()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (100, 600, ParametersName.W)
            };
            var modelElement = new ModelElement(values, true);
            modelElement.CircleParameter();
            var expected = 500;
            var actual = modelElement.Parameter(ParametersName.W).MaxValue;
            Assert.AreEqual(expected, actual, "Метод CircleParameter работает некорректно");
        }

        [Test(Description = "Позитивный тест метода ChangeForm при прямоугольной форме")]
        public void Test_ChangeFormRectangleForm()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (100, 600, ParametersName.W)
            };
            var modelElement = new ModelElement(values, false);
            modelElement.ChangeForm();
            var result = true;
            if (modelElement.FormKey() != true 
                && modelElement.Parameter(ParametersName.W).MaxValue != 500)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод ChangeForm работает некорректно");
        }

        [Test(Description = "Позитивный тест метода ChangeForm при круглой форме")]
        public void Test_ChangeFormCirckleForm()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (100, 600, ParametersName.W)
            };
            var modelElement = new ModelElement(values, true);
            modelElement.ChangeForm();
            var expected = false;
            var actual = modelElement.FormKey();
            Assert.AreEqual(expected, actual, "Метод ChangeForm работает некорректно");
        }

        [Test(Description = "Позитивный тест конструктора ModelElement")]
        public void Test_ModelParameters()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };
            var modelElement = new ModelElement(values, false);
            var result = true;
            var valuesOne = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };

            foreach (var value in valuesOne)
            {
                if (modelElement.Parameter(value.name).MaxValue != value.max ||
                    modelElement.Parameter(value.name).MinValue != value.min ||
                    modelElement.Parameter(value.name).Value != value.min ||
                    modelElement.FormKey() != false)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Конструктор ModelParameters не создает корректный экземпляр класса");
        }
    }
}
