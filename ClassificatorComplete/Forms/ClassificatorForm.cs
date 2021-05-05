using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElementId = Autodesk.Revit.DB.ElementId;
using Category = Autodesk.Revit.DB.Category;
using BuiltInCategory = Autodesk.Revit.DB.BuiltInCategory;


namespace ClassificatorComplete.Forms
{
    public partial class ClassificatorForm : Form
    {
        private StorageUtils utils;
        public InfosStorage storage;
        public bool debugMode;
        public int instanceOrType;
        public List<BuiltInCategory> checkedCats;
        public ConfigurationForm form;

        public ClassificatorForm(StorageUtils utils)
        {
            InitializeComponent();
            this.utils = utils;
            debugMode = checkBoxDebug.Checked;
            textBoxFileInfo.Text = "Конфигурационный файл не выбран.";
            btnOk.Enabled = false;
            checkedCats = new List<BuiltInCategory>();
            if (radioButtonTypeParams.Checked) instanceOrType = 2;
            else if (radioButtonInstanceParams.Checked) instanceOrType = 1;
            form = new ConfigurationForm(this);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                BuiltInCategory cat = (BuiltInCategory)checkedItem;
                checkedCats.Add(cat);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            storage = utils.getInfoStorage();
            checkFileIsCorrect();
        }

        private void checkBoxDebug_CheckedChanged(object sender, EventArgs e)
        {
            debugMode = checkBoxDebug.Checked;
        }

        private void radioButtonTypeParams_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                instanceOrType = 2;
            }
            if (storage != null)
            {
                checkFileIsCorrect();
            }
        }

        private void radioButtonInstanceParams_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                instanceOrType = 1;
            }
            if (storage != null)
            {
                checkFileIsCorrect();
            }
        }

        private void checkFileIsCorrect()
        {
            if ((storage.instanceOrType == 1 && instanceOrType == 1) || (storage.instanceOrType == 2 && instanceOrType == 2))
            {
                textBoxFileInfo.Text = "";
                textBoxFileInfo.Text += "Выбран конфигурационный файл:" + Environment.NewLine;
                textBoxFileInfo.Text += Environment.NewLine;
                textBoxFileInfo.Text += utils.xmlFilePath + Environment.NewLine;
                textBoxFileInfo.Text += Environment.NewLine;
                string info = storage.instanceOrType == 1 ? "ЭКЗЕМПЛЯРУ" : storage.instanceOrType == 2 ? "ТИПУ" : "НЕКОРРЕКТНАЯ НАСТРОЙКА КОНФИГУРАЦИОННОГО ФАЙЛА!";
                textBoxFileInfo.Text += string.Format("Файл содержит {0} правил(о) для заполнения классиифкатора по {1}.", storage.classificator.Count.ToString(), info);
                btnOk.Enabled = true;

                checkedListBox1.Items.Clear();
                foreach (BuiltInCategory bic in storage.classificator.Select(x => x.BuiltInName).ToHashSet().ToList())
                {
                    checkedListBox1.Items.Add(bic, CheckState.Checked);
                }
            }
            else
            {
                checkedListBox1.Items.Clear();
                textBoxFileInfo.Text = "";
                textBoxFileInfo.Text += "НЕКОРРЕКТНАЯ НАСТРОЙКА КОНФИГУРАЦИОННОГО ФАЙЛА!";
                btnOk.Enabled = false;
            }
        }

        private void buttonOpenConfiguration_Click(object sender, EventArgs e)
        {
            this.Hide();
            form.ShowDialog();
        }
    }
}
