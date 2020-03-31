using System.Collections.Generic;
using NUnit.Framework;
using Parameters;

namespace Plugin_KOMPAS_3D.UnitTests
{
    class ModelElementsTests
    {
        [Test(Description = "Позитивный тест метода Element")]
        public void Test_Element()
        {
            var modelElements = new ModelElements();
            var result = true;
            var element = modelElements.Element(ElementName.Rele);
            if (element.Parameter(ParametersName.D).Value != 10
                || element.Parameter(ParametersName.L).Value != 12)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод Element работает некорректно");
        }

        [Test(Description = "Негативный тест метода Element при запросе несуществующего элементе")]
        public void Test_Element_NotElement()
        {
            var modelElements = new ModelElements();
            Assert.Throws<KeyNotFoundException>(() => { modelElements.Element(ElementName.SpeakerCover4); },
                    "Должно возникать исключение если, в словаре нет элемента с запрашиваемым именем");
        }

        [Test(Description = "Позитивный тест метода IsElement для существующего элемента")]
        public void Test_IsElement_IsElement()
        {
            var modelElements = new ModelElements();
            var expected = true;
            var actual = modelElements.IsElement(ElementName.Case);
            Assert.AreEqual(expected, actual, "Метод IsElement работает некорректно");
        }

        [Test(Description = "Позитивный тест метода IsElement для несуществующего элемента")]
        public void Test_IsElement_NotElement()
        {
            var modelElements = new ModelElements();
            var expected = false;
            var actual = modelElements.IsElement(ElementName.SpeakerCover4);
            Assert.AreEqual(expected, actual, "Метод IsElement работает некорректно");
        }

        [Test(Description = "Позитивный тест метода AddElement")]
        public void Test_AddElement()
        {
            var modelElements = new ModelElements();
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };
            modelElements.AddElement(values, ElementName.SpeakerCover4, false);
            var result = true;
            var element = modelElements.Element(ElementName.SpeakerCover4);
            if (element.Parameter(ParametersName.H).Value != 100
                || element.Parameter(ParametersName.L).Value != 150
                || element.Parameter(ParametersName.W).Value != 200)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод AddElement работает некорректно");
        }

