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

        /// <summary>
        /// Хранить экземпляр построителя модели
        /// </summary>
        private Manager _manager;

        public ModelParametersForm()
        {
            //Инициализация формы
            InitializeComponent();
            ChangeSize(268);
            //Создание списка элементов TextBox
            var elements = new List<(TextBox textBox,ElementName element, ParametersName parameter)>
                  {
                    (SpeakerHeightTextBox,ElementName.SpeakerCover1, ParametersName.Height),
                     (SpeakerWidthTextBox, ElementName.SpeakerCover1, ParametersName.Width),
                     (CaseHeightTextBox, ElementName.Case, ParametersName.Height),
                     (CaseLengthTextBox, ElementName.Case, ParametersName.Width),
                     (CaseWidthTextBox, ElementName.Case, ParametersName.Length),
                     (RelayDiameterTextBox, ElementName.Rele, ParametersName.Diameter),
                     (SpeakerTTextBox, ElementName.SpeakerCover1, ParametersName.Length),
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
        private void DisplayingParameters()
        {
            if (_modelElements.IsElement(ElementName.SpeakerCover1))
            {
                Displaying(SpeakerHeightTextBox, 
                    ParametersName.Height, ElementName.SpeakerCover1);
                Displaying(SpeakerWidthTextBox, 
                    ParametersName.Width, ElementName.SpeakerCover1);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover2))
            {
                Displaying(SpeakerWidth1TextBox, 
                    ParametersName.Width, ElementName.SpeakerCover2);
                Displaying(SpeakerHeight1TextBox, 
                    ParametersName.Height, ElementName.SpeakerCover2);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover3))
            {
                Displaying(SpeakerWidth2TextBox, 
                    ParametersName.Width, ElementName.SpeakerCover3);
                Displaying(SpeakerHeight2TextBox, 
                    ParametersName.Height, ElementName.SpeakerCover3);
            }
            if (_modelElements.IsElement(ElementName.SpeakerCover4))
            {
                Displaying(SpeakerWidth3TextBox, 
                    ParametersName.Width, ElementName.SpeakerCover4);
                Displaying(SpeakerHeight3TextBox, 
                    ParametersName.Height, ElementName.SpeakerCover4);
            }
        }

        /// <summary>
        /// Метод изменяет высоту формы
        /// согласно принятому значению
        /// </summary>
        /// <param name="size">Высота формы</param>
        private void ChangeSize(int size)
        {
            this.Size = new System.Drawing.Size(683, size);
            this.MaximumSize = new System.Drawing.Size(683, size);
            this.MinimumSize = new System.Drawing.Size(683, size);
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
        private void Displaying(TextBox textBox, ParametersName name, ElementName elementName)
        {
            toolTip1.SetToolTip(textBox, "(от " + string.Concat(_modelElements.Element(elementName).
                Parameter(name).MinValue) + " до "
                + string.Concat(_modelElements.Element(elementName).
                Parameter(name).MaxValue) + ") мм");
        }

        /// <summary>
        /// Метод добавляет динамик
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void AddDynamicButton_Click(object sender, EventArgs e)
        {
            if (NumberDinamicTextBox.Text != "4")
            {
                NumberDinamicTextBox.Text =
                       (double.Parse(NumberDinamicTextBox.Text) + 1).ToString();
                _modelElements.AddDynamic();
                if (NumberDinamicTextBox.Text == "2")
                {
                    ChangeSize(297);
                    tableLayoutPanel2.Visible = true;
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    ChangeSize(325);
                    tableLayoutPanel3.Visible = true;
                }
                if (NumberDinamicTextBox.Text == "4")
                {
                    ChangeSize(354);
                    tableLayoutPanel4.Visible = true;
                }
                DisplayingParameters();
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
        private void DeleteDynamicButton_Click(object sender, EventArgs e)
        {
            if(NumberDinamicTextBox.Text != "1")
            {               
                if (NumberDinamicTextBox.Text == "4")
                {
                    ChangeSize(325);
                    tableLayoutPanel4.Visible = false;
                    if (SpeakerWidth3TextBox.Visible == false)
                    {
                        ChangeForm(Form3ComboBox, EventArgs.Empty);
                    }
                }
                if (NumberDinamicTextBox.Text == "3")
                {
                    ChangeSize(297);
                    tableLayoutPanel3.Visible = false;
                    if (SpeakerWidth2TextBox.Visible == false)
                    {
                        ChangeForm(Form2ComboBox, EventArgs.Empty);
                    }
                }
                if (NumberDinamicTextBox.Text == "2")
                {
                    ChangeSize(268);
                    tableLayoutPanel2.Visible = false;
                    if (SpeakerWidth1TextBox.Visible == false)
                    {
                        ChangeForm(Form1ComboBox, EventArgs.Empty);
                    }
                }
                _modelElements.DeleteDynamic();
                DisplayingParameters();
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
                    _modelElements.CalculationMaxHeightDynamics();
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
            if(SpeakerWidthTextBox.Visible == false)
            {
                ChangeForm(FormComboBox, EventArgs.Empty);
            }
            DeleteDynamicButton_Click(DeleteParametersButton, EventArgs.Empty);
            DeleteDynamicButton_Click(DeleteParametersButton, EventArgs.Empty);
            DeleteDynamicButton_Click(DeleteParametersButton, EventArgs.Empty);
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
                    if (SpeakerWidthTextBox.Visible == true)
                    {
                        SpeakerWidthTextBox.Visible = false;
                    }
                    else
                    {
                        FormComboBox.Text = "Прямоугольник";
                        SpeakerWidthTextBox.Visible = true;
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
                    if (SpeakerWidth1TextBox.Visible == true)
                    {
                        SpeakerWidth1TextBox.Visible = false;
                    }
                    else
                    {
                        Form1ComboBox.Text = "Прямоугольник";
                        SpeakerWidth1TextBox.Visible = true;
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
                    if (SpeakerWidth2TextBox.Visible == true)
                    {
                        SpeakerWidth2TextBox.Visible = false;
                    }
                    else
                    {
                        Form2ComboBox.Text = "Прямоугольник"; 
                        SpeakerWidth2TextBox.Visible = true;
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
                    if (SpeakerWidth3TextBox.Visible == true)
                    {
                        SpeakerWidth3TextBox.Visible = false;
                    }
                    else
                    {
                        Form3ComboBox.Text = "Прямоугольник";
                        SpeakerWidth3TextBox.Visible = true;
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
            _manager = new Manager(_modelElements);
        }

        /// <summary>
        /// Кнопка "Построить" получает фокус 
        /// при наведении наведении курсора
        /// </summary>
        /// <param name="sender">Действующий объект</param>
        /// <param name="e">Действие</param>
        private void BuildModelButton_MouseMove(object sender, MouseEventArgs e)
        {
            BuildModelButton.Focus();
        }
    }
}

