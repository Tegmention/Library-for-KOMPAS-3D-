using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Parameters;
using System.Drawing;
using Builder;


namespace Plugin_KOMPAS_3D.UI
{   /// <summary>
    /// Форма для ввода пользователем параметров модели
    /// </summary>
    public partial class ModelParametersForm : Form
    {
        /// <summary>
        /// Словарь хранить названия элементов управления
        /// в соответствии 
        /// </summary>
        private Dictionary<TextBox, string> _elements = new Dictionary<TextBox, string>();

        /// <summary>
        /// Поле хранит параметры модели
        /// </summary>
        private ModelParameters _modelParameters = new ModelParameters();

        /// <summary>
        /// Иницилизация формы и создание списка элементов TextBox
        /// </summary>
        public ModelParametersForm()
        {
            //Инициализация формы
            InitializeComponent();

            //Создание списка элементов TextBox
            var elements = new List<(TextBox element, string name)>
            {
                (CaseHeightTextBox,ParametersName.H.ToString()),
                (CaseLengthTextBox,ParametersName.L.ToString()),
                (CaseWidthTextBox,ParametersName.W.ToString()),
                (RelayDiameterTextBox,ParametersName.D.ToString()),
                (SpeakerWidthTextBox,ParametersName.WS.ToString()),
                (SpeakerLengthTextBox,ParametersName.LS.ToString()),
                (SpeakerHeightTextBox,ParametersName.HS.ToString())
            };

            foreach (var element in elements)
            {
                _elements.Add(element.element, element.name);
            }
        }

        /// <summary>
        /// Метод для обработки результата введен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Parameter_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            try //Блок где ожидается ошибка
            {
            var value = double.Parse(textBox.Text);
                //Необходимо при автоматическом вызове метода
                textBox.Text = value.ToString();
            _modelParameters.Parameters[_elements[textBox]].Value = value;
                textBox.BackColor = Color.LightGreen;
                //Заменить на элемент перечисления 
                if(_elements[textBox] == "H" || _elements[textBox] == "D")
                {
                    _modelParameters.CalculateMaxHeightDinamic();
                    Displaying(SpeakerHeightTextBox, BoundaryValueHSLabel);
                    DisplayingBoundary(SpeakerHeightTextBox);
                }
                //Заменить на элемент перечисления
                if (_elements[textBox] == "L")
                {
                    _modelParameters.CalculateMaxLenghtDinamic();
                    Displaying(SpeakerLengthTextBox, BoundaryValueLSLabel);
                    DisplayingBoundary(SpeakerLengthTextBox);
                }
            }
            catch //Обработчик ошибки
            {
                textBox.BackColor = Color.Salmon;
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
                textBox.Text = 
                    string.Concat(_modelParameters.Parameters[_elements[textBox]].Value);
                textBox.BackColor = Color.LightGreen;
            }
        }

        /// <summary>
        /// Изменяет значения всех параметров на начальные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReturnInitialValueButton_Click(object sender, EventArgs e)
        {
            _modelParameters = new ModelParameters();
            var elements = new List<TextBox>
            {
                (SpeakerLengthTextBox),
                (SpeakerHeightTextBox),
                (CaseHeightTextBox),
                (CaseLengthTextBox),
                (CaseWidthTextBox),
                (RelayDiameterTextBox),
                (SpeakerWidthTextBox)
            };

            foreach (var element in elements)
            {
                element.Text = 
                    string.Concat(_modelParameters.Parameters[_elements[element]].Value);
            }
            _modelParameters.CalculateMaxHeightDinamic();
            Displaying(SpeakerHeightTextBox, BoundaryValueHSLabel);
            _modelParameters.CalculateMaxLenghtDinamic();
            Displaying(SpeakerLengthTextBox, BoundaryValueLSLabel);
        }

        /// <summary>
        /// Отображение граничных значений параметра 
        /// в lable, в соответствии параметром
        /// </summary>
        /// <param name="textBox">Элемент TextBox</param>
        /// <param name="label">Элемент Lable</param>
        private void Displaying(TextBox textBox, Label label)
        {
            label.Text = 
                "(от " + string.Concat(_modelParameters.Parameters[_elements[textBox]].MinValue) + " до "
                + string.Concat(_modelParameters.Parameters[_elements[textBox]].MaxValue) + ") мм";
        }

        /// <summary>
        /// Присваивает значение подаваемому элементу TextBox
        /// и если оно не корректно,
        /// то присваивает максимально возможное значение параметра
        /// </summary>
        /// <param name="textBox"></param>
        private void DisplayingBoundary(TextBox textBox)
        {
            Parameter_TextChanged((object)textBox, EventArgs.Empty);
            if (textBox.BackColor == Color.Salmon)
            {
                textBox.Text
                    = string.Concat(_modelParameters.Parameters[_elements[textBox]].MaxValue);
                textBox.BackColor = Color.LightGreen;
            }
        }

        /// <summary>
        /// Инициализация нового объекта Manager
        /// (построителя модели)
        /// при клике на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BuildModelButton_Click(object sender, EventArgs e)
        {
            Manager manager = new Manager(_modelParameters);
        }

        private void BuildModelButton_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void ModelParametersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
