using Autodesk.Revit.DB;
using Autodesk.Revit.DB.ExtensibleStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static KPLN_Loader.Output.Output;
using System.Threading.Tasks;

namespace ClassificatorComplete.Data
{
    public class LastRunInfo
    {
        private static LastRunInfo instance;
        private static readonly string schemaGuid = "720080CB-DA99-40DC-9415-E53F280AA1F8";
        private Schema sch;
        private string userName;
        private string fileName;
        private string date;
        private Document doc;

        public override string ToString()
        {
            return string.Format("Последний запуск плагина в данном файле производился: \"{0}\"=Пользователем: \"{1}\".=Был использован файл: \"{2}\"", date, userName, fileName);
        }

        private LastRunInfo(Document doc)
        {
            this.doc = doc;
            Element storageElement = this.GetStorageElement();

            if (CheckStorageExists(storageElement, schemaGuid))
            {
                getLastRunInfo();
            }
            else
            {
                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Создание Schema");

                    createSchemaStorage();
                    saveFirstRunInfo();
                    getLastRunInfo();

                    t.Commit();
                }
            }
        }

        public static LastRunInfo createInstance(Document doc)
        {
            if (instance == null)
            {
                instance = new LastRunInfo(doc);
            }
            return instance;
        }

        public static LastRunInfo getInstance()
        {
            if (instance == null)
            {
                throw new Exception("Не создан экземпляр LastRunInfo!");
            }
            return instance;
        }

        public void save(string filePath)
        {
            this.date = getCurrentTime();
            this.userName = getUserNameFromDB();
            this.fileName = filePath;
            Dictionary<string, string> fieldsAndValues = new Dictionary<string, string>();
            fieldsAndValues.Add("UserName", userName);
            fieldsAndValues.Add("FileName", fileName);
            fieldsAndValues.Add("Date", date);
            foreach (string field in fieldsAndValues.Keys)
            {
                updateField(field, fieldsAndValues[field]);
            }
        }

        private string getUserNameFromDB()
        {
            return string.Format("{0} {1}", KPLN_Loader.Preferences.User.Family, KPLN_Loader.Preferences.User.Name);
        }

        private string getCurrentTime()
        {
            DateTime dateTime = DateTime.Now;
            return dateTime.ToString();
        }

        private void createSchemaStorage()
        {
            SchemaBuilder sb = new SchemaBuilder(new Guid(schemaGuid));
            sb.SetReadAccessLevel(AccessLevel.Public);
            FieldBuilder fbName = sb.AddSimpleField("UserName", typeof(string));
            FieldBuilder fbFileName = sb.AddSimpleField("FileName", typeof(string));
            FieldBuilder fbAge = sb.AddSimpleField("Date", typeof(string));
            sb.SetSchemaName("LastRunCCPlugIn");
            sch = sb.Finish();
        }

        private Element GetStorageElement()
        {
            BrowserOrganization bo = new FilteredElementCollector(doc)
            .OfClass(typeof(BrowserOrganization))
            .Cast<BrowserOrganization>()
            .First();
            return bo as Element;
        }

        private void saveFirstRunInfo()
        {
            Element elem = this.GetStorageElement();
            Field fieldName = sch.GetField("UserName");
            Field fieldFileName = sch.GetField("FileName");
            Field fieldDate = sch.GetField("Date");
            Entity ent = new Entity(sch);
            ent.Set<string>(fieldName, "-");
            ent.Set<string>(fieldFileName, "-");
            ent.Set<string>(fieldDate, "-");
            elem.SetEntity(ent);
        }

        private void getLastRunInfo()
        {
            Schema sch = Schema.Lookup(new Guid(schemaGuid));
            Element elem = this.GetStorageElement();
            Entity ent = elem.GetEntity(sch);
            Field fieldName = sch.GetField("UserName");
            Field fieldFileName = sch.GetField("FileName");
            Field fieldDate = sch.GetField("Date");
            userName = ent.Get<string>(fieldName);
            fileName = ent.Get<string>(fieldFileName);
            date = ent.Get<string>(fieldDate);
        }

        private void updateField(string field, string newValue)
        {
            Element elem = this.GetStorageElement();
            Schema sch = Schema.Lookup(new Guid(schemaGuid));
            Entity ent = elem.GetEntity(sch);
            Field fr = sch.GetField(field);
            ent.Set<string>(fr, newValue);
            elem.SetEntity(ent);
        }

        private bool CheckStorageExists(Element elem, string sGuid)
        {
            try
            {
                Schema sch = Schema.Lookup(new Guid(sGuid));
                Entity ent = elem.GetEntity(sch);
                if (ent.Schema != null) return true;
            }
            catch { }
            return false;
        }
    }
}
