using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    public class ParamUtils
    {
        public static bool nameChecker(string nameClafi, string nameElem)
        {
            string[] arrayClafiAnd = nameClafi.ToLower().Split(',');
            int index = arrayClafiAnd.Length;

            if (index == 1)
            {
                if (nameElem.ToLower().Contains(arrayClafiAnd.First()))
                {
                    return true;
                }
                return false;
            }
            else if (index > 1)
            {
                for (int i = 0; i < index; i++)
                {
                    if (arrayClafiAnd[i].Contains('|'))
                    {
                        bool check = false;
                        string[] arrayClafiOr = arrayClafiAnd[i].Split('|');
                        for (int j = 0; j < arrayClafiOr.Length; j++)
                        {
                            if (nameElem.ToLower().Contains(arrayClafiOr[j]))
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check) continue;
                        else return false;
                    }
                    else if (nameElem.ToLower().Contains(arrayClafiAnd[i])) continue;
                    else return false;
                }
                return true;
            }
            return false;
        }

        public static bool setParam(Element elem, string paramName, string valueName)
        {
            bool rsl = false;
            try
            {
                elem.LookupParameter(paramName).Set(valueName);
                rsl = true;
            }
            catch (Exception)
            {
                Print("Не удалось назначить параметр: " + paramName, KPLN_Loader.Preferences.MessageType.Warning);
            }
            return rsl;
        }

        public static void setBuiltInParam(ElementType elem, BuiltInParameter paramName, string valueName)
        {
            try
            {
                elem.get_Parameter(paramName).Set(valueName);
            }
            catch (Exception e)
            {
                PrintError(e);
            }
        }

        //public static bool checkParams(Element elem, List<ClassificatorByInstanse> valuesNamesArray, InfosStorage storage)
        //{
        //    bool check = false;
        //    List<Parameter> familyParams = elem.GetOrderedParameters().Where(i => i.StorageType == StorageType.String).ToList();
        //    List<string> familyParamsStr = new List<string>();
        //    familyParamsStr.Add("");
        //    for (int i = 0; i < familyParams.Count; i++)
        //    {
        //        familyParamsStr.Add(familyParams[i].Definition.Name);
        //    }
        //    foreach (ClassificatorByInstanse names in valuesNamesArray)
        //    {
        //        for (int i = 0; i < names.paramsValues.Count; i++)
        //        {
        //            Print(names.paramsValues[i].Length.ToString() + " " + familyParamsStr.Contains(storage.instanseParams[i]).ToString(), KPLN_Loader.Preferences.MessageType.Regular);
        //            if (names.paramsValues[i].Length == 0 && !familyParamsStr.Contains(storage.instanseParams[i]))
        //            {
        //                check = false;
        //                break; 
        //            }
        //            check = true;
        //        }
        //        if (check) return true;
        //    }
        //    return false;
        //}

        public static void setClassificator(ClassificatorByInstanse classificator, InfosStorage storage, Element elem, bool check)
        {
            bool paramChecker;
            List<string> assignedValues = new List<string>();

            if (classificator.paramsValues.Count > storage.instanseParams.Count)
            {
                Print(string.Format("Значение параметра: \"{0}\" в элементе: \"{1}\" за пределами диапазона возможных значений. Присвоение данного параметра не будет выполнено."
                    , classificator.paramsValues[classificator.paramsValues.Count - 1]
                    , classificator.FamilyName)
                    , KPLN_Loader.Preferences.MessageType.Warning);
            }
            for (int i = 0; i < Math.Min(classificator.paramsValues.Count, storage.instanseParams.Count); i++)
            {
                if (classificator.paramsValues[i].Length == 0) continue;
                paramChecker = setParam(elem, storage.instanseParams[i], classificator.paramsValues[i]);
                if (paramChecker)
                {
                    assignedValues.Add(classificator.paramsValues[i]);
                }
            }
            if (check)
            {
                Print(string.Format("Были присвоены значения: {0}", string.Join(", ", assignedValues)), KPLN_Loader.Preferences.MessageType.System_OK);

            }
        }

        private static string getValueStringOfAllParams(Element elem, string paramName)
        {
            string paramValue = null;
            Parameter paramObject = elem.LookupParameter(paramName);
            try
            {
                if (paramObject.StorageType == StorageType.String)
                {
                    paramValue = paramObject.AsString().ToString();
                }
                else if (paramObject.StorageType == StorageType.Double)
                {
                    paramValue = paramObject.AsDouble().ToString();
                }
                else if (paramObject.StorageType == StorageType.Integer)
                {
                    paramValue = paramObject.AsInteger().ToString();
                }
                else
                {
                    Print("Не удалось определить тип параметра: " + paramName, KPLN_Loader.Preferences.MessageType.Error);
                }
            }
            catch (Exception) {}
            return paramValue;
        }
    }
}
