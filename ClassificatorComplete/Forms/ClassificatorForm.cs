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
using ClassificatorComplete.Utils;

namespace ClassificatorComplete.Forms
{
    public partial class ClassificatorForm : Form
    {
        private StorageUtils utils;
        public InfosStorage storage;
        public bool debugMode;
        public int instanceOrType;
        public List<BuiltInCategory> checkedCats;
        public MainWindow form;
        public List<BuiltInCategory> allCats;
        public List<MyParameter> mparams;

        public ClassificatorForm(StorageUtils utils, List<MyParameter> mparams)
        {
            InitializeComponent();
            this.utils = utils;
            debugMode = checkBoxDebug.Checked;
            textBoxFileInfo.Text = "Конфигурационный файл не выбран.";
            btnOk.Enabled = false;
            checkedCats = new List<BuiltInCategory>();
            if (radioButtonTypeParams.Checked) instanceOrType = 2;
            else if (radioButtonInstanceParams.Checked) instanceOrType = 1;
            buttonOpenConfiguration.Enabled = false;
            buttonSaveFile.Enabled = false;
            this.mparams = mparams;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                BuiltInCategory cat = (BuiltInCategory)checkedItem;
                checkedCats.Add(cat);
            }

            //this.DialogResult = DialogResult.OK;
            //this.Close();

            KPLN_Loader.Preferences.CommandQueue.Enqueue(new CommandStartClassificator(this));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            storage = utils.getInfoStorage();
            if (storage != null)
            {
                checkFileIsCorrect();
                setRadioButton();
            }
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

        private bool checkFileIsCorrect()
        {
            if ((storage.instanceOrType == 1 && instanceOrType == 1) || (storage.instanceOrType == 2 && instanceOrType == 2))
            {
                textBoxFileInfo.Text = "";
                textBoxFileInfo.Text += "Выбран конфигурационный файл:" + Environment.NewLine;
                textBoxFileInfo.Text += Environment.NewLine;
                textBoxFileInfo.Text += utils.xmlFilePath != null ? utils.xmlFilePath + Environment.NewLine : "Создан новый конфигурационный файл. Файл не сохранён!";
                textBoxFileInfo.Text += Environment.NewLine;
                string info = storage.instanceOrType == 1 ? "ЭКЗЕМПЛЯРУ" : storage.instanceOrType == 2 ? "ТИПУ" : "НЕКОРРЕКТНАЯ НАСТРОЙКА КОНФИГУРАЦИОННОГО ФАЙЛА!";
                textBoxFileInfo.Text += string.Format("Файл содержит {0} правил(о/а/ов) для заполнения классиифкатора по {1}.", storage.classificator.Count.ToString(), info);
                btnOk.Enabled = true;

                checkedListBox1.Items.Clear();
                foreach (BuiltInCategory bic in storage.classificator.Select(x => x.BuiltInName).ToHashSet().ToList())
                {
                    checkedListBox1.Items.Add(bic, CheckState.Checked);
                }
                buttonOpenConfiguration.Enabled = true;
                return true;
            }
            else
            {
                checkedListBox1.Items.Clear();
                textBoxFileInfo.Text = "";
                textBoxFileInfo.Text += "НЕКОРРЕКТНАЯ НАСТРОЙКА КОНФИГУРАЦИОННОГО ФАЙЛА!";
                btnOk.Enabled = false;
                buttonSaveFile.Enabled = false;
                return false;
            }
        }

        private void setRadioButton()
        {
            if (storage != null && storage.instanceOrType == 1)
            {
                radioButtonInstanceParams.Checked = true;
            }
            else if (storage != null && storage.instanceOrType == 2)
            {
                radioButtonTypeParams.Checked = true;
            }
        }

        private void buttonOpenConfiguration_Click(object sender, EventArgs e)
        {
            if (allCats == null)
            {
                allCats = new List<BuiltInCategory>();
                foreach (var item in Enum.GetValues(typeof(BuiltInCategory)))
                {
                    allCats.Add((BuiltInCategory)item);
                }
                allCats.Sort();
            }
            this.Hide();
            form = new MainWindow(this, storage, allCats);
            form.Show();
        }

        private void ClassificatorForm_VisibleChanged(object sender, EventArgs e)
        {
            if (storage != null && storage.instanceOrType != 0)
            {
                checkFileIsCorrect();
                setRadioButton();
                buttonOpenConfiguration.Enabled = true;
                buttonSaveFile.Enabled = true;
            }
            else
            {
                buttonOpenConfiguration.Enabled = false;
                buttonSaveFile.Enabled = false;
            }
        }

        private void buttonCreateNewConfiguration_Click(object sender, EventArgs e)
        {
            if (storage != null && storage.instanceOrType != 0 && MessageBox.Show("При создании нового конфигурационного файла, старый будет удалён!", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            if (allCats == null)
            {
                allCats = new List<BuiltInCategory>();
                foreach (var item in Enum.GetValues(typeof(BuiltInCategory)))
                {
                    allCats.Add((BuiltInCategory)item);
                }
                allCats.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
            }
            this.Hide();
            storage = new InfosStorage();
            form = new MainWindow(this, storage, allCats);
            form.Show();
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            utils.saveInfoStorage(storage);
            checkFileIsCorrect();
        }
    }
}
