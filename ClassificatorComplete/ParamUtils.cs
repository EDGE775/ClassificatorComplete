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
        public List<Element> fullSuccessElems = new List<Element>();
        public List<Element> notFullSuccessElems = new List<Element>();
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

        public static bool setParam(Element elem, string paramName, string value)
        {
            bool rsl = false;
            string newValue = value;
            if (value.Contains("[") && value.Contains("]"))
            {
                char[] valueArray = value.ToCharArray();
                Dictionary<string, string> foundParamsAndTheirValues = new Dictionary<string, string>();
                for (int i = 0; i < valueArray.Length; i++)
                {
                    if (valueArray[i] == '[')
                    {
                        for (int j = i; j < valueArray.Length; j++)
                        {
                            if (valueArray[j] == ']')
                            {
                                string foundParamName = value.Substring(i, j - i + 1);
                                string foundParamNameForGettingValue = foundParamName.Replace("]", "").Replace("[", "");
                                string valueOfParam = null;
                                if (foundParamName.Contains("*"))
                                {
                                    valueOfParam = getValueStringOfAllParams(elem, foundParamNameForGettingValue.Split('*')[0]);
                                    try
                                    {
                                        valueOfParam = multipleSourseParamOnMultiplier(valueOfParam, foundParamNameForGettingValue);

                                        //double paramValue = double.Parse(valueOfParam);
                                        //double multiplier = double.Parse(foundParamNameForGettingValue.Split('*')[1].Split('D')[0].Replace(".", ","));
                                        //int digits = 0;
                                        //if (foundParamNameForGettingValue.Contains("D"))
                                        //{
                                        //    int.TryParse(foundParamNameForGettingValue.Split('D')[1], out digits);
                                        //}
                                        //double result = paramValue * multiplier;
                                        //valueOfParam = foundParamNameForGettingValue.Contains("D") ? Math.Round(result, digits == 0 ? 1 : digits).ToString() : Math.Round(result).ToString();
                                    }
                                    catch (Exception)
                                    {
                                        Print(string.Format("Значение параметра: \"{0}\" в классификаторе содержит операцию умножения (*), которое не было выполнено. Проверьте корректность заполнения xml файла. Значение не вписано в параметр: \"{1}\".", foundParamName, paramName), KPLN_Loader.Preferences.MessageType.Warning);
                                        return rsl;
                                    }
                                }
                                else
                                {
                                    valueOfParam = getValueStringOfAllParams(elem, foundParamNameForGettingValue);
                                }
                                foundParamsAndTheirValues.Add(foundParamName, valueOfParam);
                                break;
                            }
                        }
                    }
                }
                foreach (string item in foundParamsAndTheirValues.Keys)
                {
                    string itemValue = foundParamsAndTheirValues[item];
                    if (itemValue == null || itemValue.Length == 0)
                    {
                        Print(string.Format("Не заполнено значение параметра: \"{0}\" у элемента: {1} с id: {2}. Значение не вписано в параметр: \"{3}\".", item, elem.Name, elem.Id, paramName), KPLN_Loader.Preferences.MessageType.Warning);
                        return rsl;
                    }
                    newValue = newValue.Replace(item, itemValue);
                }
            }

            try
            {
                elem.LookupParameter(paramName).Set(newValue);
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

        public void setClassificator(ClassificatorBy classificator, InfosStorage storage, Element elem, bool check)
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
            if (assignedValues.Count == Math.Min(classificator.paramsValues.Where(i => i.Length > 0).Count(), storage.instanseParams.Where(i => i.Length > 0).Count()))
            {
                fullSuccessElems.Add(elem);
            }
            else
            {
                notFullSuccessElems.Add(elem);
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
            if (paramObject == null)
            {
                paramObject = elem.Document.GetElement(elem.GetTypeId()).LookupParameter(paramName);
            }
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
            catch (Exception) { }
            return paramValue;
        }

        private static string multipleSourseParamOnMultiplier(string valueOfParam, string foundParamNameForGettingValue) 
        {
            double paramValue = double.Parse(valueOfParam);
            double multiplier = double.Parse(foundParamNameForGettingValue.Split('*')[1].Split('D')[0].Replace(".", ","));
            int digits = 0;
            if (foundParamNameForGettingValue.Contains("D"))
            {
                int.TryParse(foundParamNameForGettingValue.Split('D')[1], out digits);
            }
            double result = paramValue * multiplier;
            return foundParamNameForGettingValue.Contains("D") ? Math.Round(result, digits == 0 ? 1 : digits).ToString() : Math.Round(result).ToString();
        }
    }
}
