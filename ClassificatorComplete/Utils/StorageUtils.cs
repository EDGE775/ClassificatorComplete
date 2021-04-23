using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.IO;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    public class StorageUtils
    {
        public InfosStorage getInfoStorage()
        {
            string dllPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folder = System.IO.Path.GetDirectoryName(dllPath);

            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = folder;
            dialog.Multiselect = false;
            dialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            string newFilePath = dialog.FileName;
            if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return null;
            string xmlFilePath = dialog.FileName;

            InfosStorage storage = new InfosStorage();
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(InfosStorage));

            using (StreamReader r = new StreamReader(xmlFilePath))
            {
                storage = (InfosStorage)serializer.Deserialize(r);
            }

            return storage;
        }
    }
}
