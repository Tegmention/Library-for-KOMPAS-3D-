using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin_KOMPAS_3D.UI
{
    public partial class ModelParametersForm : Form
    {
        public ModelParametersForm()
        {
            InitializeComponent();
        }

        private void DeleteParametersButton_Click(object sender, EventArgs e)
        {
            CleanAllTextBoxesIn(this);
            BoundaryValueHSLabel.Text = "Введите параметры : H, D";
            BoundaryValueLSLabel.Text = "Введите параметр : L";
        }

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
    }
}
