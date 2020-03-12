using System;
using Kompas6API5;
using Parameters;
using System.Runtime.InteropServices;

namespace Builder
{
    // 9 тестов
    /// <summary>
    /// Класс используется для подключения 
    /// к САПР Компас 3Д и инициализации 
    /// экземпляра построителя модели
    /// </summary>
    public class Manager
    {
        //1 тест
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
            //1 тест
            try
            {
                //1 тест
                kompas = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                kompas.Visible = true;
            }
            //Создание нового экзмепляра
            //1 тест
            catch
            {
                //1 Тест
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                //1 Tест
                kompas = (KompasObject)Activator.CreateInstance(t);
                kompas.Visible = true;
            }
            //Выполняется после заверешения try и catch
            finally
            {
                //1 Тест
                kompas.ActivateControllerAPI();
            }
            return kompas;
        }
        
        //1 тест
        /// <summary>
        /// Конструктор класса Manager
        /// Инициализирует класс построитель модели 
        /// </summary>
        /// <param name="parameters"></param>
        public Manager(ModelParameters parameters)
        {
            InirializeModel(parameters);
        }

        //1 тест
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
