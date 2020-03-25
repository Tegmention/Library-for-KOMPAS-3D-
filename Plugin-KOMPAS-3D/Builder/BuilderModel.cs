using Kompas6API5;
using Parameters;
using Kompas6Constants3D;


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
            //Получение интерфейса детали
            ksPart iPart = (ksPart)iDocument3D.GetPart((short)Part_Type.pTop_Part);
            CreateCase(iPart);
            CreateSpeakers(iDocument3D, iPart);
            CreateRele(iPart);
        }

        /// <summary>
        /// Создание эскиза
        /// </summary>
        /// <returns>Возврат нового эскиза</returns>
        private ksEntity NewSketch(ksPart iPart)
        {
            //Получаем интерфейс базовой плоскости ХОY
            ksEntity planeZOY =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            //Создаем новый эскиз
            ksEntity iSketch =
                    (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            return iSketch;
        }

        /// <summary>
        /// Построение корпуса колонки
        /// </summary>
        private void CreateCase(ksPart iPart)
        {
            //Получаем интерфейс базовой плоскости ХОY
            ksEntity planeZOY =
                iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            //Создаем новый эскиз
            ksEntity iSketch = NewSketch(iPart);
            //Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = iSketch.GetDefinition();
            //Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeZOY);
            //Создание эскиза
            iSketch.Create();
            //Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            var W = _modelelElements.Element(ElementName.Case).Parameter(ParametersName.W).Value;
            var H = _modelelElements.Element(ElementName.Case).Parameter(ParametersName.H).Value;
            var L = _modelelElements.Element(ElementName.Case).Parameter(ParametersName.L).Value;
            CreateSketchRectangle(iDocument2D, H, 0, W, 0);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPart, iSketch, L, true);
            Fillet(iPart, 20, 20 , 0);
        }

        /// <summary>
        /// Построение реле регулировки
        /// </summary>
        private void CreateRele(ksPart iPatr)
        {
            //Получаем интерфейс базовой плоскости ХОY
            ksEntity planeZOY =
                (ksEntity)iPatr.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            //Создаем новый эскиз
            ksEntity iSketch =
                (ksEntity)iPatr.NewEntity((short)Obj3dType.o3d_sketch);
            //Получаем интерфейс свойств эскиза
                ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
            //Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeZOY);
            //Создание эскиза
            iSketch.Create();
            //Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();
            var rad = _modelelElements.Element(ElementName.Rele).Parameter(ParametersName.D).Value / 2;
            var partL = _modelelElements.Element(ElementName.Case).Parameter(ParametersName.W).Value / 5;
            var height = 2.5 + rad;

            iDocument2D.ksCircle(partL * 4, height, rad, 1);
            iDocument2D.ksCircle(partL * 3, height, rad, 1);
            iDocument2D.ksCircle(partL * 2, height, rad, 1);
            iDocument2D.ksCircle(partL, height, 4, 1);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPatr, iSketch, 12, false);
        }

        /// <summary>
        /// Построение прямоугольного динамика
        /// </summary>
        private void CreateRectanglSpeaker(ksDocument3D iDocument3D, ksPart iPart, double maxH, double minH,
            double maxW, double minW, ModelElement element)
        {
            //Получаем интерфейс базовой плоскости ХОY
            ksEntity planeZOY =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            //Создаем новый эскиз
            ksEntity iSketch =
            (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            //Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
            //Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeZOY);
            //Создание эскиза
            iSketch.Create();
            //Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();           
            CreateSketchRectangle(iDocument2D, maxH, minH, maxW, minW);
            iDefinitionSketch.EndEdit();
            
            ExctrusionSketch(iPart, iSketch, element.Parameter(ParametersName.L).Value, false);
        }

        /// <summary>
        /// Построение круглого динамика
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="X">Координаты X</param>
        /// <param name="Y">Координаты Y</param>
        /// <param name="rad">Радиус круга</param>
        /// <param name="element">Элемент модели</param>
        private void CreateCirkleSpeaker(ksPart iPart, double X, double Y, double rad,  ModelElement element)
        {
            //Получаем интерфейс базовой плоскости ХОY
            ksEntity planeZOY =
                (ksEntity)iPart.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            //Создаем новый эскиз
            ksEntity iSketch =
            (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            //Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch = (ksSketchDefinition)iSketch.GetDefinition();
            //Устанавливаем плоскость эскиза
            iDefinitionSketch.SetPlane(planeZOY);
            //Создание эскиза
            iSketch.Create();
            //Создание нового 2Д документа
            ksDocument2D iDocument2D = (ksDocument2D)iDefinitionSketch.BeginEdit();           
            iDocument2D.ksCircle( X, Y, rad, 1);
            iDefinitionSketch.EndEdit();

            ExctrusionSketch(iPart, iSketch, element.Parameter(ParametersName.L).Value, false);
            Fillet(iPart, X, Y, element.Parameter(ParametersName.L).Value);

            ksEntity iSketch1 =
                (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_sketch);
            //Получаем интерфейс свойств эскиза
            ksSketchDefinition iDefinitionSketch1 = (ksSketchDefinition)iSketch1.GetDefinition();
            iDefinitionSketch1.SetPlane(planeZOY);
            //Создание эскиза
            iSketch1.Create();

            //Создание 2Д документа
            ksDocument2D iDocument2D1 = (ksDocument2D)iDefinitionSketch1.BeginEdit();
            iDocument2D1.ksCircle(X, Y, rad * 0.9, 1);
            iDocument2D1.ksCircle(X, Y, rad * 0.8, 1);
            iDefinitionSketch1.EndEdit();

            ExctrusionSketch(iPart, iSketch1, element.Parameter(ParametersName.L).Value + 4, false);
            Fillet(iPart, X + (rad * 0.9), Y, element.Parameter(ParametersName.L).Value + 4);
        }

        /// <summary>
        /// Расчет и создание динамиков модели
        /// исходя из их количества
        /// </summary>
        /// <param name="iDocument3D"></param>
        /// <param name="iPart"></param>
        private void CreateSpeakers(ksDocument3D iDocument3D, ksPart iPart)
        {
            //Максимальная сумма высот динамиков
            var maxDinamic = _modelelElements.CalculationMaxDinamics();
            //Число динамиков
            var numberDinamic = _modelelElements.NumberDinamics();
            //Максимальная ширина динамиков
            var maxWCase = _modelelElements.Element(ElementName.Case).Parameter(ParametersName.W).Value;
            var result = 10 + _modelelElements.Element(ElementName.Rele).Parameter(ParametersName.D).Value;

            //Если существует 1 динамик
            if ( numberDinamic  == 1)
            {
                //Высота 1 динамика
                var H = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.H).Value;
                //Ширина 1 динамика
                var W = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).Value;
                if (_modelelElements.Element(ElementName.SpeakerCover1).FormKey() == false)
                {
                    var pointMaxHDinamic = (maxDinamic / 2) + (H / 2) + result;
                    var pointMinHDinamic = (maxDinamic / 2) - (H / 2) + result;
                    var pointMaxwDinamic = (maxWCase / 2) + (W / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                    var L = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, (maxDinamic / 2) + result, L);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = (maxDinamic / 2) + result;
                    var rad = H / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                }
            }

            //Если существует 2 динамика
            if (numberDinamic == 2)
            {
                //Часть высоты для 1 динамика
                var PartMaxHDinamic = maxDinamic / 2;
                //Часть высоты для 2 динамика
                var PartMaxHDinamic1 = maxDinamic / 2;
                var H = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.H).Value;
                var W = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).Value;
                var H1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.H).Value;
                var W1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.W).Value;
                //Перераспределение частей высот
                if ( H > PartMaxHDinamic)
                {
                    PartMaxHDinamic = H;
                    PartMaxHDinamic1 = maxDinamic - PartMaxHDinamic;
                }
                if (H1 > PartMaxHDinamic1)
                {
                    PartMaxHDinamic1 = H1;
                    PartMaxHDinamic = maxDinamic - PartMaxHDinamic1;
                }
                var middlePart = PartMaxHDinamic / 2;
                var middlePart1 = PartMaxHDinamic1 / 2;
                //Параметры 1 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover1).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic1 + middlePart + (H / 2) + 2 ; 
                    var pointMinHDinamic = result + PartMaxHDinamic1 + middlePart - (H / 2) + 2;
                    var pointMaxwDinamic = (maxWCase / 2) + (W / 2); 
                    var pointMinwDinamic = (maxWCase / 2) - (W / 2); 
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                    var L = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic1 + middlePart + 2, L);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + PartMaxHDinamic1 + middlePart + 2;
                    var rad = H / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                    _modelelElements.Element(ElementName.SpeakerCover1));
                }
                //Параметры 2 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover2).FormKey() == false)
                {
                    var pointMaxHDinamic = result + middlePart1 + (H1 / 2);
                    var pointMinHDinamic = result + middlePart1 - (H1 / 2);
                    var pointMaxwDinamic = (maxWCase / 2) + (W1 / 2); 
                    var pointMinwDinamic = (maxWCase / 2) - (W1 / 2); 
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                    var L1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + middlePart1, L1);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + middlePart1; 
                    var rad = H1 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                }
            }

            //Если существует 3 динамика
            if (numberDinamic == 3)
            {
                var PartMaxHDinamic = maxDinamic / 3;
                var PartMaxHDinamic1 = maxDinamic / 3;
                var PartMaxHDinamic2 = maxDinamic / 3;
                var H = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.H).Value;
                var W = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).Value;
                var H1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.H).Value;
                var W1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.W).Value;
                var H2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.H).Value;
                var W2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.W).Value;
                //Перераспределение частей высот
                if (H > PartMaxHDinamic)
                {
                    PartMaxHDinamic = H;
                    PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic) / 2;
                    PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic) / 2;
                }
                if (H1 > PartMaxHDinamic1)
                {
                    PartMaxHDinamic1 = H1;
                    if (PartMaxHDinamic == maxDinamic / 3)
                    {
                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic1) / 2;
                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic1) / 2;
                    }
                    else
                    {
                        PartMaxHDinamic2 = maxDinamic - PartMaxHDinamic - PartMaxHDinamic1;
                    }
                }
                if (H2 > PartMaxHDinamic2)
                {
                    PartMaxHDinamic2 = H2;
                    if(PartMaxHDinamic > maxDinamic / 3)
                    {
                        PartMaxHDinamic1 = maxDinamic - PartMaxHDinamic - PartMaxHDinamic2;
                    }
                    if (PartMaxHDinamic1 > maxDinamic / 3)
                    {
                        PartMaxHDinamic = maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic2;
                    }
                    if(PartMaxHDinamic == (maxDinamic / 3)
                        && (PartMaxHDinamic1 == maxDinamic / 3))
                    {
                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic2) / 2;
                        PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic2) / 2;
                    }
                }
                var middlePart = PartMaxHDinamic / 2;
                var middlePart1 = PartMaxHDinamic1 / 2;
                var middlePart2 = PartMaxHDinamic2 / 2;
                //Параметры 1 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover1).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic1 + PartMaxHDinamic2 + middlePart + (H / 2) + 2;
                    var pointMinHDinamic = result + PartMaxHDinamic1 + PartMaxHDinamic2 + middlePart - (H / 2) + 2;
                    var pointMaxwDinamic = (maxWCase / 2) + (W / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                    var L = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic1 + PartMaxHDinamic2 + middlePart + 2, L);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + PartMaxHDinamic1 + PartMaxHDinamic2 + middlePart + 2;
                    var rad = H / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                }
                //Параметры 2 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover2).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic2 + middlePart1 + (H1 / 2);
                    var pointMinHDinamic = result + PartMaxHDinamic2 + middlePart1 - (H1 / 2);
                    var pointMaxwDinamic = (maxWCase / 2) + (W1 / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W1 / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                    var L1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic2 + middlePart1, L1);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + PartMaxHDinamic2 + middlePart1;
                    var rad = H1 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                }
                //Параметры 3 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover3).FormKey() == false)
                {
                    var pointMaxHDinamic = result + middlePart2 + (H2 / 2) - 2;
                    var pointMinHDinamic = result + middlePart2 - (H2 / 2) - 2;
                    var pointMaxwDinamic = (maxWCase / 2) + (W2 / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W2 / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover3));
                    var L2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + middlePart2 - 2, L2);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + middlePart2 - 2;
                    var rad = H2 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover3));
                }
            }

            //Если существует 4 динамика
            if (numberDinamic == 4)
            {
                var PartMaxHDinamic = maxDinamic / 4;
                var PartMaxHDinamic1 = maxDinamic / 4;
                var PartMaxHDinamic2 = maxDinamic / 4;
                var PartMaxHDinamic3 = maxDinamic / 4;
                var H = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.H).Value;
                var W = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.W).Value;
                var H1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.H).Value;
                var W1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.W).Value;
                var H2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.H).Value;
                var W2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.W).Value;
                var H3 = _modelelElements.Element(ElementName.SpeakerCover4).Parameter(ParametersName.H).Value;
                var W3 = _modelelElements.Element(ElementName.SpeakerCover4).Parameter(ParametersName.W).Value;
                //Перераспределение частей высот
                if (H > PartMaxHDinamic)
                {
                    PartMaxHDinamic = H;
                    PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic) / 3;
                    PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic) / 3;
                    PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic) / 3;
                }
                if ( H1 > PartMaxHDinamic1)
                {
                    PartMaxHDinamic1 = H1;
                    if(PartMaxHDinamic == maxDinamic / 4)
                    {
                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic1) / 3;
                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic1) / 3;
                        PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic1) / 3;
                    }
                    else
                    {
                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic1) / 2;
                        PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic1) / 2;
                    }
                }
                if ( H2 > PartMaxHDinamic2)
                {
                    PartMaxHDinamic2 = H2;
                    if(PartMaxHDinamic > maxDinamic / 4 && PartMaxHDinamic1 > maxDinamic / 4)
                    {
                        PartMaxHDinamic3 = maxDinamic - PartMaxHDinamic - PartMaxHDinamic1 - PartMaxHDinamic2;
                    }
                    else
                    {
                        if (PartMaxHDinamic > maxDinamic / 4)
                        {

                            PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic2) / 2;
                            PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic2) / 2;
                        }

                        if (PartMaxHDinamic1 > maxDinamic / 4)
                        {
                            PartMaxHDinamic = (maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic2) / 2;
                            PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic2) / 2;
                        }
                        if (PartMaxHDinamic == maxDinamic / 4
                            && PartMaxHDinamic1 == maxDinamic / 4)
                        {
                            PartMaxHDinamic = (maxDinamic - PartMaxHDinamic2) / 3;
                            PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic2) / 3;
                            PartMaxHDinamic3 = (maxDinamic - PartMaxHDinamic2) / 3;
                        }
                    }
                }
                if (H3 > PartMaxHDinamic3)
                {
                    PartMaxHDinamic3 = H3;
                    if (PartMaxHDinamic == maxDinamic / 4 && PartMaxHDinamic1 == maxDinamic / 4
                                    && PartMaxHDinamic2 == maxDinamic / 4)
                    {
                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic3) / 3;
                        PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic3) / 3;
                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic3) / 3;
                    }
                    else
                    {
                        if (PartMaxHDinamic > maxDinamic / 4 && PartMaxHDinamic1 > maxDinamic / 4)
                        {
                            PartMaxHDinamic2 = maxDinamic - PartMaxHDinamic - PartMaxHDinamic1 - PartMaxHDinamic3;
                        }
                        else
                        {
                            if (PartMaxHDinamic > maxDinamic / 4 && PartMaxHDinamic2 > maxDinamic / 4)
                            {
                                PartMaxHDinamic1 = maxDinamic - PartMaxHDinamic - PartMaxHDinamic2 - PartMaxHDinamic3;
                            }
                            else
                            {
                                if (PartMaxHDinamic1 > maxDinamic / 4 && PartMaxHDinamic2 > maxDinamic / 4)
                                {
                                    PartMaxHDinamic = maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic2 - PartMaxHDinamic3;
                                }
                                else
                                {
                                    if (PartMaxHDinamic > maxDinamic / 4)
                                    {
                                        PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic3) / 2;
                                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic - PartMaxHDinamic3) / 2;
                                    }
                                    if (PartMaxHDinamic1 > maxDinamic / 4)
                                    {
                                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic3) / 2;
                                        PartMaxHDinamic2 = (maxDinamic - PartMaxHDinamic1 - PartMaxHDinamic3) / 2;
                                    }
                                    if (PartMaxHDinamic2 > maxDinamic / 4)
                                    {
                                        PartMaxHDinamic = (maxDinamic - PartMaxHDinamic2 - PartMaxHDinamic3) / 2;
                                        PartMaxHDinamic1 = (maxDinamic - PartMaxHDinamic2 - PartMaxHDinamic3) / 2;
                                    }
                                }
                            }
                        }
                    }
                }
                var middlePart = PartMaxHDinamic / 2;
                var middlePart1 = PartMaxHDinamic1 / 2;
                var middlePart2 = PartMaxHDinamic2 / 2;
                var middlePart3 = PartMaxHDinamic3 / 2;
                //Параметры 1 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover1).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic1 + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart + (H / 2);
                    var pointMinHDinamic = result + PartMaxHDinamic1 + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart - (H / 2);
                    var pointMaxwDinamic = (maxWCase / 2) + (W / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                    var L = _modelelElements.Element(ElementName.SpeakerCover1).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic1 + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart, L);
                }
                else
                {
                    var X = maxWCase / 2;
                    var Y = result + PartMaxHDinamic1 + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart;
                    var rad = H / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover1));
                }
                //Параметры 2 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover2).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart1 + (H1 / 2) - 2;
                    var pointMinHDinamic = result + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart1 - (H1 / 2) - 2;
                    var pointMaxwDinamic = (maxWCase / 2) + (W1 / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W1 / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                    var L1 = _modelelElements.Element(ElementName.SpeakerCover2).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart1 - 2, L1);
                }
                else
                {
                    var X = (maxWCase / 2);
                    var Y = result + PartMaxHDinamic2 + PartMaxHDinamic3 + middlePart1 - 2;
                    var rad = H1 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover2));
                }
                //Параметры 3 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover3).FormKey() == false)
                {
                    var pointMaxHDinamic = result + PartMaxHDinamic3 + middlePart2 + (H2 / 2) - 4;
                    var pointMinHDinamic = result + PartMaxHDinamic3 + middlePart2 - (H2 / 2) - 4;
                    var pointMaxwDinamic = (maxWCase / 2) + (W2 / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W2 / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover3));
                    var L2 = _modelelElements.Element(ElementName.SpeakerCover3).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + PartMaxHDinamic3 + middlePart2 - 4, L2);
                }
                else
                {
                    var X = (maxWCase / 2);
                    var Y = result + PartMaxHDinamic3 + middlePart2 - 4;
                    var rad = H2 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover3));
                }
                //Параметры 4 динамика
                if (_modelelElements.Element(ElementName.SpeakerCover4).FormKey() == false)
                {
                    var pointMaxHDinamic = result + middlePart3 + (H3 / 2) - 6;
                    var pointMinHDinamic = result + middlePart3 - (H3 / 2) - 6;
                    var pointMaxwDinamic = (maxWCase / 2) + (W3 / 2);
                    var pointMinwDinamic = (maxWCase / 2) - (W3 / 2);
                    CreateRectanglSpeaker(iDocument3D, iPart, pointMaxHDinamic, pointMinHDinamic, pointMaxwDinamic, pointMinwDinamic,
                        _modelelElements.Element(ElementName.SpeakerCover4));
                    var L3 = _modelelElements.Element(ElementName.SpeakerCover4).Parameter(ParametersName.L).Value;
                    Fillet(iPart, maxWCase / 2, result + middlePart3 - 6, L3);
                }
                else
                {
                    var X = (maxWCase / 2);
                    var Y = result + middlePart3 - 6;
                    var rad = H3 / 2;
                    CreateCirkleSpeaker(iPart, X, Y, rad,
                        _modelelElements.Element(ElementName.SpeakerCover4));
                }
       
            }
        }

        /// <summary>
        /// Создание чертежа прямоугольника по
        /// заданным координатам
        /// </summary>
        /// <param name="iDocument2D">Интерфейс эскиза</param>
        /// <param name="maxH">Максимальная высота</param>
        /// <param name="minH">Минимальная высота</param>
        /// <param name="maxW">Максимальная ширина</param>
        /// <param name="minW">Минимальная ширина</param>
        private void CreateSketchRectangle(ksDocument2D iDocument2D, double maxH, double minH,
            double maxW, double minW)
        {
            iDocument2D.ksLineSeg( minW, minH, maxW, minH, 1);
            iDocument2D.ksLineSeg( minW, minH, minW, maxH, 1);
            iDocument2D.ksLineSeg( minW, maxH, maxW, maxH, 1);
            iDocument2D.ksLineSeg( maxW, minH, maxW, maxH, 1);
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
            //Операция выдавливание
            ksEntity entityExtr = (ksEntity)iPart.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            //Интерфейс операции выдавливания
            ksBossExtrusionDefinition extrusionDef =
                (ksBossExtrusionDefinition)entityExtr.GetDefinition();                
            //Интерфейс структуры параметров выдавливания
            ksExtrusionParam extrProp =
                (ksExtrusionParam)extrusionDef.ExtrusionParam();    
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

        /// <summary>
        /// Скругление относительно граней
        /// </summary>
        /// <param name="iPart">Интерфейс детали</param>
        /// <param name="type">Тип скругляемой грани</param>
        private void Fillet(ksPart iPart, double X, double Z, double Y)
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
            entityCollectionPart.SelectByPoint(X,  -Y, -Z);
            entityCollectionFillet.Add(entityCollectionPart.First());
            //Создаем скругление
            entityFillet.Create();
        }
    }
}
