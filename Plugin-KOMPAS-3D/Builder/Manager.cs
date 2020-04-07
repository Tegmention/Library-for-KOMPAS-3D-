using System;
using Kompas6API5;
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
        /// Поле хранит экземпляр построителя 3D модели
        /// </summary>
        private BuilderModel _builderModel;

        /// <summary>
        /// Подключение к экземпляру компас 3Д
        /// Если экземпляр есть создан,
        /// то подключиться к существующему
        /// Если экземпляр не создан,
        /// то создать и подключиться к новому
        /// </summary>
        /// <returns>Экземпляр Kompas3D</returns>
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
                kompas = 
                    (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                kompas.Visible = true;
            }
            //Создание нового экзмепляра
            catch
            {
                Type type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompas = (KompasObject)Activator.CreateInstance(type);
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
        /// Вызывает метод для инициализации
        /// экземпляра построителя 3D модели 
        /// </summary>
        /// <param name="elements">Элементы модели</param>
        public Manager(ModelElements elements)
        {
            InirializeModel(elements);
        }

        /// <summary>
        /// Метод создает экземпляр 
        /// класса построителя модели
        /// </summary>
        /// <param name="elements">Элементы модели</param>
        private void InirializeModel(ModelElements elements)
        {
            _builderModel = new BuilderModel(elements,OpenKompas3D());
        }
    }
}
