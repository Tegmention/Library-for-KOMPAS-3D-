using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;
using Parameters;
using System.Runtime.InteropServices;

namespace Builder
{
    /// <summary>
    /// Класс используется для подключения 
    /// к САПР Компас 3Д и инициализации 
    /// экземпляра построителя модели
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Подключение к экземпляру компас 3Д
        /// Если экземпляр есть создан,
        /// то подключиться к существующему
        /// Если экземпляр не создан,
        /// то создать и подключиться к новому
        /// </summary>
        /// <returns></returns>
        private KompasObject OpenKompas3D()
        {
            //Начальное присвоение 
            KompasObject kompas = null;
            //Экзмпляр уже существует
            //Отображение необходимо в каждом случае
            //так как возможна ошибка при подключении 
            //к уже закрытому экземпляру
            try
            {
                kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                kompas.Visible = true;
            }
            //Создание нового экзмепляра
            catch
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompas = (KompasObject)Activator.CreateInstance(t);
                kompas.Visible = true;
            }
            //Выполняется после заверешения try и catch
            finally
            {
                kompas.ActivateControllerAPI();
            }
            return kompas;
        }
        
        /// <summary>
        /// Конструктор класса Manager
        /// Инициализирует класс построитель модели 
        /// </summary>
        /// <param name="parameters"></param>
        public Manager(ModelParameters parameters)
        {
            InirializeModel(parameters);
        }

        /// <summary>
        /// Метод создает экземпляр 
        /// класса построителья модели
        /// </summary>
        /// <param name="parameters"></param>
        private void InirializeModel(ModelParameters parameters)
        {
            var BuilderModel = new BuilderModel(parameters,OpenKompas3D());
        }
    }
}