        [Test(Description = "Позитивный тест метода AddDinamic для добавления 2 динамика")]
        public void Test_AddDinamic_Dinamic2()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2)
            };
            foreach(var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 65
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
            {
                result = false;
            }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно добавляет 2 динамик");
        }

        [Test(Description = "Позитивный тест метода AddDinamic для добавления 3 динамика")]
        public void Test_AddDinamic_Dinamic3()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 55
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно добавляет 3 динамик");
        }

        [Test(Description = "Позитивный тест метода AddDinamic для добавления 4 динамика")]
        public void Test_AddDinamic_Dinamic4()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3),
                modelElements.Element(ElementName.SpeakerCover4)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 45
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно добавляет 4 динамик");
        }

        [Test(Description = "Позитивный тест метода AddDinamic при существовании 4 динамиков")]
        public void Test_AddDinamic_More4Dinamic()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            var expected = 4;
            var actual = modelElements.NumberDinamics();
            Assert.AreEqual(expected, actual, "Метод AddDinamic работает некорректно при существовании 4 динамиков");
        }

        [Test(Description = "Позитивный тест метода DeleteDinamic для удаления 4 динамика")]
        public void Test_DeleteDinamic_Dinamic4()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.DeleteDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 55
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно удаляет 4 динамик");
        }

        [Test(Description = "Позитивный тест метода DeleteDinamic для удаления 3 динамика")]
        public void Test_DeleteDinamic_Dinamic3()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.DeleteDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 65
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно удаляет 3 динамик");
        }

        [Test(Description = "Позитивный тест метода DeleteDinamic для удаления 2 динамика")]
        public void Test_DeleteDinamic_Dinamic2()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.DeleteDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 75
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно удаляет 2 динамик");
        }

        [Test(Description = "Позитивный тест метода DeleteDinamic при существовании 1 динамика")]
        public void Test_DeleteDinamic_Dinamic1()
        {
            var modelElements = new ModelElements();
            modelElements.DeleteDinamic();
            var expected = 1;
            var actual = modelElements.NumberDinamics();
            Assert.AreEqual(expected, actual, "Метод AddDinamic работает некорректно, при существовании 1 динамика");
        }

        [Test(Description = "Позитивный тест метода DeleteElement")]
        public void Test_DeleteElement()
        {
            var modelElements = new ModelElements();
            modelElements.DeleteElement(ElementName.Case);
            var expected = false;
            var actual = modelElements.IsElement(ElementName.Case);
            Assert.AreEqual(expected, actual, "Метод AddElement не удаляет элемент");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 1 при существовании 1 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic1(ElementName name)
        {
            var modelElements = new ModelElements();
            modelElements.CalculationMaxHDinamic(name);
            var actual = modelElements.Element(name).
                Parameter(ParametersName.H).MaxValue;
            var expected = 75;
            Assert.AreEqual(expected, actual, "Метод CalculationMaxHDinamic не расчитывает максимальное значение высоты");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 1 при существовании 2 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 2 при существовании 2 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic2(ElementName name)
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.CalculationMaxHDinamic(name);
            var actual = modelElements.Element(name).
                Parameter(ParametersName.H).MaxValue;
            var expected = 65;
            Assert.AreEqual(expected, actual, "Метод CalculationMaxHDinamic не расчитывает максимальное значение высоты");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 1 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 2 при существовании 3 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 3 при существовании 3 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic3(ElementName name)
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.CalculationMaxHDinamic(name);
            var actual = modelElements.Element(name).
                Parameter(ParametersName.H).MaxValue;
            var expected = 55;
            Assert.AreEqual(expected, actual, "Метод CalculationMaxHDinamic не расчитывает максимальное значение высоты");
        }

        [TestCase(ElementName.SpeakerCover1,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 1 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover2,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 2 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 3 при существовании 4 динамика")]
        [TestCase(ElementName.SpeakerCover3,
           TestName = "Позитивный тест метода CalculationMaxHDinamic для расчета " +
            "высоты динамика 4 при существовании 4 динамика")]
        public void Test_CalculationMaxHDinamic_Dinamic4(ElementName name)
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.CalculationMaxHDinamic(name);
            var actual = modelElements.Element(name).
                Parameter(ParametersName.H).MaxValue;
            var expected = 45;
            Assert.AreEqual(expected, actual, "Метод CalculationMaxHDinamic не расчитывает максимальное значение высоты");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxHDinamic при существовании 1 динамика")]
        public void Test_CalculationMaxHDinamic_1Dinamic()
        {
            var modelElements = new ModelElements();
            modelElements.CalculationMaxDinamics();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MaxValue != 75)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно расчитывает значение высоты при 1 динамике");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxHDinamic при существовании 2 динамиков")]
        public void Test_CalculationMaxHDinamic_2Dinamic()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.CalculationMaxDinamics();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MaxValue != 65)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно расчитывает значение высоты при 2 динамиках");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxHDinamic при существовании 3 динамиков")]
        public void Test_CalculationMaxHDinamic_3Dinamic()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.CalculationMaxDinamics();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MaxValue != 55)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно расчитывает значение высоты при 3 динамиках");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxHDinamic при существовании 4 динамиков")]
        public void Test_CalculationMaxHDinamic_4Dinamic()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.CalculationMaxDinamics();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3),
                modelElements.Element(ElementName.SpeakerCover4)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.H).MaxValue != 45)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод AddDinamic некорректно расчитывает значение высоты при 4 динамиках");
        }

        [Test(Description = "Позитивный тест метода ChangeForm")]
        public void Test_ChangeForm()
        {
            var modelElements = new ModelElements();
            modelElements.ChangeForm(ElementName.SpeakerCover1);
            var element = modelElements.Element(ElementName.SpeakerCover1);
            var result = true;
            if( element.FormKey() != true)
            {
                result = false;
            }
            Assert.IsTrue(result, "Метод ChangeForm некорректно изменяет параметры элемента");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxWDinamic")]
        public void Test_CalculationMaxWDinamic()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.AddDinamic();
            modelElements.CalculationMaxWDinamic();
            var result = true;
            var elements = new List<ModelElement>
            {
                modelElements.Element(ElementName.SpeakerCover1),
                modelElements.Element(ElementName.SpeakerCover2),
                modelElements.Element(ElementName.SpeakerCover3),
                modelElements.Element(ElementName.SpeakerCover4)
            };
            foreach (var element in elements)
            {
                if (element.Parameter(ParametersName.W).MaxValue != 195)
                {
                    result = false;
                }
            }
            Assert.IsTrue(result, "Метод ChangeForm некорректно расчитывает ширину динамиков");
        }

        [Test(Description = "Позитивный тест метода CalculationMaxDinamics")]
        public void Test_CalculationMaxDinamics()
        {
            var modelElements = new ModelElements();
            var expected = 75;
            var actual = modelElements.CalculationMaxDinamics();
            Assert.AreEqual(expected, actual, "Метод CalculationMaxDinamics некорректно расчитывает сумму высот динамиков");
        }

        [Test(Description = "Позитивный тест метода NumberDinamics")]
        public void Test_NumberDinamics()
        {
            var modelElements = new ModelElements();
            modelElements.AddDinamic();
            var expected = 2;
            var actual = modelElements.NumberDinamics();
            Assert.AreEqual(expected, actual, "Метод NumberDinamics некорректно расчитывает число динамиков");
        }

        [Test(Description = "Позитивный тест конструктора ModelElements")]
        public void Test_ModelElements()
        {
            var modelElements = new ModelElements();
            var result = true;
            var element = modelElements.Element(ElementName.Case);
            if (element.Parameter(ParametersName.H).MinValue != 100
                || element.Parameter(ParametersName.L).MinValue != 150
                || element.Parameter(ParametersName.W).MinValue != 200
                || element.Parameter(ParametersName.H).Value != 100
                || element.Parameter(ParametersName.L).Value != 150
                || element.Parameter(ParametersName.W).Value != 200
                || element.Parameter(ParametersName.H).MaxValue != 500
                || element.Parameter(ParametersName.L).MaxValue != 200
                || element.Parameter(ParametersName.W).MaxValue != 300)
            {
                result = false;
            }

            element = modelElements.Element(ElementName.Rele);
            if (element.Parameter(ParametersName.D).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 12
                || element.Parameter(ParametersName.D).Value != 10
                || element.Parameter(ParametersName.L).Value != 12
                || element.Parameter(ParametersName.D).MaxValue != 20
                || element.Parameter(ParametersName.L).MaxValue != 12)
            {
                result = false;
            }

            element = modelElements.Element(ElementName.SpeakerCover1);
            if (element.Parameter(ParametersName.H).MinValue != 10
                || element.Parameter(ParametersName.L).MinValue != 5
                || element.Parameter(ParametersName.W).MinValue != 150
                || element.Parameter(ParametersName.H).Value != 10
                || element.Parameter(ParametersName.L).Value != 5
                || element.Parameter(ParametersName.W).Value != 150
                || element.Parameter(ParametersName.H).MaxValue != 75
                || element.Parameter(ParametersName.L).MaxValue != 20
                || element.Parameter(ParametersName.W).MaxValue != 195)
            {
                result = false;
            }
            Assert.IsTrue(result, "Конструктор ModelElements некорретно создает объект класса");
        }
    }
}
