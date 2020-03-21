using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Parameters;
using System.Drawing;
using Builder;


namespace Plugin_KOMPAS_3D.UI
{
    /// <summary>
    /// Форма для ввода пользователем параметров модели
    /// </summary>
    public partial class ModelParametersForm : Form
    {
        /// <summary>
        /// Поле хранит элементы модели
        /// </summary>
        private ModelElements _modelElements = new ModelElements();

        /// <summary>
        /// Хранит словарь соответствий TextBox и параметра элемента
        /// </summary>
        private Dictionary<TextBox, List<(ElementName element, ParametersName parameter)>> _elements =
            new Dictionary<TextBox, List<(ElementName element, ParametersName parameter)>>();
        
        public ModelParametersForm()
        {
            //Инициализация формы
            InitializeComponent();
            this.Size = new System.Drawing.Size(683, 345);
            //Создание списка элементов TextBox
                    var elements = new List<(TextBox textBox,ElementName element, ParametersName parameter)>
                    {
                        (CaseHeightTextBox, ElementName.Case, ParametersName.H),
                        (CaseLengthTextBox, ElementName.Case, ParametersName.W),
                        (CaseWidthTextBox, ElementName.Case, ParametersName.L),
                        (RelayDiameterTextBox, ElementName.Rele, ParametersName.D),
                        (SpeakerTTextBox, ElementName.SpeakerCover1, ParametersName.L),
                        (SpeakerWidthTextBox, ElementName.SpeakerCover1, ParametersName.W),
                        (SpeakerHeightTextBox,ElementName.SpeakerCover1, ParametersName.H),
                        (SpeakerT1TextBox, ElementName.SpeakerCover2, ParametersName.L),
                        (SpeakerWidth1TextBox, ElementName.SpeakerCover2, ParametersName.W),
                        (SpeakerHeight1TextBox,ElementName.SpeakerCover2, ParametersName.H),
                        (SpeakerT2TextBox, ElementName.SpeakerCover3, ParametersName.L),
                        (SpeakerWidth2TextBox, ElementName.SpeakerCover3, ParametersName.W),
                        (SpeakerHeight2TextBox,ElementName.SpeakerCover3, ParametersName.H),
                        (SpeakerT3TextBox, ElementName.SpeakerCover4, ParametersName.L),
                        (SpeakerWidth3TextBox, ElementName.SpeakerCover4, ParametersName.W),
                        (SpeakerHeight3TextBox,ElementName.SpeakerCover4, ParametersName.H),
                    };

            foreach (var element in elements)
            {
                var parametersName = new List<(ElementName element, ParametersName parameter)>
                {
                    (element.element, element.parameter)
                };
                _elements.Add(element.textBox, parametersName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisplayingParameters()
        {
            if (_modelElements.IsElement(ElementName.SpeakerCover1))
            {
                Displaying(BoundaryValueHSLabel, ParametersName.H, ElementName.SpeakerCover1);
                Displaying(BoundaryValueWSLabel, ParametersName.W, ElementName.SpeakerCover1);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover2))
            {
                Displaying(BoundaryValueWS1Label, ParametersName.W, ElementName.SpeakerCover2);
                Displaying(BoundaryValueHS1Label, ParametersName.H, ElementName.SpeakerCover2);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover3))
            {
                Displaying(BoundaryValueWS2Label, ParametersName.W, ElementName.SpeakerCover3);
                Displaying(BoundaryValueHS2Label, ParametersName.H, ElementName.SpeakerCover3);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover4))
            {
                Displaying(BoundaryValueWS3Label, ParametersName.W, ElementName.SpeakerCover4);
                Displaying(BoundaryValueHS3Label, ParametersName.H, ElementName.SpeakerCover4);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="name"></param>
        /// <param name="elementName"></param>
        public void Displaying(Label label, ParametersName name, ElementName elementName)
        {
                label.Text = "(от " + string.Concat(_modelElements.Element(elementName).Parameter(name).MinValue) + " до "
                            + string.Concat(_modelElements.Element(elementName).Parameter(name).MaxValue) + ") мм";
        }

        private void AddDinamicButton_Click(object sender, EventArgs e)
        {
            if (NumberDinamicTextBox.Text != "4")
            {
                NumberDinamicTextBox.Text =
                       (double.Parse(NumberDinamicTextBox.Text) + 1).ToString();
                _modelElements.AddDinamic();
                if (NumberDinamicTextBox.Text == "2")
                {
                    SpeakerDimensions2GroupBox.Visible = true;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    this.Size = new System.Drawing.Size(683, 488);
                    SpeakerDimensions3GroupBox.Visible = true;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "4")
                {
                    SpeakerDimensions4GroupBox.Visible = true;
                    DisplayingParameters();
                }
                DisplayingBoundary(SpeakerHeightTextBox);
                DisplayingBoundary(SpeakerHeight1TextBox);
                DisplayingBoundary(SpeakerHeight2TextBox);
                DisplayingBoundary(SpeakerHeight3TextBox);
            }
        }

        private void DeleteDinamicButton_Click(object sender, EventArgs e)
        {
            if(NumberDinamicTextBox.Text != "1")
            {
                _modelElements.DeleteDinamic();
                if (NumberDinamicTextBox.Text == "4")
                {
                    SpeakerDimensions4GroupBox.Visible = false;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    this.Size = new System.Drawing.Size(683, 345);
                    SpeakerDimensions3GroupBox.Visible = false;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "2")
                {
                    SpeakerDimensions2GroupBox.Visible = false;
                    DisplayingParameters();
                }
                NumberDinamicTextBox.Text =
                      (double.Parse(NumberDinamicTextBox.Text) - 1).ToString();
            }  
        }

        /// <summary>
        /// Изменяет некорректное значение введенное пользователем
        /// на последнее вводимое корректное значение
        /// при потере фокуса элементом TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Leave(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.BackColor == Color.Salmon)
            {
                var information = _elements[textBox];
                var parameters = information[0];
                var element = parameters.element;
                var parameter = parameters.parameter;
                textBox.Text =
                    string.Concat(_modelElements.Element(element).Parameter(parameter).Value);
                textBox.BackColor = Color.LightGreen;
            }
        }

        private void TextBoxChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            try 
            {
              var information = _elements[textBox];
                var parameters = information[0];
                var element = parameters.element;
                var parameter = parameters.parameter;
                var value = double.Parse(textBox.Text);
                _modelElements.Element(element).Parameter(parameter).Value = value;
                textBox.BackColor = Color.LightGreen;
                //Заменить на элемент перечисления 
                if (parameter == ParametersName.H || parameter == ParametersName.D)
                {
                    _modelElements.CalculationMaxHDinamics();
                    DisplayingBoundary(SpeakerHeightTextBox);
                    DisplayingBoundary(SpeakerHeight1TextBox);
                    DisplayingBoundary(SpeakerHeight2TextBox);
                    DisplayingBoundary(SpeakerHeight3TextBox);
                    DisplayingParameters();
                }
                //Заменить на элемент перечисления
                if (parameter == ParametersName.W)
                {
                    _modelElements.CalculationMaxWDinamic();
                    DisplayingBoundary(SpeakerWidthTextBox);
                    DisplayingBoundary(SpeakerWidth1TextBox);
                    DisplayingBoundary(SpeakerWidth2TextBox);
                    DisplayingBoundary(SpeakerWidth3TextBox);
                    DisplayingParameters();
                }
            }
            catch
            {
                textBox.BackColor = Color.Salmon;
            }
        }

