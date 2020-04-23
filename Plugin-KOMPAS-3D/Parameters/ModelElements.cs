using System.Collections.Generic;

namespace Parameters
{
    /// <summary>
    /// Класс хранит словарь элементов модели
    /// </summary>
    public class ModelElements
    {
        /// <summary>
        /// Хранит словарь элементов модели
        /// </summary>
        private Dictionary<ElementName, ModelElement> _elements = 
            new Dictionary<ElementName, ModelElement>();

        /// <summary>
        /// Возвращает элемент модели
        /// </summary>
        /// <param name="name">Имя элемента модели</param>
        /// <returns>Элемент модели</returns>
        public ModelElement Element(ElementName name)
        {
            return _elements[name];
        }

        /// <summary>
        /// Метод проверяет существование 
        /// элемента
        /// </summary>
        /// <param name="name">Имя элемента</param>
        /// <returns>
        /// true - элемент существует
        /// false - элемент не существует
        /// </returns>
        public bool IsElement(ElementName name)
        {
            return _elements.ContainsKey(name);
        }

        /// <summary>
        /// Метод добавляет элемент
        /// </summary>
        /// <param name="parameters">Параметры элемента</param>
        /// <param name="name">Имя элемента</param>
        /// <param name="formKey">Ключ формы</param>
        public void AddElement(List<(double min, double max, ParametersName name)> parameters,
            ElementName name, ElementFormKey formKey)
        { 
            var modelElement = new ModelElement(parameters, formKey);
            _elements.Add(name, modelElement);
        }

