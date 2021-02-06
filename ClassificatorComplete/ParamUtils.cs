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

        public static void setParam(Element elem, string paramName, string valueName)
        {
            try
            {
                elem.LookupParameter(paramName).Set(valueName);
            }
            catch (Exception e)
            {
                PrintError(e);
            }
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
    }
}
