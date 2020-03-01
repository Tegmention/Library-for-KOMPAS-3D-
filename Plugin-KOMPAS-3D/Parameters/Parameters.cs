using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parameters
{
    class Parameters
    {
        /// <summary>
        /// Хранит словарь параметров модели
        /// </summary>
        private Dictionary<string, Parameter<double>> _modelParameters;

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Parameter<double>> ModelParameters { get; set; }

        /// <summary>
        /// Метод высчитывает максимальное значение параметра
        /// высоты динамика(HS)
        /// исходя из значений высоты корпуса(H)
        /// и диаметра реле регулировки(D)
        /// по формуле HS=H-5-(D+10)
        /// </summary>
        public void CalculateMaxHeightDinamic()
        {
            _modelParameters["HS"].MaxValue = _modelParameters["H"].Value - 5
                - (_modelParameters["D"].Value + 10);
        }

        /// <summary>
        /// Метод высчитывает максимальное значение параметра 
        /// длинна динамика(LS)
        /// исходя из значения длинны колонки(L)
        /// по формуле LS=L-5
        /// </summary>
        public void CalculateMaxLenghtDinamic()
        {
            _modelParameters["LS"].MaxValue = _modelParameters["L"].Value - 5;
        }

        /// <summary>
        /// Конструктор класса Parameters
        /// </summary>
        public Parameters()
        {
            double[] massMin = {100,200,150,60,10,5,150};
            double[] massMax = {500,300,200,500,30,20,195};
            double[] massValue ={100,200,150,75,10,5,150};
            string[] massName = {"H","L","W","HS","D","WS","LS"};
            foreach (int i in massValue)
            {
                _modelParameters[massName[i]] = new Parameter<double>(massMin[i],
                    massMax[i], massValue[i], massName[i]);
            }
        }   
    }
}
