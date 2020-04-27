using Kompas6API5;
using Parameters;
using Kompas6Constants3D;
using System;

namespace Builder
{
    /// <summary>
    /// Класс для построения 3Д модели 
    /// в САПР Компас 3Д
    /// </summary>
    public class BuilderModel
    {
        /// <summary>
        /// Хранит ссылку на экземпляр Компас 3Д
        /// </summary>
        private KompasObject _kompasObject;

        /// <summary>
        /// Хранит элементы модели колонки
        /// </summary>
        private ModelElements _modelelElements;

        /// <summary>
        /// Конструктор класса BuilderModel
        /// </summary>
        /// <param name="parameters">Параметры модели</param>
        /// <param name="kompas">Экзепляр Компас 3Д</param>
        public BuilderModel(ModelElements elements, KompasObject kompas)
        {
            _modelelElements = elements;
            _kompasObject = kompas;
            CreateModel();
        }

        /// <summary>
        /// Построения 3Д модели
        /// </summary>
        private void CreateModel()
        {
            ksDocument3D iDocument3D = (ksDocument3D)_kompasObject.Document3D();
            iDocument3D.Create(false, true);
            // Получение интерфейса детали
            ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);
            CreateCase(iPart);
            CreateSpeakers(iDocument3D, iPart);
            CreateRele(iPart);
        }

