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
        private Dictionary<TextBox, 
            List<(ElementName element, ParametersName parameter)>> _elements =
            new Dictionary<TextBox, List<(ElementName element, ParametersName parameter)>>();
        
        public ModelParametersForm()
        {
            //Инициализация формы
            InitializeComponent();
            this.Size = new System.Drawing.Size(683, 345);
            this.MaximumSize = new System.Drawing.Size(683, 345);
            //Создание списка элементов TextBox
            var elements = new List<(TextBox textBox,ElementName element, ParametersName parameter)>
                  {
                     (CaseHeightTextBox, ElementName.Case, ParametersName.Height),
                     (CaseLengthTextBox, ElementName.Case, ParametersName.Width),
                     (CaseWidthTextBox, ElementName.Case, ParametersName.Length),
                     (RelayDiameterTextBox, ElementName.Rele, ParametersName.Diameter),
                     (SpeakerTTextBox, ElementName.SpeakerCover1, ParametersName.Length),
                     (SpeakerWidthTextBox, ElementName.SpeakerCover1, ParametersName.Width),
                     (SpeakerHeightTextBox,ElementName.SpeakerCover1, ParametersName.Height),
                     (SpeakerT1TextBox, ElementName.SpeakerCover2, ParametersName.Length),
                     (SpeakerWidth1TextBox, ElementName.SpeakerCover2, ParametersName.Width),
                     (SpeakerHeight1TextBox,ElementName.SpeakerCover2, ParametersName.Height),
                     (SpeakerT2TextBox, ElementName.SpeakerCover3, ParametersName.Length),
                     (SpeakerWidth2TextBox, ElementName.SpeakerCover3, ParametersName.Width),
                     (SpeakerHeight2TextBox,ElementName.SpeakerCover3, ParametersName.Height),
                     (SpeakerT3TextBox, ElementName.SpeakerCover4, ParametersName.Length),
                     (SpeakerWidth3TextBox, ElementName.SpeakerCover4, ParametersName.Width),
                     (SpeakerHeight3TextBox,ElementName.SpeakerCover4, ParametersName.Height),
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
        /// Метод отображает 
        /// граничные значения 
        /// существующих параметров
        /// </summary>
        public void DisplayingParameters()
        {
            if (_modelElements.IsElement(ElementName.SpeakerCover1))
            {
                Displaying(BoundaryValueHSLabel, 
                    ParametersName.Height, ElementName.SpeakerCover1);
                Displaying(BoundaryValueWSLabel, 
                    ParametersName.Width, ElementName.SpeakerCover1);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover2))
            {
                Displaying(BoundaryValueWS1Label, 
                    ParametersName.Width, ElementName.SpeakerCover2);
                Displaying(BoundaryValueHS1Label, 
                    ParametersName.Height, ElementName.SpeakerCover2);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover3))
            {
                Displaying(BoundaryValueWS2Label, 
                    ParametersName.Width, ElementName.SpeakerCover3);
                Displaying(BoundaryValueHS2Label, 
                    ParametersName.Height, ElementName.SpeakerCover3);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover4))
            {
                Displaying(BoundaryValueWS3Label, 
                    ParametersName.Width, ElementName.SpeakerCover4);
                Displaying(BoundaryValueHS3Label, 
                    ParametersName.Height, ElementName.SpeakerCover4);
            }
        }

        /// <summary>
        /// Метод отображает значения 
        /// граничных параметров параметра
        /// </summary>
        /// <param name="label">
        /// Элемент отображения
        /// диапозона возможных значений
        /// </param>
        /// <param name="name">Название параметра</param>
        /// <param name="elementName">Название элемента</param>
        public void Displaying(Label label, ParametersName name, ElementName elementName)
        {
                label.Text = "(от " + string.Concat(_modelElements.Element(elementName).
                    Parameter(name).MinValue) + " до "
                    + string.Concat(_modelElements.Element(elementName).
                    Parameter(name).MaxValue) + ") мм";
        }

        /// <summary>
        /// Метод добавляет динамик
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void AddDinamicButton_Click(object sender, EventArgs e)
        {
            if (NumberDinamicTextBox.Text != "4")
            {
                NumberDinamicTextBox.Text =
                       (double.Parse(NumberDinamicTextBox.Text) + 1).ToString();
                _modelElements.AddDynamic();
                if (NumberDinamicTextBox.Text == "2")
                {
                    SpeakerDimensions2GroupBox.Visible = true;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    this.MaximumSize = new System.Drawing.Size(683, 488);
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

        /// <summary>
        /// Метод удаляет динамик 
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void DeleteDinamicButton_Click(object sender, EventArgs e)
        {
            if(NumberDinamicTextBox.Text != "1")
            {               
                if (NumberDinamicTextBox.Text == "4")
                {
                    if (SpeakerLength3Label.Visible != true)
                    {
                        ChangeForm(Form3ComboBox, EventArgs.Empty);
                    }
                    _modelElements.DeleteDynamic();
                    SpeakerDimensions4GroupBox.Visible = false;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    if (SpeakerLength2Label.Visible != true)
                    {
                        ChangeForm(Form2ComboBox, EventArgs.Empty);
                    }
                    
                    _modelElements.DeleteDynamic();
                    this.MaximumSize = new System.Drawing.Size(683, 345);
                    this.Size = new System.Drawing.Size(683, 345);
                    SpeakerDimensions3GroupBox.Visible = false;
                    DisplayingParameters();
                }
                if (NumberDinamicTextBox.Text == "2")
                {
                    if (SpeakerLength1Label.Visible != true)
                    {
                        ChangeForm(Form1ComboBox, EventArgs.Empty);
                    }
                    _modelElements.DeleteDynamic();
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
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
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

        /// <summary>
        /// Присваивает параметру значение 
        /// из соответствующего элемента TextBox
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
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
                if (_modelElements.IsElement(element))
                {
                    _modelElements.Element(element).Parameter(parameter).Value = value;
                }
                textBox.BackColor = Color.LightGreen;
                if (parameter == ParametersName.Height || parameter == ParametersName.Diameter)
                {
                    _modelElements.CalculationMaxHDynamics();
                    _modelElements.CalculationMaxWidthDynamic();
                    DisplayingBoundary(SpeakerHeightTextBox);
                    DisplayingBoundary(SpeakerHeight1TextBox);
                    DisplayingBoundary(SpeakerHeight2TextBox);
                    DisplayingBoundary(SpeakerHeight3TextBox);
                    DisplayingParameters();
                }
                if (parameter == ParametersName.Width)
                {
                    _modelElements.CalculationMaxWidthDynamic();
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
        /// <param name="textBox">Действие</param>
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
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void ReturnInitialValueButton_Click(object sender, EventArgs e)
        {
            if(SpeakerLengthLabel.Visible != true)
            {
                ChangeForm(FormComboBox, EventArgs.Empty);
            }
            if (SpeakerLength1Label.Visible != true)
            {
                ChangeForm(Form1ComboBox, EventArgs.Empty);
            }
            if (SpeakerLength2Label.Visible != true)
            {
                ChangeForm(Form2ComboBox, EventArgs.Empty);
            }
            if (SpeakerLength3Label.Visible != true)
            {
                ChangeForm(Form3ComboBox, EventArgs.Empty);
            }
            DeleteDinamicButton_Click(DeleteParametersButton, EventArgs.Empty);
            DeleteDinamicButton_Click(DeleteParametersButton, EventArgs.Empty);
            DeleteDinamicButton_Click(DeleteParametersButton, EventArgs.Empty);
            _modelElements = new ModelElements();
            foreach (var textBox in _elements.Keys)
            {
                var information = _elements[textBox];
                var parameters = information[0];
                var element = parameters.element;
                if (element == ElementName.SpeakerCover2 
                    || element == ElementName.SpeakerCover3
                    || element == ElementName.SpeakerCover4)
                {
                    element = ElementName.SpeakerCover1;
                }
                var parameter = parameters.parameter;
                textBox.BackColor = Color.LightGreen;
                textBox.Text =
                    string.Concat(_modelElements.Element(element).Parameter(parameter).Value);
            }
        }

        /// <summary>
        /// Отображает необходимые 
        /// параметры при изменении 
        /// формы
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void ChangeForm(object sender, EventArgs e)
        {
            if(sender == FormComboBox)
            {
                if (_modelElements.IsElement(ElementName.SpeakerCover1))
                {
                    _modelElements.ChangeForm(ElementName.SpeakerCover1);
                    if (SpeakerLengthLabel.Visible == true)
                    {
                        SpeakerHeightLabel.Text = "Диаметр (D:)";
                        SpeakerLengthLabel.Visible = false;
                        SpeakerWidthTextBox.Visible = false;
                        BoundaryValueWSLabel.Visible = false;
                    }
                    else
                    {
                        FormComboBox.Text = "Прямоугольник";
                        SpeakerHeightLabel.Text = "Высота (HS):";
                        SpeakerLengthLabel.Visible = true;
                        SpeakerWidthTextBox.Visible = true;
                        BoundaryValueWSLabel.Visible = true;
                    }
                    DisplayingBoundary(SpeakerHeightTextBox);
                    DisplayingParameters();
                }
            }
            if (sender == Form1ComboBox)
            {
                if (_modelElements.IsElement(ElementName.SpeakerCover2))
                {
                    _modelElements.ChangeForm(ElementName.SpeakerCover2);
                    if (SpeakerLength1Label.Visible == true)
                    {
                        SpeakerHeight1Label.Text = "Диаметр (D:)";
                        SpeakerLength1Label.Visible = false;
                        SpeakerWidth1TextBox.Visible = false;
                        BoundaryValueWS1Label.Visible = false;
                    }
                    else
                    {
                        Form1ComboBox.Text = "Прямоугольник";
                        SpeakerHeight1Label.Text = "Высота (HS):";
                        SpeakerLength1Label.Visible = true;
                        SpeakerWidth1TextBox.Visible = true;
                        BoundaryValueWS1Label.Visible = true;
                    }
                    DisplayingBoundary(SpeakerHeight1TextBox);
                    DisplayingParameters();
                }
            }
            if (sender == Form2ComboBox)
            {
                if (_modelElements.IsElement(ElementName.SpeakerCover3))
                {
                    _modelElements.ChangeForm(ElementName.SpeakerCover3);
                    if (SpeakerLength2Label.Visible == true)
                    {
                        SpeakerHeight2Label.Text = "Диаметр (D:)";
                        SpeakerLength2Label.Visible = false;
                        SpeakerWidth2TextBox.Visible = false;
                        BoundaryValueWS2Label.Visible = false;
                    }
                    else
                    {
                        Form2ComboBox.Text = "Прямоугольник";
                        SpeakerHeight2Label.Text = "Высота (HS):";
                        SpeakerLength2Label.Visible = true;
                        SpeakerWidth2TextBox.Visible = true;
                        BoundaryValueWS2Label.Visible = true;
                    }
                    DisplayingBoundary(SpeakerHeight2TextBox);
                    DisplayingParameters();
                }
            }
            if (sender == Form3ComboBox)
            {
                if (_modelElements.IsElement(ElementName.SpeakerCover4))
                {
                    _modelElements.ChangeForm(ElementName.SpeakerCover4);
                    if (SpeakerLength3Label.Visible == true)
                    {
                        SpeakerHeight3Label.Text = "Диаметр (D:)";
                        SpeakerLength3Label.Visible = false;
                        SpeakerWidth3TextBox.Visible = false;
                        BoundaryValueWS3Label.Visible = false;
                    }
                    else
                    {
                        Form3ComboBox.Text = "Прямоугольник";
                        SpeakerHeight3Label.Text = "Высота (HS):";
                        SpeakerLength3Label.Visible = true;
                        SpeakerWidth3TextBox.Visible = true;
                        BoundaryValueWS3Label.Visible = true;
                    }
                    DisplayingBoundary(SpeakerHeight3TextBox);
                    DisplayingParameters();
                }
            }
        }

        /// <summary>
        /// Инициализация нового объекта Manager
        /// (построителя модели)
        /// при клике на кнопку "Построить"
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void BuildModelButton_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager(_modelElements);
        }

        /// <summary>
        /// Кнопка "Построить" получает фокус 
        /// принаведении наведении курсора
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void BuildModelButton_MouseMove(object sender, MouseEventArgs e)
        {
            BuildModelButton.Focus();
        }
    }
}

