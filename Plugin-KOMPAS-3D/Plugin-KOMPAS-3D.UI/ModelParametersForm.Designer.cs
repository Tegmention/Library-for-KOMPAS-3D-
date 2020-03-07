﻿namespace Plugin_KOMPAS_3D.UI
{
    partial class ModelParametersForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.CaseLengthTextBox = new System.Windows.Forms.TextBox();
            this.CaseHeightTextBox = new System.Windows.Forms.TextBox();
            this.CaseWidthTextBox = new System.Windows.Forms.TextBox();
            this.SpeakerHeightTextBox = new System.Windows.Forms.TextBox();
            this.SpeakerLengthTextBox = new System.Windows.Forms.TextBox();
            this.SpeakerWidthTextBox = new System.Windows.Forms.TextBox();
            this.RelayDiameterTextBox = new System.Windows.Forms.TextBox();
            this.CaseHeightlLabel = new System.Windows.Forms.Label();
            this.CaseLengthLabel = new System.Windows.Forms.Label();
            this.CaseWidthLabel = new System.Windows.Forms.Label();
            this.SpeakerHeightLabel = new System.Windows.Forms.Label();
            this.SpeakerLengthLabel = new System.Windows.Forms.Label();
            this.SpeakerWidthLabel = new System.Windows.Forms.Label();
            this.RelayDiameterLabel = new System.Windows.Forms.Label();
            this.СaseDimensionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BoundaryValueWLabel = new System.Windows.Forms.Label();
            this.BoundaryValueLLabel = new System.Windows.Forms.Label();
            this.SpeakerDimensionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BoundaryValueLSLabel = new System.Windows.Forms.Label();
            this.BoundaryValueWSLabel = new System.Windows.Forms.Label();
            this.BoundaryValueHSLabel = new System.Windows.Forms.Label();
            this.ReleDimensionsGroupBox = new System.Windows.Forms.GroupBox();
            this.BoundaryValueDLabel = new System.Windows.Forms.Label();
            this.BuildModelButton = new System.Windows.Forms.Button();
            this.BoundaryValueHLabel = new System.Windows.Forms.Label();
            this.DeleteParametersButton = new System.Windows.Forms.Button();
            this.СaseDimensionsGroupBox.SuspendLayout();
            this.SpeakerDimensionsGroupBox.SuspendLayout();
            this.ReleDimensionsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CaseLengthTextBox
            // 
            this.CaseLengthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CaseLengthTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.CaseLengthTextBox.Location = new System.Drawing.Point(78, 53);
            this.CaseLengthTextBox.Name = "CaseLengthTextBox";
            this.CaseLengthTextBox.Size = new System.Drawing.Size(93, 20);
            this.CaseLengthTextBox.TabIndex = 1;
            this.CaseLengthTextBox.Text = "200";
            this.CaseLengthTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.CaseLengthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // CaseHeightTextBox
            // 
            this.CaseHeightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CaseHeightTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.CaseHeightTextBox.Location = new System.Drawing.Point(78, 27);
            this.CaseHeightTextBox.Name = "CaseHeightTextBox";
            this.CaseHeightTextBox.Size = new System.Drawing.Size(93, 20);
            this.CaseHeightTextBox.TabIndex = 0;
            this.CaseHeightTextBox.Text = "100";
            this.CaseHeightTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.CaseHeightTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // CaseWidthTextBox
            // 
            this.CaseWidthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CaseWidthTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.CaseWidthTextBox.Location = new System.Drawing.Point(78, 79);
            this.CaseWidthTextBox.Name = "CaseWidthTextBox";
            this.CaseWidthTextBox.Size = new System.Drawing.Size(93, 20);
            this.CaseWidthTextBox.TabIndex = 2;
            this.CaseWidthTextBox.Text = "150";
            this.CaseWidthTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.CaseWidthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // SpeakerHeightTextBox
            // 
            this.SpeakerHeightTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeakerHeightTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.SpeakerHeightTextBox.Location = new System.Drawing.Point(78, 79);
            this.SpeakerHeightTextBox.Name = "SpeakerHeightTextBox";
            this.SpeakerHeightTextBox.Size = new System.Drawing.Size(93, 20);
            this.SpeakerHeightTextBox.TabIndex = 6;
            this.SpeakerHeightTextBox.Text = "60";
            this.SpeakerHeightTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.SpeakerHeightTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // SpeakerLengthTextBox
            // 
            this.SpeakerLengthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeakerLengthTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.SpeakerLengthTextBox.Location = new System.Drawing.Point(78, 53);
            this.SpeakerLengthTextBox.Name = "SpeakerLengthTextBox";
            this.SpeakerLengthTextBox.Size = new System.Drawing.Size(93, 20);
            this.SpeakerLengthTextBox.TabIndex = 5;
            this.SpeakerLengthTextBox.Text = "150";
            this.SpeakerLengthTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.SpeakerLengthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // SpeakerWidthTextBox
            // 
            this.SpeakerWidthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeakerWidthTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.SpeakerWidthTextBox.Location = new System.Drawing.Point(78, 27);
            this.SpeakerWidthTextBox.Name = "SpeakerWidthTextBox";
            this.SpeakerWidthTextBox.Size = new System.Drawing.Size(93, 20);
            this.SpeakerWidthTextBox.TabIndex = 4;
            this.SpeakerWidthTextBox.Text = "5";
            this.SpeakerWidthTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.SpeakerWidthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // RelayDiameterTextBox
            // 
            this.RelayDiameterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.RelayDiameterTextBox.BackColor = System.Drawing.Color.LightGreen;
            this.RelayDiameterTextBox.Location = new System.Drawing.Point(78, 27);
            this.RelayDiameterTextBox.Name = "RelayDiameterTextBox";
            this.RelayDiameterTextBox.Size = new System.Drawing.Size(93, 20);
            this.RelayDiameterTextBox.TabIndex = 3;
            this.RelayDiameterTextBox.Text = "10";
            this.RelayDiameterTextBox.TextChanged += new System.EventHandler(this.Parameter_TextChanged);
            this.RelayDiameterTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // CaseHeightlLabel
            // 
            this.CaseHeightlLabel.AutoSize = true;
            this.CaseHeightlLabel.Location = new System.Drawing.Point(3, 30);
            this.CaseHeightlLabel.Name = "CaseHeightlLabel";
            this.CaseHeightlLabel.Size = new System.Drawing.Size(65, 13);
            this.CaseHeightlLabel.TabIndex = 7;
            this.CaseHeightlLabel.Text = "Высота (H):";
            // 
            // CaseLengthLabel
            // 
            this.CaseLengthLabel.AutoSize = true;
            this.CaseLengthLabel.Location = new System.Drawing.Point(3, 56);
            this.CaseLengthLabel.Name = "CaseLengthLabel";
            this.CaseLengthLabel.Size = new System.Drawing.Size(64, 13);
            this.CaseLengthLabel.TabIndex = 8;
            this.CaseLengthLabel.Text = "Длинна (L):";
            // 
            // CaseWidthLabel
            // 
            this.CaseWidthLabel.AutoSize = true;
            this.CaseWidthLabel.Location = new System.Drawing.Point(3, 82);
            this.CaseWidthLabel.Name = "CaseWidthLabel";
            this.CaseWidthLabel.Size = new System.Drawing.Size(69, 13);
            this.CaseWidthLabel.TabIndex = 9;
            this.CaseWidthLabel.Text = "Ширина (W):";
            // 
            // SpeakerHeightLabel
            // 
            this.SpeakerHeightLabel.AutoSize = true;
            this.SpeakerHeightLabel.Location = new System.Drawing.Point(3, 82);
            this.SpeakerHeightLabel.Name = "SpeakerHeightLabel";
            this.SpeakerHeightLabel.Size = new System.Drawing.Size(72, 13);
            this.SpeakerHeightLabel.TabIndex = 10;
            this.SpeakerHeightLabel.Text = "Высота (HS):";
            // 
            // SpeakerLengthLabel
            // 
            this.SpeakerLengthLabel.AutoSize = true;
            this.SpeakerLengthLabel.Location = new System.Drawing.Point(3, 56);
            this.SpeakerLengthLabel.Name = "SpeakerLengthLabel";
            this.SpeakerLengthLabel.Size = new System.Drawing.Size(71, 13);
            this.SpeakerLengthLabel.TabIndex = 11;
            this.SpeakerLengthLabel.Text = "Длинна (LS):";
            // 
            // SpeakerWidthLabel
            // 
            this.SpeakerWidthLabel.AutoSize = true;
            this.SpeakerWidthLabel.Location = new System.Drawing.Point(3, 30);
            this.SpeakerWidthLabel.Name = "SpeakerWidthLabel";
            this.SpeakerWidthLabel.Size = new System.Drawing.Size(76, 13);
            this.SpeakerWidthLabel.TabIndex = 12;
            this.SpeakerWidthLabel.Text = "Ширина (WS):";
            // 
            // RelayDiameterLabel
            // 
            this.RelayDiameterLabel.AutoSize = true;
            this.RelayDiameterLabel.Location = new System.Drawing.Point(6, 30);
            this.RelayDiameterLabel.Name = "RelayDiameterLabel";
            this.RelayDiameterLabel.Size = new System.Drawing.Size(73, 13);
            this.RelayDiameterLabel.TabIndex = 13;
            this.RelayDiameterLabel.Text = "Диаметр (D):";
            // 
            // СaseDimensionsGroupBox
            // 
            this.СaseDimensionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseHeightTextBox);
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseHeightlLabel);
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseLengthTextBox);
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseLengthLabel);
            this.СaseDimensionsGroupBox.Controls.Add(this.BoundaryValueWLabel);
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseWidthTextBox);
            this.СaseDimensionsGroupBox.Controls.Add(this.BoundaryValueLLabel);
            this.СaseDimensionsGroupBox.Controls.Add(this.CaseWidthLabel);
            this.СaseDimensionsGroupBox.Location = new System.Drawing.Point(9, 9);
            this.СaseDimensionsGroupBox.Name = "СaseDimensionsGroupBox";
            this.СaseDimensionsGroupBox.Size = new System.Drawing.Size(322, 109);
            this.СaseDimensionsGroupBox.TabIndex = 14;
            this.СaseDimensionsGroupBox.TabStop = false;
            this.СaseDimensionsGroupBox.Text = "Габариты корпуса";
            // 
            // BoundaryValueWLabel
            // 
            this.BoundaryValueWLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueWLabel.AutoSize = true;
            this.BoundaryValueWLabel.Location = new System.Drawing.Point(177, 82);
            this.BoundaryValueWLabel.Name = "BoundaryValueWLabel";
            this.BoundaryValueWLabel.Size = new System.Drawing.Size(100, 13);
            this.BoundaryValueWLabel.TabIndex = 20;
            this.BoundaryValueWLabel.Text = "(от 150 до 200) мм";
            // 
            // BoundaryValueLLabel
            // 
            this.BoundaryValueLLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueLLabel.AutoSize = true;
            this.BoundaryValueLLabel.Location = new System.Drawing.Point(177, 56);
            this.BoundaryValueLLabel.Name = "BoundaryValueLLabel";
            this.BoundaryValueLLabel.Size = new System.Drawing.Size(100, 13);
            this.BoundaryValueLLabel.TabIndex = 19;
            this.BoundaryValueLLabel.Text = "(от 200 до 300) мм";
            // 
            // SpeakerDimensionsGroupBox
            // 
            this.SpeakerDimensionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeakerDimensionsGroupBox.Controls.Add(this.BoundaryValueLSLabel);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.BoundaryValueWSLabel);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.BoundaryValueHSLabel);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerLengthTextBox);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerWidthTextBox);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerWidthLabel);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerLengthLabel);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerHeightTextBox);
            this.SpeakerDimensionsGroupBox.Controls.Add(this.SpeakerHeightLabel);
            this.SpeakerDimensionsGroupBox.Location = new System.Drawing.Point(9, 188);
            this.SpeakerDimensionsGroupBox.Name = "SpeakerDimensionsGroupBox";
            this.SpeakerDimensionsGroupBox.Size = new System.Drawing.Size(322, 109);
            this.SpeakerDimensionsGroupBox.TabIndex = 15;
            this.SpeakerDimensionsGroupBox.TabStop = false;
            this.SpeakerDimensionsGroupBox.Text = "Габариты динамика";
            // 
            // BoundaryValueLSLabel
            // 
            this.BoundaryValueLSLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueLSLabel.AutoSize = true;
            this.BoundaryValueLSLabel.Location = new System.Drawing.Point(177, 56);
            this.BoundaryValueLSLabel.Name = "BoundaryValueLSLabel";
            this.BoundaryValueLSLabel.Size = new System.Drawing.Size(100, 13);
            this.BoundaryValueLSLabel.TabIndex = 20;
            this.BoundaryValueLSLabel.Text = "(от 150 до 195) мм";
            // 
            // BoundaryValueWSLabel
            // 
            this.BoundaryValueWSLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueWSLabel.AutoSize = true;
            this.BoundaryValueWSLabel.Location = new System.Drawing.Point(177, 30);
            this.BoundaryValueWSLabel.Name = "BoundaryValueWSLabel";
            this.BoundaryValueWSLabel.Size = new System.Drawing.Size(82, 13);
            this.BoundaryValueWSLabel.TabIndex = 23;
            this.BoundaryValueWSLabel.Text = "(от 5 до 20) мм";
            // 
            // BoundaryValueHSLabel
            // 
            this.BoundaryValueHSLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueHSLabel.AutoSize = true;
            this.BoundaryValueHSLabel.Location = new System.Drawing.Point(177, 82);
            this.BoundaryValueHSLabel.Name = "BoundaryValueHSLabel";
            this.BoundaryValueHSLabel.Size = new System.Drawing.Size(88, 13);
            this.BoundaryValueHSLabel.TabIndex = 19;
            this.BoundaryValueHSLabel.Text = "(от 60 до 75) мм";
            // 
            // ReleDimensionsGroupBox
            // 
            this.ReleDimensionsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ReleDimensionsGroupBox.Controls.Add(this.BoundaryValueDLabel);
            this.ReleDimensionsGroupBox.Controls.Add(this.RelayDiameterTextBox);
            this.ReleDimensionsGroupBox.Controls.Add(this.RelayDiameterLabel);
            this.ReleDimensionsGroupBox.Location = new System.Drawing.Point(9, 124);
            this.ReleDimensionsGroupBox.Name = "ReleDimensionsGroupBox";
            this.ReleDimensionsGroupBox.Size = new System.Drawing.Size(322, 58);
            this.ReleDimensionsGroupBox.TabIndex = 16;
            this.ReleDimensionsGroupBox.TabStop = false;
            this.ReleDimensionsGroupBox.Text = "Габариты реле регулировки ";
            // 
            // BoundaryValueDLabel
            // 
            this.BoundaryValueDLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueDLabel.AutoSize = true;
            this.BoundaryValueDLabel.Location = new System.Drawing.Point(177, 30);
            this.BoundaryValueDLabel.Name = "BoundaryValueDLabel";
            this.BoundaryValueDLabel.Size = new System.Drawing.Size(88, 13);
            this.BoundaryValueDLabel.TabIndex = 24;
            this.BoundaryValueDLabel.Text = "(от 10 до 20) мм";
            // 
            // BuildModelButton
            // 
            this.BuildModelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildModelButton.Location = new System.Drawing.Point(259, 303);
            this.BuildModelButton.Name = "BuildModelButton";
            this.BuildModelButton.Size = new System.Drawing.Size(72, 23);
            this.BuildModelButton.TabIndex = 7;
            this.BuildModelButton.Text = "Построить";
            this.BuildModelButton.UseVisualStyleBackColor = true;
            // 
            // BoundaryValueHLabel
            // 
            this.BoundaryValueHLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BoundaryValueHLabel.AutoSize = true;
            this.BoundaryValueHLabel.Location = new System.Drawing.Point(186, 39);
            this.BoundaryValueHLabel.Name = "BoundaryValueHLabel";
            this.BoundaryValueHLabel.Size = new System.Drawing.Size(100, 13);
            this.BoundaryValueHLabel.TabIndex = 18;
            this.BoundaryValueHLabel.Text = "(от 100 до 500) мм";
            // 
            // DeleteParametersButton
            // 
            this.DeleteParametersButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteParametersButton.Location = new System.Drawing.Point(178, 303);
            this.DeleteParametersButton.Name = "DeleteParametersButton";
            this.DeleteParametersButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteParametersButton.TabIndex = 8;
            this.DeleteParametersButton.Text = "Сбросить";
            this.DeleteParametersButton.UseVisualStyleBackColor = true;
            this.DeleteParametersButton.Click += new System.EventHandler(this.ReturnInitialValueButton_Click);
            // 
            // ModelParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 335);
            this.Controls.Add(this.DeleteParametersButton);
            this.Controls.Add(this.BoundaryValueHLabel);
            this.Controls.Add(this.BuildModelButton);
            this.Controls.Add(this.SpeakerDimensionsGroupBox);
            this.Controls.Add(this.ReleDimensionsGroupBox);
            this.Controls.Add(this.СaseDimensionsGroupBox);
            this.MaximumSize = new System.Drawing.Size(500, 374);
            this.MinimumSize = new System.Drawing.Size(356, 374);
            this.Name = "ModelParametersForm";
            this.ShowIcon = false;
            this.Text = "Параметры модели";
            this.СaseDimensionsGroupBox.ResumeLayout(false);
            this.СaseDimensionsGroupBox.PerformLayout();
            this.SpeakerDimensionsGroupBox.ResumeLayout(false);
            this.SpeakerDimensionsGroupBox.PerformLayout();
            this.ReleDimensionsGroupBox.ResumeLayout(false);
            this.ReleDimensionsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox CaseLengthTextBox;
        private System.Windows.Forms.TextBox CaseHeightTextBox;
        private System.Windows.Forms.TextBox CaseWidthTextBox;
        private System.Windows.Forms.TextBox SpeakerHeightTextBox;
        private System.Windows.Forms.TextBox SpeakerLengthTextBox;
        private System.Windows.Forms.TextBox SpeakerWidthTextBox;
        private System.Windows.Forms.TextBox RelayDiameterTextBox;
        private System.Windows.Forms.Label CaseHeightlLabel;
        private System.Windows.Forms.Label CaseLengthLabel;
        private System.Windows.Forms.Label CaseWidthLabel;
        private System.Windows.Forms.Label SpeakerHeightLabel;
        private System.Windows.Forms.Label SpeakerLengthLabel;
        private System.Windows.Forms.Label SpeakerWidthLabel;
        private System.Windows.Forms.Label RelayDiameterLabel;
        private System.Windows.Forms.GroupBox СaseDimensionsGroupBox;
        private System.Windows.Forms.GroupBox SpeakerDimensionsGroupBox;
        private System.Windows.Forms.GroupBox ReleDimensionsGroupBox;
        private System.Windows.Forms.Button BuildModelButton;
        private System.Windows.Forms.Label BoundaryValueHLabel;
        private System.Windows.Forms.Label BoundaryValueLLabel;
        private System.Windows.Forms.Label BoundaryValueWLabel;
        private System.Windows.Forms.Label BoundaryValueWSLabel;
        private System.Windows.Forms.Label BoundaryValueDLabel;
        private System.Windows.Forms.Label BoundaryValueLSLabel;
        private System.Windows.Forms.Label BoundaryValueHSLabel;
        private System.Windows.Forms.Button DeleteParametersButton;
    }
}

