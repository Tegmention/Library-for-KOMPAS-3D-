using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Plugin_KOMPAS_3D.UI
{   /// <summary>
    /// 
    /// </summary>
    public partial class ModelParametersForm : Form
    {
        private Dictionary<TextBox, string> _elements = new Dictionary<TextBox, string>();
        /// <summary>
        /// Иницилизация формы
        /// </summary>
        public ModelParametersForm()
        {
            InitializeComponent();
            _elements.Add(SpeakerHeightTextBox,"SH");
        }

        private void AllTextBoxesIn(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    c.Text = "";
                if (c.GetType() == typeof(GroupBox))
                    AllTextBoxesIn(c);
            }
        }
        ///Ловить ошибки можно через try или catch
        /// <summary>
        /// Очистка всех параметров модели
        /// введенных пользователем 
        /// блокировка ввода параметров высоты и длинны динамика
        /// отображение необходиости ввода параметров H,D и L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteParametersButton_Click(object sender, EventArgs e)
        {
            CleanAllTextBoxesIn(this);
            BoundaryValueHSLabel.Text = "Введите параметры : H, D";
            BoundaryValueLSLabel.Text = "Введите параметр : L";
            SpeakerHeightTextBox.Enabled = false;
            SpeakerLengthTextBox.Enabled = false;
        }

        /// <summary>
        /// Обход элементов формы с присвоением 
        /// TextBox.Text = "" и рекурсивным заходом в GroupBox
        /// </summary>
        /// <param name="parent"></param>
        private void CleanAllTextBoxesIn(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                    c.Text = "";
                if (c.GetType() == typeof(GroupBox))
                    CleanAllTextBoxesIn(c);
            }
        }

        /// <summary>
        /// Вызов функции отображения 
        /// граничных параметров высоты динамика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CaseHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayBoundaryValuesHS();
        }

        /// <summary>
        /// Отображение диапозона параметров длинны динамика 
        /// и разблокировка возможности ввода длинны динамика
        /// если введена длинна корпуса
        /// иначе блокировка ввода длинны динамика 
        /// и отображение необходимости ввода параметра L
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CaseLengthTextBox_TextChanged(object sender, EventArgs e)
        {
            double L;
            if (double.TryParse(CaseLengthTextBox.Text, out L))
            {
                if (L > 0)
                {
                    BoundaryValueLSLabel.Text = "(от 150 до) мм";
                    SpeakerLengthTextBox.Enabled = true;
                }
            }
            //Если значения L не являются числом
            else
            {
                SpeakerLengthTextBox.Enabled = false;
                SpeakerLengthTextBox.Text = "";
                BoundaryValueLSLabel.Text = "Введите параметр : L";
            }
        }

        /// <summary>
        /// Вызов функции отображения 
        /// граничных параметров высоты динамика
        /// при изменении параметра диаметра реле регулировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelayDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            //DisplayBoundaryValuesHS();
            _elements.Add((TextBox)sender, "D");
        }

        /// <summary>
        /// Отображение диапозона параметров высоты динамика
        /// если введены высота корпуса и диаметр реле регулировки
        /// иначе блокировка ввода высоты динамика 
        /// и отображение необходимости ввода параметров H и D
        /// </summary>
        private void DisplayBoundaryValuesHS()
        {
            double H;
            double D;
                if (double.TryParse(CaseHeightTextBox.Text,out H) && double.TryParse(RelayDiameterTextBox.Text, out D))
                {
                    if (H>0 && D>0)
                    {
                        BoundaryValueHSLabel.Text = "(от 60 до ) мм";
                        SpeakerHeightTextBox.Enabled = true;
                    }
                }
                //Если значение H или D не является числом
                else
                {
                    SpeakerHeightTextBox.Enabled = false;
                    SpeakerHeightTextBox.Text = "";
                    BoundaryValueHSLabel.Text = "Введите параметры : H, D";
                }
        }

        private void ModelParametersForm_Load(object sender, EventArgs e)
        {

        }

        private void SpeakerHeightTextBox_Leave(object sender, EventArgs e)
        {
            var textbox = (TextBox)sender;
            SpeakerHeightTextBox.Text = textbox.Name;
        }
    }
}