            /// Присваивает значение подаваемому элементу TextBox
            /// и если оно не корректно,
            /// то присваивает максимально возможное значение параметра
            /// </summary>
            /// <param name="textBox"></param>
            private void DisplayingBoundary(TextBox textBox)
        {
            var information = _elements[textBox];
            var parameters = information[0];
            var element = parameters.element;
            var parameter = parameters.parameter;
            if (_modelElements.IsElement(element))
            {
                if (Double.Parse(textBox.Text) >
                    _modelElements.Element(element).Parameter(parameter).MaxValue)
                {
                    textBox.Text
                        = string.Concat(_modelElements.Element(element).Parameter(parameter).MaxValue);
                }
            }
        }
            /// <summary>
            /// Изменяет значения всех параметров на начальные
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void ReturnInitialValueButton_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(683, 345);
            SpeakerDimensions4GroupBox.Visible = false;
            SpeakerDimensions3GroupBox.Visible = false;
            SpeakerDimensions2GroupBox.Visible = false;
            _modelElements = new ModelElements();
            foreach (var textBox in _elements.Keys)
            {
                var information = _elements[textBox];
                var parameters = information[0];
                var element = parameters.element;
                var parameter = parameters.parameter;
                    textBox.Text =
                        string.Concat(_modelElements.Element(element).Parameter(parameter).Value);
            }
            _modelElements.CalculationMaxWDinamic();
            _modelElements.CalculationMaxHDinamics();
            DisplayingParameters();
        }
    }



    ///////
    //    /// <summary>
    //    /// Словарь хранить названия элементов управления
    //    /// в соответствии 
    //    /// </summary>
    //    private Dictionary<TextBox, ParametersName> _elements = new Dictionary<TextBox, ParametersName>();

    //    /// <summary>
    //    /// Поле хранит параметры модели
    //    /// </summary>
    //    private ModelParameters _modelParameters = new ModelParameters();

    //    /// <summary>
    //    /// Иницилизация формы и создание списка элементов TextBox
    //    /// </summary>
    //    public ModelParametersForm()
    //    {
    //        //Инициализация формы
    //        InitializeComponent();


    //        //Создание списка элементов TextBox
    //        var elements = new List<(TextBox element, ParametersName name)>
    //        {
    //            (CaseHeightTextBox,ParametersName.H),
    //            (CaseLengthTextBox,ParametersName.W),
    //            (CaseWidthTextBox,ParametersName.L),
    //            (RelayDiameterTextBox,ParametersName.D),
    //            (SpeakerWidthTextBox,ParametersName.TS),
    //            (SpeakerLengthTextBox,ParametersName.WS),
    //            (SpeakerHeightTextBox,ParametersName.HS)
    //        };

    //        foreach (var element in elements)
    //        {
    //            _elements.Add(element.element, element.name);
    //        }
    //    }

    //    /// <summary>
    //    /// Метод для обработки результата введен
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void Parameter_TextChanged(object sender, EventArgs e)
    //    {
    //        var textBox = (TextBox)sender;
    //        try //Блок где ожидается ошибка
    //        {
    //            var value = double.Parse(textBox.Text);
    //            //Необходимо при автоматическом вызове метода
    //            textBox.Text = value.ToString();
    //            _modelParameters.Parameter(_elements[textBox]).Value = value;
    //            textBox.BackColor = Color.LightGreen;
    //            //Заменить на элемент перечисления 
    //            if(_elements[textBox] == ParametersName.H || _elements[textBox] == ParametersName.D)
    //            {
    //                _modelParameters.CalculateMaxHeightDinamic();
    //                Displaying(SpeakerHeightTextBox, BoundaryValueHSLabel);
    //                DisplayingBoundary(SpeakerHeightTextBox);
    //            }
    //            //Заменить на элемент перечисления
    //            if (_elements[textBox] == ParametersName.W)
    //            {
    //                _modelParameters.CalculateMaxLenghtDinamic();
    //                Displaying(SpeakerLengthTextBox, BoundaryValueLSLabel);
    //                DisplayingBoundary(SpeakerLengthTextBox);
    //            }
    //        }
    //        catch //Обработчик ошибки
    //        {
    //            textBox.BackColor = Color.Salmon;
    //        }
    //    }

    //    /// <summary>
    //    /// Изменяет некорректное значение введенное пользователем
    //    /// на последнее вводимое корректное значение
    //    /// при потере фокуса элементом TextBox
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void TextBox_Leave(object sender, EventArgs e)
    //    {
    //        var textBox = (TextBox)sender;
    //        if (textBox.BackColor == Color.Salmon)
    //        {
    //            textBox.Text = 
    //                string.Concat(_modelParameters.Parameter(_elements[textBox]).Value);
    //            textBox.BackColor = Color.LightGreen;
    //        }
    //    }

    //    /// <summary>
    //    /// Изменяет значения всех параметров на начальные
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void ReturnInitialValueButton_Click(object sender, EventArgs e)
    //    {
    //        _modelParameters = new ModelParameters();
    //        var elements = new List<TextBox>
    //        {
    //            (SpeakerLengthTextBox),
    //            (SpeakerHeightTextBox),
    //            (CaseHeightTextBox),
    //            (CaseLengthTextBox),
    //            (CaseWidthTextBox),
    //            (RelayDiameterTextBox),
    //            (SpeakerWidthTextBox)
    //        };

    //        foreach (var element in elements)
    //        {
    //            element.Text = 
    //                string.Concat(_modelParameters.Parameter(_elements[element]).Value);
    //        }
    //        _modelParameters.CalculateMaxHeightDinamic();
    //        Displaying(SpeakerHeightTextBox, BoundaryValueHSLabel);
    //        _modelParameters.CalculateMaxLenghtDinamic();
    //        Displaying(SpeakerLengthTextBox, BoundaryValueLSLabel);
    //    }

    //    /// <summary>
    //    /// Отображение граничных значений параметра 
    //    /// в lable, в соответствии параметром
    //    /// </summary>
    //    /// <param name="textBox">Элемент TextBox</param>
    //    /// <param name="label">Элемент Lable</param>
    //    private void Displaying(TextBox textBox, Label label)
    //    {
    //        label.Text = 
    //            "(от " + string.Concat(_modelParameters.Parameter(_elements[textBox]).MinValue) + " до "
    //            + string.Concat(_modelParameters.Parameter(_elements[textBox]).MaxValue) + ") мм";
    //    }

    //    /// <summary>
    //    /// Присваивает значение подаваемому элементу TextBox
    //    /// и если оно не корректно,
    //    /// то присваивает максимально возможное значение параметра
    //    /// </summary>
    //    /// <param name="textBox"></param>
    //    private void DisplayingBoundary(TextBox textBox)
    //    {
    //        Parameter_TextChanged((object)textBox, EventArgs.Empty);
    //        if (textBox.BackColor == Color.Salmon)
    //        {
    //            textBox.Text
    //                = string.Concat(_modelParameters.Parameter(_elements[textBox]).MaxValue);
    //            textBox.BackColor = Color.LightGreen;
    //        }
    //    }

    //    /// <summary>
    //    /// Инициализация нового объекта Manager
    //    /// (построителя модели)
    //    /// при клике на кнопку "Построить"
    //    /// </summary>
    //    /// <param name="sender"></param>
    //    /// <param name="e"></param>
    //    private void BuildModelButton_Click(object sender, EventArgs e)
    //    {
    //        Manager manager = new Manager(_modelParameters);
    //    }

    //    private void BuildModelButton_MouseMove(object sender, MouseEventArgs e)
    //    {
    //        BuildModelButton.Focus();
    //    }

    //    //Сделать массив и хранить там GroupBox
    //    private void AddDinamicButton_Click(object sender, EventArgs e)
    //    {
    //        if(NumberDinamicTextBox.Text != "4")
    //        {
    //            //Прибавить +1 к числу элементов
    //            NumberDinamicTextBox.Text =
    //                 (double.Parse(NumberDinamicTextBox.Text) + 1).ToString();
    //            //Отобразить TextBox
    //            if (NumberDinamicTextBox.Text == "2")
    //            {
    //                _modelParameters.AddParametersCap();
    //                _modelParameters.CalculationMax();
    //                SpeakerDimensions2GroupBox.Visible = true;
    //                BoundaryValueLSLabel.Text =
    //                     "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS).MinValue) + " до "
    //                    + string.Concat(_modelParameters.Parameter(ParametersName.WS).MaxValue) + ") мм";
    //                BoundaryValueHSLabel.Text =
    //                     "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS).MinValue) + " до "
    //                    + string.Concat(_modelParameters.Parameter(ParametersName.HS).MaxValue) + ") мм";
    //                BoundaryValueLS1Label.Text =
    //                     "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MinValue) + " до "
    //                    + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MaxValue) + ") мм";
    //                BoundaryValueHS1Label.Text =
    //                     "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MinValue) + " до "
    //                    + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MaxValue) + ") мм";
    //            }

    //        }
    //            if (NumberDinamicTextBox.Text == "3")
    //            {
    //                _modelParameters.AddParametersCap();
    //                _modelParameters.CalculationMax();
    //                SpeakerDimensions3GroupBox.Visible = true;
    //                BoundaryValueLSLabel.Text =
    //                     "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS).MinValue) + " до "
    //                    + string.Concat(_modelParameters.Parameter(ParametersName.WS).MaxValue) + ") мм";

    //                BoundaryValueHSLabel.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS).MaxValue) + ") мм";

    //                BoundaryValueLS1Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MaxValue) + ") мм";

    //                BoundaryValueHS1Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MaxValue) + ") мм";


    //                BoundaryValueLS2Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS2).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.WS2).MaxValue) + ") мм";

    //                BoundaryValueHS2Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS2).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS2).MaxValue) + ") мм";
    //        }
    //            if (NumberDinamicTextBox.Text == "4")
    //            {
    //                _modelParameters.AddParametersCap();
    //                _modelParameters.CalculationMax();
    //                SpeakerDimensions4GroupBox.Visible = true;

    //                BoundaryValueLSLabel.Text =
    //                    "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS).MinValue) + " до "
    //                   + string.Concat(_modelParameters.Parameter(ParametersName.WS).MaxValue) + ") мм";

    //                BoundaryValueHSLabel.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS).MaxValue) + ") мм";

    //                BoundaryValueLS1Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.WS1).MaxValue) + ") мм";

    //                BoundaryValueHS1Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS1).MaxValue) + ") мм";


    //                BoundaryValueLS2Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS2).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.WS2).MaxValue) + ") мм";

    //                BoundaryValueHS2Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS2).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS2).MaxValue) + ") мм";

    //                BoundaryValueLS3Label.Text =
    //                "(от " + string.Concat(_modelParameters.Parameter(ParametersName.WS3).MinValue) + " до "
    //               + string.Concat(_modelParameters.Parameter(ParametersName.WS3).MaxValue) + ") мм";

    //                BoundaryValueHS3Label.Text =
    //                 "(от " + string.Concat(_modelParameters.Parameter(ParametersName.HS3).MinValue) + " до "
    //                + string.Concat(_modelParameters.Parameter(ParametersName.HS3).MaxValue) + ") мм";

    //        }
    //            //Исправить интервал всех элементов
    //    }
    //}
}

