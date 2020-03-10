using Kompas6API5;
using Parameters;
using Kompas6Constants3D;


namespace Builder
{
    /// <summary>
    /// Класс для построения 3Д модели 
    /// в САПР Компас 3Д
    /// </summary>
    class BuilderModel
    {
        /// <summary>
        /// Хранит ссылку на экземпляр Компас 3Д
        /// </summary>
        private KompasObject _kompasObject;

        /// <summary>
        /// Хранит параметры модели колонки
        /// </summary>
        private ModelParameters _modelParameters;

        /// <summary>
        /// Конструктор класса BuilderModel
        /// </summary>
        /// <param name="parameters">Параметры модели</param>
        /// <param name="kompas">Экзепляр Компас 3Д</param>
        public BuilderModel(ModelParameters parameters, KompasObject kompas)
        {
            _modelParameters = parameters;
            _kompasObject = kompas;
            CreateModel();
        }

        /// <summary>
        /// Построения 3Д модели
        /// </summary>
        private void CreateModel()
        {
            ksDocument3D iDocument3D = (ksDocument3D)_kompasObject.Document3D();
            iDocument3D.Create(false,true);
            ksPart iPatr = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);
            CreateCase(iDocument3D, iPatr);
            CreateSpeaker(iDocument3D, iPatr);
            CreateRele(iDocument3D, iPatr);
        }

