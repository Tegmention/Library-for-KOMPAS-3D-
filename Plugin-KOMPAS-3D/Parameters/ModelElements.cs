using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ModelElement Element(ElementName name)
        {
            return _elements[name];
        }

        public bool IsElement(ElementName name)
        {
            var result = false;
            if(_elements.ContainsKey(name))
            {
                result = true;
            }
            return result;
        }

        //Метод для добавления элемента 
        public void AddElement(List<(double min, double max, ParametersName name)> parameters,
            ElementName name, bool formKey)
        { 
            var modelElement = new ModelElement(parameters, formKey);
            _elements.Add(name, modelElement);
        }

        public void AddDinamic()
        {
            var values = new List<(double min, double max, ParametersName name)>
            {
                (10, 75, ParametersName.H),
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
        }

        /// <summary>
        /// Метод расчитывает и присваивает 
        /// максимальное значение высоты динамиков
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
        /// Метод расчитывает и присваивает
        /// максимальную ширину динамика
        /// если он имеет прямоугольную форму
        /// </summary>
        public void CalculationMaxWDinamic()
        {
            var W = Element(ElementName.Case).Parameter(ParametersName.W).Value;
            var maxW = W - 5;
            if (_elements.ContainsKey(ElementName.SpeakerCover1) 
                && Element(ElementName.SpeakerCover1).FormKey() == false)
            {
                Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).MaxValue = maxW;
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover2)
                && Element(ElementName.SpeakerCover2).FormKey() == false)
            {
                Element(ElementName.SpeakerCover2).Parameter(ParametersName.W).MaxValue = maxW;
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover3)
               && Element(ElementName.SpeakerCover3).FormKey() == false)
            {
                Element(ElementName.SpeakerCover3).Parameter(ParametersName.W).MaxValue = maxW;
            }
            if (_elements.ContainsKey(ElementName.SpeakerCover4)
               && Element(ElementName.SpeakerCover4).FormKey() == false)
            {
                Element(ElementName.SpeakerCover4).Parameter(ParametersName.W).MaxValue = maxW;
            }
        }


        /// <summary>
        /// Конструктор класса ModelElements
        /// Создаютя элементы:
        /// 1.Корпус
        /// 2.Реле регулировки
        /// 3.Кнопка включения
        /// 4.Динамик №1
        /// </summary>
        /// <param name="name"></param>
        public ModelElements()
        {
            //Параметры корпуса
            var values = new List<(double min, double max, ParametersName name)>
            {
                (100, 500, ParametersName.H),
                (200, 300, ParametersName.W),
                (150, 200, ParametersName.L)
            };
            AddElement(values, ElementName.Case, false);

            //Параметры реле регулировки
            //Для круга H = W
            values = new List<(double min, double max, ParametersName name)>
            {
                (10, 20, ParametersName.D),
                (12, 12, ParametersName.L)
            };
            AddElement(values, ElementName.Rele, true);

            //Параметры кнопки включения
            values = new List<(double min, double max, ParametersName name)>
            {
                (10, 10, ParametersName.D),
                (12, 12, ParametersName.L)
            };
            AddElement(values, ElementName.PowerButton, true);

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
