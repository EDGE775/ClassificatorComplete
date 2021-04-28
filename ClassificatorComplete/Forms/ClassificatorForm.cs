using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassificatorComplete.Forms
{
    public partial class ClassificatorForm : Form
    {
        private StorageUtils utils;
        public InfosStorage storage;
        public ClassificatorForm(StorageUtils utils)
        {
            InitializeComponent();
            this.utils = utils;
            textBoxFileInfo.Text = "Конфигурационный файл не выбран.";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void buttonChooseFile_Click(object sender, EventArgs e)
        {
            storage = utils.getInfoStorage();
            textBoxFileInfo.Text = string.Format("Выбран конфигурационный файл: {0}\nФайл содержит {1} правил для заполнения классификатора.", utils.xmlFilePath, storage.classificator.Count);
        }
    }
}