        /// <summary>
        /// Построение корпуса колонки
        /// </summary>
        private void CreateCase(ksDocument3D iDocument3D, ksPart iPart)
        {
            if(iPart != null)
            {
                //Получаем интерфейс базовой плоскости ХОY
                ksEntity planeZOY = 
                    (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                //Создаем новый эскиз
                ksEntity iSketch = 
                    (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                if(iSketch != null)
                {
                    //Получаем интерфейс свойств эскиза
                    ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                    if(iDefinitionSketch != null)
                    {
                        //Устанавливаем плоскость эскиза
                        iDefinitionSketch.SetPlane(planeZOY);
                        //Создание эскиза
                        iSketch.Create();

                        //Создание нового 2Д документа
                        ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                        iDocument2D.ksLineSeg(0, 0, _modelParameters.Parameters["L"].Value, 0, 1);
                        iDocument2D.ksLineSeg(0, 0, 0, _modelParameters.Parameters["H"].Value, 1);
                        iDocument2D.ksLineSeg(0, _modelParameters.Parameters["H"].Value,
                            _modelParameters.Parameters["L"].Value, _modelParameters.Parameters["H"].Value, 1);
                        iDocument2D.ksLineSeg(_modelParameters.Parameters["L"].Value, 0,
                            _modelParameters.Parameters["L"].Value, _modelParameters.Parameters["H"].Value, 1);
                        iDefinitionSketch.EndEdit();

                        //Операция выдавливание
                        ExctrusionSketch(iPart, iSketch, _modelParameters.Parameters["W"].Value, true);
                        Fillet(iPart, "case");
                    }
                }
            }
        }

        /// <summary>
        /// Построение реле регулировки
        /// </summary>
        private void CreateRele(ksDocument3D iDocument3D, ksPart iPatr)
        {
            if (iPatr != null)
            {
                //Получаем интерфейс базовой плоскости ХОY
                ksEntity planeZOY =
                    (ksEntity)iPatr.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                //Создаем новый эскиз
                ksEntity iSketch =
                    (ksEntity)iPatr.NewEntity((short)Obj3dType.o3d_sketch);
                if (iSketch != null)
                {
                    //Получаем интерфейс свойств эскиза
                    ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                    if (iDefinitionSketch != null)
                    {
                        //Устанавливаем плоскость эскиза
                        iDefinitionSketch.SetPlane(planeZOY);
                        //Создание эскиза
                        iSketch.Create();

                        //Создание нового 2Д документа
                        ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                        var rad = _modelParameters.Parameters["D"].Value / 2;
                        var partL = _modelParameters.Parameters["L"].Value / 5;
                        var height = 2.5 + rad;
                        iDocument2D.ksCircle(partL * 4, height, rad, 1);
                        iDocument2D.ksCircle(partL * 3, height, rad, 1);
                        iDocument2D.ksCircle(partL * 2, height, rad , 1);
                        iDocument2D.ksCircle(partL, height, 4, 1);
                        iDefinitionSketch.EndEdit();

                        //Операция выдавливание
                        ExctrusionSketch(iPatr, iSketch, 12, false);
                    }
                }
            }
        }

        /// <summary>
        /// Построение динамика колонки
        /// 15
        /// угол 2
        /// 
        /// </summary>
        private void CreateSpeaker(ksDocument3D iDocument3D, ksPart iPart)
        { 
            if (iPart != null)
            {
                //Получаем интерфейс базовой плоскости ХОY
                ksEntity planeZOY =
                    (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                //Создаем новый эскиз
                ksEntity iSketch =
                    (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
                if (iSketch != null)
                {
                    //Получаем интерфейс свойств эскиза
                    ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
                    if (iDefinitionSketch != null)
                    {
                        //Устанавливаем плоскость эскиза
                        iDefinitionSketch.SetPlane(planeZOY);
                        //Создание эскиза
                        iSketch.Create();

                        //Создание нового 2Д документа
                        ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
                        var middleMaxHS = _modelParameters.Parameters["HS"].MaxValue/2;
                        var middleHS = _modelParameters.Parameters["HS"].Value / 2;
                        var middleLS = _modelParameters.Parameters["LS"].Value / 2;
                        var middleL = _modelParameters.Parameters["L"].Value / 2;
                        var result = 10 + _modelParameters.Parameters["D"].Value;
                        iDocument2D.ksLineSeg(
                            middleL - middleLS, 
                            middleMaxHS - middleHS + result,
                            middleL - middleLS,
                            middleMaxHS + middleHS + result,
                            1);
                        iDocument2D.ksLineSeg(
                            middleL - middleLS,
                            middleMaxHS + middleHS + result,
                            middleL + middleLS,
                            middleMaxHS + middleHS + result,
                            1);
                        iDocument2D.ksLineSeg(
                            middleL + middleLS,
                            middleMaxHS + middleHS + result,
                            middleL + middleLS,
                            middleMaxHS - middleHS + result,
                            1);
                        iDocument2D.ksLineSeg(
                            middleL - middleLS,
                            middleMaxHS - middleHS + result,
                            middleL + middleLS,
                            middleMaxHS - middleHS + result,
                            1);
                        iDefinitionSketch.EndEdit();

                        //Операция выдавливание
                        ExctrusionSketch(iPart, iSketch, _modelParameters.Parameters["WS"].Value, false);
                        Fillet(iPart, "speaker");
                    }
                }
            }
        }

        /// <summary>
        /// Выдавливание по эскизу
        /// </summary>
        private void ExctrusionSketch (ksPart iPatr, ksEntity iSketch, double depth, bool type)
        {
            //Операция выдавливание
            ksEntity entityExtr = (ksEntity)iPatr.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            if (entityExtr != null)
            {
                //Интерфейс операции выдавливания
                ksBossExtrusionDefinition extrusionDef =
                    (ksBossExtrusionDefinition)entityExtr.GetDefinition();
                if (extrusionDef != null)
                {
                    //Интерфейс структуры параметров выдавливания
                    ksExtrusionParam extrProp =
                        (ksExtrusionParam)extrusionDef.ExtrusionParam();
                    if (extrProp != null)
                    {
                        //Эскиз операции выдавливания
                        extrusionDef.SetSketch(iSketch);

                        //Направление выдавливания
                        if (type == false)
                        {
                            extrProp.direction = (short)Direction_Type.dtReverse;
                        }
                        if (type == true)
                        {
                            extrProp.direction = (short)Direction_Type.dtNormal;
                        }
                        //Тип выдавливания
                        extrProp.typeNormal = (short)End_Type.etBlind;
                        //Глубина выдавливания
                        if (type == false)
                        {
                            extrProp.depthReverse = depth;
                        }
                        if (type == true)
                        {
                            extrProp.depthNormal = depth;
                        }
                        //Создание операции
                        entityExtr.Create();
                    }
                }
            }
        }

      /// <summary>
      /// Скругление относительно граней
      /// </summary>
      /// <param name="iPart">Объект</param>
      /// <param name="type">Тип скругляемой грани</param>
        private void Fillet(ksPart iPart, string type)
        {
            //Получение интерфейса объекта скругление
            ksEntity entityFillet = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_fillet);
            //Получаем интерфейс параметров объекта скругление
            ksFilletDefinition filletDefinition =
                (ksFilletDefinition)entityFillet.GetDefinition();
            //Радиус скругления 
            filletDefinition.radius = 2;
            //Не продолжать по касательным ребрам
            filletDefinition.tangent = false;
            //Получаем массив граней объекта
            ksEntityCollection entityCollectionPart =
                (ksEntityCollection)iPart.EntityCollection((short)Obj3dType.o3d_face);
            //Получаем массив скругляемых граней
            ksEntityCollection entityCollectionFillet =
                (ksEntityCollection)filletDefinition.array();
            entityCollectionFillet.Clear();
            //Заполняем массив скругляемых объектов
            //Грань корпуса
            if(type == "case")
            {
                entityCollectionPart.SelectByPoint(20, 0, -20);
            }
            //Грань динамика
            if (type == "speaker")
            {
                entityCollectionPart.SelectByPoint(
                    _modelParameters.Parameters["L"].Value / 2,
                    -_modelParameters.Parameters["WS"].Value,
                    -_modelParameters.Parameters["H"].Value / 2);
            }
            entityCollectionFillet.Add(entityCollectionPart.First());
            //Создаем скругление
            entityFillet.Create();
        }
    }
}
