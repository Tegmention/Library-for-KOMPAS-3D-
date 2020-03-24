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
        //Поле хранит экземпляр построителя 3D модели
        private BuilderModel _builderModel;

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
        
        //1 тест
        /// <summary>
        /// Конструктор класса Manager
        /// Вызывает метод для инициализации
        /// экземпляра построителя 3D модели 
        /// </summary>
        /// <param name="parameters"></param>
        public Manager(ModelElements elements)
        {
            InirializeModel(elements);
        }

        //1 тест
        /// <summary>
        /// Метод создает экземпляр 
        /// класса построителья модели
        /// </summary>
        /// <param name="parameters"></param>
        private void InirializeModel(ModelElements elements)
        {
            _builderModel = new BuilderModel(elements,OpenKompas3D());
        }
    }
}