        /// <summary>
        /// Добавление динамика
        /// </summary>
        public void AddDynamic()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (10, 10, ParametersName.Height),
                (150, 195, ParametersName.Width),
                (5, 20, ParametersName.Length)
            };
            if (!_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                AddElement(values,
                    ElementName.SpeakerCover2, 
                    ElementFormKey.Rectangle);
            }
            else
            {
                if (!_elements.ContainsKey(ElementName.SpeakerCover3))
                {
                    AddElement(values, 
                        ElementName.SpeakerCover3, 
                        ElementFormKey.Rectangle);
                }
                else
                {
                    if (!_elements.ContainsKey(ElementName.SpeakerCover4))
                    {
                        AddElement(values, 
                            ElementName.SpeakerCover4, 
                            ElementFormKey.Rectangle);
                    }
                }
            }
            CalculationMaxHeightDynamics();
            CalculationMaxWidthDynamic();
        }

        /// <summary>
        /// Удаление динамика
        /// </summary>
        public void DeleteDynamic()
        {
            if (_elements.ContainsKey(ElementName.SpeakerCover4))
            {
                DeleteElement(ElementName.SpeakerCover4);
            }
            else
            {
                if (_elements.ContainsKey(ElementName.SpeakerCover3))
                {
                    DeleteElement(ElementName.SpeakerCover3);
                }
                else
                {
                    if (_elements.ContainsKey(ElementName.SpeakerCover2))
                    {
                        DeleteElement(ElementName.SpeakerCover2);
                    }
                }
            }
            CalculationMaxHeightDynamics();
            CalculationMaxWidthDynamic();
        }

        /// <summary>
        /// Метод выполняет удаление 
        /// элемента с указанным названием
        /// </summary>
        /// <param name="name">Название элемента</param>
        public void DeleteElement(ElementName name)
        {
            _elements.Remove(name);
        }

        /// <summary>
        /// Метод расчитывает максимальное 
        /// значение высоты динамика
        /// </summary>
        /// <param name="name">Название элемента</param>
        public void CalculationMaxHeightDynamic(ElementName name)
        {
            var diameterRele = 
                Element(ElementName.Rele).Parameter(ParametersName.Diameter).Value;
            var heightCase = 
                Element(ElementName.Case).Parameter(ParametersName.Height).Value;
            var heightSpeakerCover1 = 0.0;
            if(name != ElementName.SpeakerCover1)
            {
                heightSpeakerCover1 = 
                    Element(ElementName.SpeakerCover1)
                    .Parameter(ParametersName.Height).Value;
            }
            var heightSpeakerCover2 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover2) 
                && name != ElementName.SpeakerCover2)
            {
                heightSpeakerCover2 = 
                    Element(ElementName.SpeakerCover2).Parameter(ParametersName.Height).Value;
            }
            var heightSpeakerCover3 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover3) 
                && name != ElementName.SpeakerCover3)
            {
                heightSpeakerCover3 = 
                    Element(ElementName.SpeakerCover3).Parameter(ParametersName.Height).Value;
            }
            var heightSpeakerCover4 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover4) 
                && name != ElementName.SpeakerCover4)
            {
                heightSpeakerCover4 = 
                    Element(ElementName.SpeakerCover4).Parameter(ParametersName.Height).Value;
            }
            var maxHeight = heightCase - 5 - (diameterRele + 10) 
                - heightSpeakerCover1 - heightSpeakerCover2 
                - heightSpeakerCover3 - heightSpeakerCover4;
            Element(name).Parameter(ParametersName.Height).MaxValue = maxHeight;
            Element(name).CircleParameter();
        }

        /// <summary>
        /// Метод расчитывает и присваивает 
        /// максимальное значение высот динамиков
        /// </summary>
        public void CalculationMaxHeightDynamics()
        {
            CalculationMaxHeightDynamic(ElementName.SpeakerCover1);
            if (_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                CalculationMaxHeightDynamic(ElementName.SpeakerCover2);
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover3))
            {
                CalculationMaxHeightDynamic(ElementName.SpeakerCover3);
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover4))
            {
                CalculationMaxHeightDynamic(ElementName.SpeakerCover4);
            }
        }

        /// <summary>
        /// Метод для изменения формы элемента
        /// </summary>
        /// <param name="name">Имя элемента</param>
        public void ChangeForm(ElementName name)
        {
            var element = Element(name);
            element.ChangeForm();
            CalculationMaxHeightDynamics();
            CalculationMaxWidthDynamic();
        }

        /// <summary>
        /// Метод расчитывает и присваивает
        /// максимальную ширину динамика
        /// </summary>
        public void CalculationMaxWidthDynamic()
        {
            var widthCase = 
                Element(ElementName.Case).Parameter(ParametersName.Width).Value;
            var maxWidthDynamic = widthCase - 5;
            if (_elements.ContainsKey(ElementName.SpeakerCover1))
            {
                Element(ElementName.SpeakerCover1).Parameter(ParametersName.Width).MaxValue = 
                    maxWidthDynamic;
                CalculationMaxHeightDynamic(ElementName.SpeakerCover1);
                Element(ElementName.SpeakerCover1).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                Element(ElementName.SpeakerCover2).Parameter(ParametersName.Width).MaxValue = 
                    maxWidthDynamic;
                CalculationMaxHeightDynamic(ElementName.SpeakerCover2);
                Element(ElementName.SpeakerCover2).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover3))
            {
                Element(ElementName.SpeakerCover3).Parameter(ParametersName.Width).MaxValue = 
                    maxWidthDynamic;
                CalculationMaxHeightDynamic(ElementName.SpeakerCover3);
                Element(ElementName.SpeakerCover3).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover4))
            {
                Element(ElementName.SpeakerCover4).Parameter(ParametersName.Width).MaxValue = 
                    maxWidthDynamic;
                CalculationMaxHeightDynamic(ElementName.SpeakerCover4);
                Element(ElementName.SpeakerCover4).CircleParameter();
            }
        }

        /// <summary>
        /// Метод возвращает максимальную
        /// сумму высот динамиков
        /// </summary>
        /// <returns>Максимальна высота динамиков</returns>
        public double CalculationMaxDynamics()
        {
            var diameterRele = 
                Element(ElementName.Rele).Parameter(ParametersName.Diameter).Value;
            var heightCase = 
                Element(ElementName.Case).Parameter(ParametersName.Height).Value;
            var maxHeightDinamics = heightCase - 5 - (diameterRele + 10);
            return maxHeightDinamics;
        }

        /// <summary>
        /// Метод возвращает число динамиков 
        /// модели
        /// </summary>
        /// <returns>Число динамиков</returns>
        public int NumberDynamics()
        {
            return _elements.Count - 2;
        }

        /// <summary>
        /// Конструктор класса ModelElements
        /// Создаютя элементы:
        /// 1.Корпус
        /// 2.Реле регулировки
        /// 3.Кнопка включения
        /// 4.Динамик №1
        /// </summary>
        public ModelElements()
        {
            // Добавление параметров
            // корпуса колонки
            AddElement(
                new List<(double min, double max, ParametersName name)>
                {
                    (100, 500, ParametersName.Height),
                    (200, 300, ParametersName.Width),
                    (150, 200, ParametersName.Length)
                },
                ElementName.Case, 
                ElementFormKey.Rectangle);

            // Добавление параметров 
            // реле регулировки
            AddElement(
                new List<(double min, double max, ParametersName name)>
                {
                    (10, 20, ParametersName.Diameter),
                    (12, 12, ParametersName.Length)
                }, 
                ElementName.Rele, 
                ElementFormKey.Circle);

            // Добавление параметров
            // первого динамик
            AddElement(
                new List<(double min, double max, ParametersName name)>
                {
                    (10, 75, ParametersName.Height),
                    (150, 195, ParametersName.Width),
                    (5, 20, ParametersName.Length)
                }, 
                ElementName.SpeakerCover1, 
                ElementFormKey.Rectangle);
        }
    }
}
