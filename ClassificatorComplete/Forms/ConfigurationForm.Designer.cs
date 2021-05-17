
namespace ClassificatorComplete.Forms
{
    partial class ConfigurationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonAcceptRule = new System.Windows.Forms.Button();
            this.groupBoxValueField = new System.Windows.Forms.GroupBox();
            this.textBoxField = new System.Windows.Forms.TextBox();
            this.buttonDeleteValueField = new System.Windows.Forms.Button();
            this.buttonPlusValueField = new System.Windows.Forms.Button();
            this.buttonDeleteRule = new System.Windows.Forms.Button();
            this.labelTypeName = new System.Windows.Forms.Label();
            this.labelFamilyName = new System.Windows.Forms.Label();
            this.textBoxTypeName = new System.Windows.Forms.TextBox();
            this.textBoxFamilyName = new System.Windows.Forms.TextBox();
            this.comboBoxBuiltIn = new System.Windows.Forms.ComboBox();
            this.buttonAddElems = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panelOfRules = new System.Windows.Forms.Panel();
            this.groupBox.SuspendLayout();
            this.groupBoxValueField.SuspendLayout();
            this.panelOfRules.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.buttonAcceptRule);
            this.groupBox.Controls.Add(this.groupBoxValueField);
            this.groupBox.Controls.Add(this.buttonDeleteRule);
            this.groupBox.Controls.Add(this.labelTypeName);
            this.groupBox.Controls.Add(this.labelFamilyName);
            this.groupBox.Controls.Add(this.textBoxTypeName);
            this.groupBox.Controls.Add(this.textBoxFamilyName);
            this.groupBox.Controls.Add(this.comboBoxBuiltIn);
            this.groupBox.Location = new System.Drawing.Point(3, 3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(839, 137);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Правило 1";
            // 
            // buttonAcceptRule
            // 
            this.buttonAcceptRule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAcceptRule.Location = new System.Drawing.Point(806, 15);
            this.buttonAcceptRule.Name = "buttonAcceptRule";
            this.buttonAcceptRule.Size = new System.Drawing.Size(27, 25);
            this.buttonAcceptRule.TabIndex = 13;
            this.buttonAcceptRule.Text = "V";
            this.buttonAcceptRule.UseVisualStyleBackColor = true;
            // 
            // groupBoxValueField
            // 
            this.groupBoxValueField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxValueField.Controls.Add(this.textBoxField);
            this.groupBoxValueField.Controls.Add(this.buttonDeleteValueField);
            this.groupBoxValueField.Controls.Add(this.buttonPlusValueField);
            this.groupBoxValueField.Location = new System.Drawing.Point(6, 16);
            this.groupBoxValueField.Name = "groupBoxValueField";
            this.groupBoxValueField.Size = new System.Drawing.Size(639, 47);
            this.groupBoxValueField.TabIndex = 12;
            this.groupBoxValueField.TabStop = false;
            this.groupBoxValueField.Text = "Значение параметра 1";
            // 
            // textBoxField
            // 
            this.textBoxField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxField.Location = new System.Drawing.Point(6, 17);
            this.textBoxField.Name = "textBoxField";
            this.textBoxField.Size = new System.Drawing.Size(575, 20);
            this.textBoxField.TabIndex = 1;
            // 
            // buttonDeleteValueField
            // 
            this.buttonDeleteValueField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteValueField.Location = new System.Drawing.Point(613, 16);
            this.buttonDeleteValueField.Name = "buttonDeleteValueField";
            this.buttonDeleteValueField.Size = new System.Drawing.Size(20, 20);
            this.buttonDeleteValueField.TabIndex = 11;
            this.buttonDeleteValueField.Text = "х";
            this.buttonDeleteValueField.UseVisualStyleBackColor = true;
            // 
            // buttonPlusValueField
            // 
            this.buttonPlusValueField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlusValueField.Location = new System.Drawing.Point(587, 16);
            this.buttonPlusValueField.Name = "buttonPlusValueField";
            this.buttonPlusValueField.Size = new System.Drawing.Size(20, 20);
            this.buttonPlusValueField.TabIndex = 3;
            this.buttonPlusValueField.Text = "+";
            this.buttonPlusValueField.UseVisualStyleBackColor = true;
            this.buttonPlusValueField.Click += new System.EventHandler(this.buttonPlusValueField_Click);
            // 
            // buttonDeleteRule
            // 
            this.buttonDeleteRule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteRule.Location = new System.Drawing.Point(806, 49);
            this.buttonDeleteRule.Name = "buttonDeleteRule";
            this.buttonDeleteRule.Size = new System.Drawing.Size(27, 82);
            this.buttonDeleteRule.TabIndex = 8;
            this.buttonDeleteRule.Text = "Х";
            this.buttonDeleteRule.UseVisualStyleBackColor = true;
            this.buttonDeleteRule.Click += new System.EventHandler(this.buttonDeleteRule_Click);
            // 
            // labelTypeName
            // 
            this.labelTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTypeName.AutoSize = true;
            this.labelTypeName.Location = new System.Drawing.Point(651, 94);
            this.labelTypeName.Name = "labelTypeName";
            this.labelTypeName.Size = new System.Drawing.Size(110, 13);
            this.labelTypeName.TabIndex = 7;
            this.labelTypeName.Text = "Значение TypeName";
            // 
            // labelFamilyName
            // 
            this.labelFamilyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFamilyName.AutoSize = true;
            this.labelFamilyName.Location = new System.Drawing.Point(651, 49);
            this.labelFamilyName.Name = "labelFamilyName";
            this.labelFamilyName.Size = new System.Drawing.Size(115, 13);
            this.labelFamilyName.TabIndex = 6;
            this.labelFamilyName.Text = "Значение FamilyName";
            // 
            // textBoxTypeName
            // 
            this.textBoxTypeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTypeName.Location = new System.Drawing.Point(654, 111);
            this.textBoxTypeName.Name = "textBoxTypeName";
            this.textBoxTypeName.Size = new System.Drawing.Size(146, 20);
            this.textBoxTypeName.TabIndex = 5;
            // 
            // textBoxFamilyName
            // 
            this.textBoxFamilyName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFamilyName.Location = new System.Drawing.Point(654, 66);
            this.textBoxFamilyName.Name = "textBoxFamilyName";
            this.textBoxFamilyName.Size = new System.Drawing.Size(146, 20);
            this.textBoxFamilyName.TabIndex = 4;
            // 
            // comboBoxBuiltIn
            // 
            this.comboBoxBuiltIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBuiltIn.FormattingEnabled = true;
            this.comboBoxBuiltIn.Location = new System.Drawing.Point(654, 19);
            this.comboBoxBuiltIn.Name = "comboBoxBuiltIn";
            this.comboBoxBuiltIn.Size = new System.Drawing.Size(146, 21);
            this.comboBoxBuiltIn.TabIndex = 0;
            // 
            // buttonAddElems
            // 
            this.buttonAddElems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddElems.Location = new System.Drawing.Point(742, 12);
            this.buttonAddElems.Name = "buttonAddElems";
            this.buttonAddElems.Size = new System.Drawing.Size(115, 23);
            this.buttonAddElems.TabIndex = 1;
            this.buttonAddElems.Text = "Добавить правило";
            this.buttonAddElems.UseVisualStyleBackColor = true;
            this.buttonAddElems.Click += new System.EventHandler(this.buttonAddElems_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Параметр 1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(91, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(140, 20);
            this.textBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 9;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(263, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 10;
            this.button2.Text = "х";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panelOfRules
            // 
            this.panelOfRules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOfRules.AutoScroll = true;
            this.panelOfRules.Controls.Add(this.groupBox);
            this.panelOfRules.Location = new System.Drawing.Point(12, 41);
            this.panelOfRules.Name = "panelOfRules";
            this.panelOfRules.Size = new System.Drawing.Size(845, 531);
            this.panelOfRules.TabIndex = 11;
            // 
            // ConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(869, 584);
            this.Controls.Add(this.panelOfRules);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAddElems);
            this.Name = "ConfigurationForm";
            this.Text = "ConfigurationForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigurationForm_FormClosed);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBoxValueField.ResumeLayout(false);
            this.groupBoxValueField.PerformLayout();
            this.panelOfRules.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button buttonPlusValueField;
        private System.Windows.Forms.TextBox textBoxField;
        private System.Windows.Forms.ComboBox comboBoxBuiltIn;
        private System.Windows.Forms.Button buttonDeleteRule;
        private System.Windows.Forms.Label labelTypeName;
        private System.Windows.Forms.Label labelFamilyName;
        private System.Windows.Forms.TextBox textBoxTypeName;
        private System.Windows.Forms.TextBox textBoxFamilyName;
        private System.Windows.Forms.Button buttonAddElems;
        private System.Windows.Forms.Button buttonDeleteValueField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panelOfRules;
        private System.Windows.Forms.GroupBox groupBoxValueField;
        private System.Windows.Forms.Button buttonAcceptRule;
    }
}