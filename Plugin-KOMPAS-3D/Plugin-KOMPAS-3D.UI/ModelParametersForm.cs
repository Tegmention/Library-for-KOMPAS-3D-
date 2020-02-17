using System;
using System.Windows.Forms;

namespace Plugin_KOMPAS_3D.UI
{
    public partial class ModelParametersForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public ModelParametersForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
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
        /// 
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

        private void CaseHeightTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayBoundaryValuesHS();
        }
        /// <summary>
        /// 
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
            else
            {
                SpeakerLengthTextBox.Enabled = false;
                SpeakerLengthTextBox.Text = "";
                BoundaryValueLSLabel.Text = "Введите параметр : L";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RelayDiameterTextBox_TextChanged(object sender, EventArgs e)
        {
            DisplayBoundaryValuesHS();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="H"></param>
        /// <param name="D"></param>
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
        /// 
        /// </summary>
        /// <param name="L"></param>
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
        /// 
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
                else
                {
                SpeakerHeightTextBox.Enabled = false;
                SpeakerHeightTextBox.Text = "";
                BoundaryValueHSLabel.Text = "Введите параметры : H, D";
                }
        }
    }
}
