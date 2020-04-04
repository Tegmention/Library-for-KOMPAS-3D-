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
        private Dictionary<ElementName, ModelElement> _elements = new Dictionary<ElementName, ModelElement>();

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
            //TODO: Можно просто вернуть ContainsKey
            var result = false;
            if(_elements.ContainsKey(name))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Метод добавляет элемент
        /// </summary>
        /// <param name="parameters">Параметры элемента</param>
        /// <param name="name">Имя элемента</param>
        /// <param name="formKey">Ключ формы</param>
        public void AddElement(List<(double min, double max, ParametersName name)> parameters,
            ElementName name, bool formKey)
        { 
            var modelElement = new ModelElement(parameters, formKey);
            _elements.Add(name, modelElement);
        }

        /// <summary>
        /// Добавление динамика
        /// </summary>
        /// //TODO: dYnamic - везде переписать с Y
        public void AddDinamic()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (10, 10, ParametersName.H),
                (150, 195, ParametersName.W),
                (5, 20, ParametersName.L)
            };
            if (!_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                AddElement(values, ElementName.SpeakerCover2, false);
            }
            else
            {
                if (!_elements.ContainsKey(ElementName.SpeakerCover3))
                {
                    AddElement(values, ElementName.SpeakerCover3, false);
                }
                else
                {
                    if (!_elements.ContainsKey(ElementName.SpeakerCover4))
                    {
                        AddElement(values, ElementName.SpeakerCover4, false);
                    }
                }
            }
            CalculationMaxHDinamics();
            CalculationMaxWDinamic();
        }

        /// <summary>
        /// Удалаение динамика
        /// </summary>
        public void DeleteDinamic()
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
            CalculationMaxHDinamics();
            CalculationMaxWDinamic();
        }

        //TODO: XML
        //Метод для удаления элемента
        public void DeleteElement(ElementName name)
        {
            _elements.Remove(name);
        }

        /// <summary>
        /// Метод расчитывает максимальное значение высоты динамика
        /// </summary>
        public void CalculationMaxHDinamic(ElementName name)
        {
            //TODO: RSDN - именование
            var D = Element(ElementName.Rele).Parameter(ParametersName.D).Value;
            var H = Element(ElementName.Case).Parameter(ParametersName.H).Value;
            var H1 = 0.0;
            if(name != ElementName.SpeakerCover1)
            {
                H1 = Element(ElementName.SpeakerCover1).Parameter(ParametersName.H).Value;
            }
            var H2 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover2) && name != ElementName.SpeakerCover2)
            {
                H2 = Element(ElementName.SpeakerCover2).Parameter(ParametersName.H).Value;
            }
            var H3 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover3) && name != ElementName.SpeakerCover3)
            {
                H3 = Element(ElementName.SpeakerCover3).Parameter(ParametersName.H).Value;
            }
            var H4 = 0.0;
            if (_elements.ContainsKey(ElementName.SpeakerCover4) && name != ElementName.SpeakerCover4)
            {
                H4 = Element(ElementName.SpeakerCover4).Parameter(ParametersName.H).Value;
            }
            var maxH = H - 5 - (D + 10) - H1 - H2 - H3 - H4;
            Element(name).Parameter(ParametersName.H).MaxValue = maxH;
            Element(name).CircleParameter();
        }

        /// <summary>
        /// Метод расчитывает и присваивает 
        /// максимальное значение высот динамиков
        /// </summary>
        public void CalculationMaxHDinamics()
        {
            CalculationMaxHDinamic(ElementName.SpeakerCover1);
            if (_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                CalculationMaxHDinamic(ElementName.SpeakerCover2);
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover3))
            {
                CalculationMaxHDinamic(ElementName.SpeakerCover3);
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover4))
            {
                CalculationMaxHDinamic(ElementName.SpeakerCover4);
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
            CalculationMaxHDinamics();
            CalculationMaxWDinamic();
        }

        /// <summary>
        /// Метод расчитывает и присваивает
        /// максимальную ширину динамика
        /// </summary>
        public void CalculationMaxWDinamic()
        {
            //TODO: RSDN - именование
            var W = Element(ElementName.Case).Parameter(ParametersName.W).Value;
            var maxW = W - 5;
            if (_elements.ContainsKey(ElementName.SpeakerCover1))
            {
                Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).MaxValue = maxW;
                CalculationMaxHDinamic(ElementName.SpeakerCover1);
                Element(ElementName.SpeakerCover1).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover2))
            {
                Element(ElementName.SpeakerCover2).Parameter(ParametersName.W).MaxValue = maxW;
                CalculationMaxHDinamic(ElementName.SpeakerCover2);
                Element(ElementName.SpeakerCover2).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover3))
            {
                Element(ElementName.SpeakerCover3).Parameter(ParametersName.W).MaxValue = maxW;
                CalculationMaxHDinamic(ElementName.SpeakerCover3);
                Element(ElementName.SpeakerCover3).CircleParameter();
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover4))
            {
                Element(ElementName.SpeakerCover4).Parameter(ParametersName.W).MaxValue = maxW;
                CalculationMaxHDinamic(ElementName.SpeakerCover4);
                Element(ElementName.SpeakerCover4).CircleParameter();
            }
        }

        /// <summary>
        /// Метод возвращает максимальную
        /// сумму высот динамиков
        /// </summary>
        /// <returns>Максимальна высота динамиков</returns>
        public double CalculationMaxDinamics()
        {
            //TODO: RSDN - именование
            var D = Element(ElementName.Rele).Parameter(ParametersName.D).Value;
            var H = Element(ElementName.Case).Parameter(ParametersName.H).Value;
            var maxH = H - 5 - (D + 10);
            return maxH;
        }

        /// <summary>
        /// Метод возвращает число динамиков 
        /// модели
        /// </summary>
        /// <returns>Число динамиков</returns>
        public int NumberDinamics()
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
        /// //TODO: XML - параметры
        /// <param name="name"></param>
        public ModelElements()
        {
            //TODO: Не переиспользуйте одну и туже переменную - это плохая практика,
            //TODO которая может привести к ошибкам. Лучше уж напрямую создавать эти
            //TODO: списки в передаваемых аргументах
            //Параметры корпуса
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };
            AddElement(values, ElementName.Case, false);

            //Параметры реле регулировки
            values = new List<(double min, double max, ParametersName name)>
            {
                (10, 20, ParametersName.D),
                (12, 12, ParametersName.L)
            };
            AddElement(values, ElementName.Rele, true);

            //Добавить 1 динамик
            values = new List<(double min, double max, ParametersName name)>
            {
                (10, 75, ParametersName.H),
                (150, 195, ParametersName.W),
                (5, 20, ParametersName.L)
            };
            AddElement(values, ElementName.SpeakerCover1, false);
        }
    }
}
