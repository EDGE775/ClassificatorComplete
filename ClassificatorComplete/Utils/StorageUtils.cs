using Autodesk.Revit.DB;
using ClassificatorComplete.Data;
using System;
using System.Collections.Generic;
using System.IO;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    public class StorageUtils
    {
        public string xmlFilePath;

        public StorageUtils()
        {
            this.xmlFilePath = null;
        }
        public InfosStorage getInfoStorage()
        {
            System.Xml.Serialization.XmlSerializer storageSerializer =
                new System.Xml.Serialization.XmlSerializer(typeof(UtilsStorage));

            UtilsStorage utilsStorage = null;
            try
            {
                using (StreamReader r = new StreamReader("C:\\TEMP\\ccsettings.xml"))
                {
                    utilsStorage = (UtilsStorage)storageSerializer.Deserialize(r);
                }
                System.Windows.Forms.OpenFileDialog storageDialog = new System.Windows.Forms.OpenFileDialog();
                storageDialog.InitialDirectory = utilsStorage.path;
                storageDialog.Multiselect = false;
                storageDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (storageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    xmlFilePath = storageDialog.FileName;
                }
                else return null;
            }
            catch (Exception){ }

            if (utilsStorage == null)
            {
                string dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                string folder = System.IO.Path.GetDirectoryName(dllPath);
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.InitialDirectory = folder;
                dialog.Multiselect = false;
                dialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
                xmlFilePath = dialog.FileName;
                utilsStorage = new UtilsStorage();
            }
            utilsStorage.path = Path.GetDirectoryName(xmlFilePath);

            InfosStorage storage = new InfosStorage();
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(InfosStorage));

            using (StreamReader r = new StreamReader(xmlFilePath))
            {
                storage = (InfosStorage)serializer.Deserialize(r);
            }

            using (StreamWriter r = new StreamWriter("C:\\TEMP\\ccsettings.xml"))
            {
                storageSerializer.Serialize(r, utilsStorage);
            }

            return storage;
        }
    }
}