        /// <summary>
        /// Создание корпуса 
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateCase(ksPart iPart)
        {
            // Получаем интерфейс базовой плоскости ХОZ
            ksEntity planeXOZ =
                iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            // Создаем новый эскиз
            ksEntity iSketch = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            // Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = iSketch.GetDefinition();
            // Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeXOZ);
            // Создание эскиза
            iSketch.Create();
            // Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            var width = 
                _modelelElements.Element(ElementName.Case).Parameter(ParametersName.Width).Value;
            var height = 
                _modelelElements.Element(ElementName.Case).Parameter(ParametersName.Height).Value;
            var length = 
                _modelelElements.Element(ElementName.Case).Parameter(ParametersName.Length).Value;
            CreateSketchRectangle(iDocument2D, height, 0, width, 0);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPart, iSketch, length, true);
            Fillet(iPart, 20, 20 , 0);
        }

        /// <summary>
        /// Создание реле регулировки
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateRele(ksPart iPart)
        {
            // Получаем интерфейс базовой плоскости ХОZ
            ksEntity planeXOZ =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            // Создаем новый эскиз
            ksEntity iSketch =
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            // Получаем интерфейс свойств эскиза
                ksSketchDefinition iDefinitionSketch = 
                (ksSketchDefinition)iSketch.GetDefinition();
            // Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeXOZ);
            // Создание эскиза
            iSketch.Create();
            // Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            var radius = 
                _modelelElements.Element(ElementName.Rele).
                Parameter(ParametersName.Diameter).Value / 2;
            var partLength = 
                _modelelElements.Element(ElementName.Case).
                Parameter(ParametersName.Width).Value / 5;
            var indent = 2.5;
            var height = indent + radius;

            iDocument2D.ksCircle(partLength * 4, height, radius, 1);
            iDocument2D.ksCircle(partLength * 3, height, radius, 1);
            iDocument2D.ksCircle(partLength * 2, height, radius, 1);
            iDocument2D.ksCircle(partLength, height, 4, 1);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPart, iSketch, 12, false);
        }

        /// <summary>
        /// Создание прямоугольного 
        /// динамика
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интрфейс детали</param>
        /// <param name="maxHeight">Максимальная высота</param>
        /// <param name="minHeight">Минимальная высота</param>
        /// <param name="maxWidth">Максимальная ширина</param>
        /// <param name="minWidth">Минимальная ширина</param>
        /// <param name="element">Название элемента</param>
        private void CreateRectanglSpeaker(ksDocument3D iDocument3D, 
            ksPart iPart, double maxHeight, double minHeight,
            double maxWidth, double minWidth, ModelElement element)
        {
            // Получаем интерфейс базовой плоскости ХОZ
            ksEntity planeXOZ =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            // Создаем новый эскиз
            ksEntity iSketch =
            (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            // Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = 
                (ksSketchDefinition)iSketch.GetDefinition();
            // Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeXOZ);
            // Создание эскиза
            iSketch.Create();
            // Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();           
            CreateSketchRectangle(iDocument2D, maxHeight, minHeight, maxWidth, minWidth);
            iDefinitionSketch.EndEdit();
            // Операция выдавливания
            ExctrusionSketch(iPart, iSketch, element.Parameter(ParametersName.Length).Value, false);
        }

        /// <summary>
        /// Построение круглого динамика
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="X">Координаты X</param>
        /// <param name="Y">Координаты Y</param>
        /// <param name="radius">Радиус круга</param>
        /// <param name="element">Элемент модели</param>
        private void CreateCirсleSpeaker(ksPart iPart, double X, double Y, 
            double radius,  ModelElement element)
        {
            // Получаем интерфейс базовой плоскости XOZ
            ksEntity planeXOZ =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            // Создаем новый эскиз
            ksEntity iSketch =
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            // Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = 
                (ksSketchDefinition)iSketch.GetDefinition();
            // Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeXOZ);
            // Создание эскиза
            iSketch.Create();
            // Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();           
            iDocument2D.ksCircle( X, Y, radius, 1);
            iDefinitionSketch.EndEdit();
            // Операция выдавливания
            ExctrusionSketch(iPart, iSketch, 
                element.Parameter(ParametersName.Length).Value, false);
            // Операция скругления
            Fillet(iPart, X, Y, element.Parameter(ParametersName.Length).Value);
            CreateRimCirсleSpeaker(iPart, X, Y, radius, element);
        }

        /// <summary>
        /// Построение обода 
        /// круглого динамика
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="X">Координаты X</param>
        /// <param name="Y">Координаты Y</param>
        /// <param name="radius">Радиус круга</param>
        /// <param name="element">Элемент модели</param>
        private void CreateRimCirсleSpeaker(ksPart iPart, double X, double Y,
            double radius, ModelElement element)
        {
            // Получаем интерфейс базовой плоскости XOZ
            ksEntity planeXOZ =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            // Создаем новый эскиз
            ksEntity iSketch =
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            // Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch =
                (ksSketchDefinition)iSketch.GetDefinition();
            iDefinitionSketch.SetPlane(planeXOZ);
            // Создание эскиза
            iSketch.Create();

            // Создание 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            iDocument2D.ksCircle(X, Y, radius * 0.9, 1);
            iDocument2D.ksCircle(X, Y, radius * 0.8, 1);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPart, iSketch, 
                element.Parameter(ParametersName.Length).Value + 4, false);
            Fillet(iPart, X + (radius * 0.9), Y, 
                element.Parameter(ParametersName.Length).Value + 4);
        }

        /// <summary>
        /// Создание 1 динамика
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateSpeakers1Dynamic(ksDocument3D iDocument3D, ksPart iPart)
        {
            var maxDinamic = _modelelElements.CalculationMaxDynamics();
            var result = 10 + 
                _modelelElements.Element(ElementName.Rele).
                Parameter(ParametersName.Diameter).Value;
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover1, (maxDinamic / 2) + result);
        }

        /// <summary>
        /// Создание 2 динамиков
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateSpeakers2Dynamic(ksDocument3D iDocument3D, ksPart iPart)
        {
            var maxDynamic = _modelelElements.CalculationMaxDynamics();
            var result = 10 + _modelelElements.Element(ElementName.Rele).
               Parameter(ParametersName.Diameter).Value;
            var heightDinamic1 = _modelelElements.Element(ElementName.SpeakerCover1).
                Parameter(ParametersName.Height).Value;
            var heightDinamic2 = _modelelElements.Element(ElementName.SpeakerCover2).
                Parameter(ParametersName.Height).Value;
            var indent = ((maxDynamic - heightDinamic1 - heightDinamic2) / 3);
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover1, 
                 result + (2*indent) + heightDinamic2 + (heightDinamic1 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover2, result + indent + (heightDinamic2 / 2));
        }

        /// <summary>
        /// Создание 3 динамиков
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateSpeakers3Dynamic(ksDocument3D iDocument3D, ksPart iPart)
        {
            var maxDynamic = _modelelElements.CalculationMaxDynamics();
            var result = 10 + _modelelElements.Element(ElementName.Rele).
               Parameter(ParametersName.Diameter).Value;
            var heightDinamic1 = _modelelElements.Element(ElementName.SpeakerCover1).
                Parameter(ParametersName.Height).Value;
            var heightDinamic2 = _modelelElements.Element(ElementName.SpeakerCover2).
                Parameter(ParametersName.Height).Value;
            var heightDinamic3 = _modelelElements.Element(ElementName.SpeakerCover3).
                Parameter(ParametersName.Height).Value;
            var indent = ((maxDynamic - heightDinamic1 - heightDinamic2
                - heightDinamic3) / 4);
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover1,
                 result + (3*indent) + heightDinamic3 
                 + heightDinamic2 + (heightDinamic1 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover2, result + (2*indent) 
                + heightDinamic3 + (heightDinamic2 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover3, result + indent
                + (heightDinamic3 / 2));
        }

        /// <summary>
        /// Создание 4 динамиков
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateSpeakers4Dynamic(ksDocument3D iDocument3D, ksPart iPart)
        {
            var maxDynamic = _modelelElements.CalculationMaxDynamics();
            var result = 10 + _modelelElements.Element(ElementName.Rele).
               Parameter(ParametersName.Diameter).Value;
            var heightDinamic1 = _modelelElements.Element(ElementName.SpeakerCover1).
                Parameter(ParametersName.Height).Value;
            var heightDinamic2 = _modelelElements.Element(ElementName.SpeakerCover2).
                Parameter(ParametersName.Height).Value;
            var heightDinamic3 = _modelelElements.Element(ElementName.SpeakerCover3).
                Parameter(ParametersName.Height).Value;
            var heightDinamic4 = _modelelElements.Element(ElementName.SpeakerCover4).
                Parameter(ParametersName.Height).Value;
            var indent = ((maxDynamic - heightDinamic1 - heightDinamic2
                - heightDinamic3 - heightDinamic4) / 5);
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover1,
                 result + (4*indent) + heightDinamic4 + heightDinamic3
                 + heightDinamic2 + (heightDinamic1 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover2, result + (3*indent) + heightDinamic4
                + heightDinamic3 + (heightDinamic2 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover3, result + (2*indent) + heightDinamic4
                + (heightDinamic3 / 2));
            CreateDynamic(iPart, iDocument3D,
                ElementName.SpeakerCover4, result + indent + (heightDinamic4 / 2));
        }

        /// <summary>
        /// Создание динамика
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="name">Название параметра</param>
        /// <param name="Y">Координаты Y</param>
        private void CreateDynamic(ksPart iPart, ksDocument3D iDocument3D, ElementName name, double Y)
        {
            var maxWidthCase = 
                _modelelElements.Element(ElementName.Case).Parameter(ParametersName.Width).Value;
            var width = 
                _modelelElements.Element(name).Parameter(ParametersName.Width).Value;
            var height = 
                _modelelElements.Element(name).Parameter(ParametersName.Height).Value;
            if (_modelelElements.Element(name).FormKey() == ElementFormKey.Rectangle)
            {
                var pointMaxHeightDinamic = Y + (height / 2);
                var pointMinHeightDinamic = Y - (height / 2);
                var pointMaxWidthDinamic = (maxWidthCase / 2) + (width / 2);
                var pointMinWidthDinamic = (maxWidthCase / 2) - (width / 2);
                CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHeightDinamic, 
                    pointMinHeightDinamic, pointMaxWidthDinamic, pointMinWidthDinamic,
                    _modelelElements.Element(name));
                var L3 = _modelelElements.Element(name).Parameter(ParametersName.Length).Value;
                Fillet(iPart, maxWidthCase / 2, Y, L3);
            }
            else
            {
                var X = (maxWidthCase / 2);
                var radius = height / 2;
                CreateCirсleSpeaker(iPart, X, Y, radius,
                    _modelelElements.Element(name));
            }
        }

        /// <summary>
        /// Cоздание динамиков модели
        /// исходя из их количества
        /// </summary>
        /// <param name="iDocument3D">Интерфейс 3D документа</param>
        /// <param name="iPart">Интерфейс детали</param>
        private void CreateSpeakers(ksDocument3D iDocument3D, ksPart iPart)
        {
            //Число динамиков
            var numberDynamic = _modelelElements.NumberDynamics();

            //Если существует 1 динамик
            if ( numberDynamic  == 1)
            {
                CreateSpeakers1Dynamic(iDocument3D, iPart);
            }

            //Если существует 2 динамика
            if (numberDynamic == 2)
            {
                CreateSpeakers2Dynamic(iDocument3D, iPart);
            }

            //Если существует 3 динамика
            if (numberDynamic == 3)
            {
                CreateSpeakers3Dynamic(iDocument3D, iPart);
            }

            //Если существует 4 динамика
            if (numberDynamic == 4)
            {
                CreateSpeakers4Dynamic(iDocument3D, iPart);
            }
        }

        /// <summary>
        /// Создание чертежа прямоугольника по
        /// заданным координатам
        /// </summary>
        /// <param name="iDocument2D">Интерфейс эскиза</param>
        /// <param name="maxHeight">Максимальная высота</param>
        /// <param name="minHeight">Минимальная высота</param>
        /// <param name="maxWidth">Максимальная ширина</param>
        /// <param name="minWidth">Минимальная ширина</param>
        private void CreateSketchRectangle(ksDocument2D iDocument2D, 
            double maxHeight, double minHeight,
            double maxWidth, double minWidth)
        {
            iDocument2D.ksLineSeg( minWidth, minHeight, maxWidth, minHeight, 1);
            iDocument2D.ksLineSeg( minWidth, minHeight, minWidth, maxHeight, 1);
            iDocument2D.ksLineSeg( minWidth, maxHeight, maxWidth, maxHeight, 1);
            iDocument2D.ksLineSeg( maxWidth, minHeight, maxWidth, maxHeight, 1);
        }

        /// <summary>
        /// Выдавливание по эскизу
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="iSketch">Эскиз</param>
        /// <param name="depth">Глубина выдавливания</param>
        /// <param name="type">Тип выдавливания</param>
        private void ExctrusionSketch (ksPart iPart, ksEntity iSketch, double depth, bool type)
        {
            // Операция выдавливание
            ksEntity entityExtr = 
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            // Интерфейс операции выдавливания
            ksBossExtrusionDefinition extrusionDef =
                (ksBossExtrusionDefinition)entityExtr.GetDefinition();                
            // Интерфейс структуры параметров выдавливания
            ksExtrusionParam extrProp =
                (ksExtrusionParam)extrusionDef.ExtrusionParam();    
            // Эскиз операции выдавливания
            extrusionDef.SetSketch(iSketch);
            // Направление выдавливания
            if (type == false)
            {
                extrProp.direction = (short)Direction_Type.dtReverse;
            }
            if (type == true)
            {
                extrProp.direction = (short)Direction_Type.dtNormal;
            }
            // Тип выдавливания
            extrProp.typeNormal = (short)End_Type.etBlind;
            // Глубина выдавливания
            if (type == false)
            {
                extrProp.depthReverse = depth;
            }
            if (type == true)
            {
                extrProp.depthNormal = depth;
            }
            // Создание операции
            entityExtr.Create();
        }

        /// <summary>
        /// Скругление относительно грани
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="X">Координата X</param>
        /// <param name="Z">Координата Z</param>
        /// <param name="Y">Координата Y</param>
        private void Fillet(ksPart iPart, double X, double Z, double Y)
        {
            // Получение интерфейса объекта скругление
            ksEntity entityFillet = 
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_fillet);
            // Получаем интерфейс параметров объекта скругление
            ksFilletDefinition filletDefinition =
                (ksFilletDefinition)entityFillet.GetDefinition();
            // Радиус скругления 
            filletDefinition.radius = 2;
            // Не продолжать по касательным ребрам
            filletDefinition.tangent = false;
            // Получаем массив граней объекта
            ksEntityCollection entityCollectionPart =
                (ksEntityCollection)iPart.EntityCollection((short)Obj3dType.o3d_face);
            // Получаем массив скругляемых граней
            ksEntityCollection entityCollectionFillet =
                (ksEntityCollection)filletDefinition.array();
            entityCollectionFillet.Clear();
            // Заполняем массив скругляемых объектов
            entityCollectionPart.SelectByPoint(X,  -Y, -Z);
            entityCollectionFillet.Add(entityCollectionPart.First());
            // Создаем скругление
            entityFillet.Create();
        }
    }
}
