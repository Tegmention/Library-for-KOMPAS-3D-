using System;
using System.Windows.Forms;

namespace Plugin_KOMPAS_3D.UI
{   /// <summary>
    /// 
    /// </summary>
    public partial class ModelParametersForm : Form
    {
        /// <summary>
        /// Иницилизация формы
        /// </summary>
        public ModelParametersForm()
        {
            InitializeComponent();
        }

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
                    BoundaryValueLSLabel.Text = "(от 150 до " + CalculationMaximumValuesLS(L) + ") мм";
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
            DisplayBoundaryValuesHS();
        }

        /// <summary>
        /// Расчет максимального значения 
        /// высоты динамика в зависимости 
        /// от значений высоты корпуса и диаметра реле
        /// регулировки
        /// !!!Не хранить параметры в форме!!!
        /// </summary>
        /// <param name="H">Высота корпуса</param>
        /// <param name="D">Диаметр реле регулировки</param>
        /// <returns></returns>
        private double CalculationMaximumValuesHS(double H, double D)
        {
            double result = H - 5 - (D + 10);
            if (result >575)
            {
                result = 575;
            }
            if (result < 65)
            {
                result = 65;
            }
            return result;
        }

        /// <summary>
        /// Расчет максимального значения 
        /// длинны динамика в зависимости от 
        /// длинны корпуса
        /// !!!Не хранить параметры в форме!!!
        /// </summary>
        /// <param name="L">Длинна корпуса</param>
        /// <returns></returns>
        private double CalculationMaximumValuesLS(double L)
        {
            double result = L - 5;
            if (result > 295)
            {
                result = 295;
            }
            if (result < 195)
            {
                result = 195;
            }
            return result;
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
                        BoundaryValueHSLabel.Text = "(от 60 до "+ CalculationMaximumValuesHS(H,D)+") мм";
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
    }
}
