﻿
namespace ClassificatorComplete.Forms
{
    partial class ClassificatorForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonInstanceParams = new System.Windows.Forms.RadioButton();
            this.radioButtonTypeParams = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDebug = new System.Windows.Forms.CheckBox();
            this.buttonChooseFile = new System.Windows.Forms.Button();
            this.textBoxFileInfo = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.buttonOpenConfiguration = new System.Windows.Forms.Button();
            this.buttonCreateNewConfiguration = new System.Windows.Forms.Button();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(12, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(231, 470);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(141, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Классифицировать!";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(373, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Выберите категории для классификации (для опытных пользователей):";
            // 
            // radioButtonInstanceParams
            // 
            this.radioButtonInstanceParams.AutoSize = true;
            this.radioButtonInstanceParams.Checked = true;
            this.radioButtonInstanceParams.Enabled = false;
            this.radioButtonInstanceParams.Location = new System.Drawing.Point(6, 42);
            this.radioButtonInstanceParams.Name = "radioButtonInstanceParams";
            this.radioButtonInstanceParams.Size = new System.Drawing.Size(153, 17);
            this.radioButtonInstanceParams.TabIndex = 4;
            this.radioButtonInstanceParams.TabStop = true;
            this.radioButtonInstanceParams.Text = "Заполние по экземпляру";
            this.radioButtonInstanceParams.UseVisualStyleBackColor = true;
            this.radioButtonInstanceParams.CheckedChanged += new System.EventHandler(this.radioButtonInstanceParams_CheckedChanged);
            // 
            // radioButtonTypeParams
            // 
            this.radioButtonTypeParams.AutoSize = true;
            this.radioButtonTypeParams.Enabled = false;
            this.radioButtonTypeParams.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTypeParams.Name = "radioButtonTypeParams";
            this.radioButtonTypeParams.Size = new System.Drawing.Size(114, 17);
            this.radioButtonTypeParams.TabIndex = 5;
            this.radioButtonTypeParams.Text = "Заполние по типу";
            this.radioButtonTypeParams.UseVisualStyleBackColor = true;
            this.radioButtonTypeParams.CheckedChanged += new System.EventHandler(this.radioButtonTypeParams_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonTypeParams);
            this.groupBox1.Controls.Add(this.radioButtonInstanceParams);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Алгоритм";
            // 
            // checkBoxDebug
            // 
            this.checkBoxDebug.AutoSize = true;
            this.checkBoxDebug.Location = new System.Drawing.Point(21, 89);
            this.checkBoxDebug.Name = "checkBoxDebug";
            this.checkBoxDebug.Size = new System.Drawing.Size(174, 17);
            this.checkBoxDebug.TabIndex = 7;
            this.checkBoxDebug.Text = "Вывести лог работы плагина";
            this.checkBoxDebug.UseVisualStyleBackColor = true;
            this.checkBoxDebug.CheckedChanged += new System.EventHandler(this.checkBoxDebug_CheckedChanged);
            // 
            // buttonChooseFile
            // 
            this.buttonChooseFile.Location = new System.Drawing.Point(12, 117);
            this.buttonChooseFile.Name = "buttonChooseFile";
            this.buttonChooseFile.Size = new System.Drawing.Size(183, 23);
            this.buttonChooseFile.TabIndex = 8;
            this.buttonChooseFile.Text = "Выбрать файл конфигурации";
            this.buttonChooseFile.UseVisualStyleBackColor = true;
            this.buttonChooseFile.Click += new System.EventHandler(this.buttonChooseFile_Click);
            // 
            // textBoxFileInfo
            // 
            this.textBoxFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFileInfo.Location = new System.Drawing.Point(12, 146);
            this.textBoxFileInfo.Multiline = true;
            this.textBoxFileInfo.Name = "textBoxFileInfo";
            this.textBoxFileInfo.ReadOnly = true;
            this.textBoxFileInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxFileInfo.Size = new System.Drawing.Size(360, 152);
            this.textBoxFileInfo.TabIndex = 9;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 317);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(360, 139);
            this.checkedListBox1.TabIndex = 10;
            // 
            // buttonOpenConfiguration
            // 
            this.buttonOpenConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenConfiguration.Location = new System.Drawing.Point(286, 20);
            this.buttonOpenConfiguration.Name = "buttonOpenConfiguration";
            this.buttonOpenConfiguration.Size = new System.Drawing.Size(86, 28);
            this.buttonOpenConfiguration.TabIndex = 11;
            this.buttonOpenConfiguration.Text = "Ред. файл";
            this.buttonOpenConfiguration.UseVisualStyleBackColor = true;
            this.buttonOpenConfiguration.Click += new System.EventHandler(this.buttonOpenConfiguration_Click);
            // 
            // buttonCreateNewConfiguration
            // 
            this.buttonCreateNewConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCreateNewConfiguration.Location = new System.Drawing.Point(286, 55);
            this.buttonCreateNewConfiguration.Name = "buttonCreateNewConfiguration";
            this.buttonCreateNewConfiguration.Size = new System.Drawing.Size(86, 28);
            this.buttonCreateNewConfiguration.TabIndex = 12;
            this.buttonCreateNewConfiguration.Text = "Новый файл";
            this.buttonCreateNewConfiguration.UseVisualStyleBackColor = true;
            this.buttonCreateNewConfiguration.Click += new System.EventHandler(this.buttonCreateNewConfiguration_Click);
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveFile.Location = new System.Drawing.Point(261, 117);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(111, 23);
            this.buttonSaveFile.TabIndex = 13;
            this.buttonSaveFile.Text = "Сохранить файл";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // ClassificatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 502);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.buttonCreateNewConfiguration);
            this.Controls.Add(this.buttonOpenConfiguration);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.textBoxFileInfo);
            this.Controls.Add(this.buttonChooseFile);
            this.Controls.Add(this.checkBoxDebug);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.MinimumSize = new System.Drawing.Size(400, 540);
            this.Name = "ClassificatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Классификация";
            this.TopMost = true;
            this.VisibleChanged += new System.EventHandler(this.ClassificatorForm_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonInstanceParams;
        private System.Windows.Forms.RadioButton radioButtonTypeParams;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxDebug;
        private System.Windows.Forms.Button buttonChooseFile;
        private System.Windows.Forms.TextBox textBoxFileInfo;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button buttonOpenConfiguration;
        private System.Windows.Forms.Button buttonCreateNewConfiguration;
        private System.Windows.Forms.Button buttonSaveFile;
    }
}