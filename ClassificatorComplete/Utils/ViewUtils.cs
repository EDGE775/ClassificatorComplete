using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KPLN_Loader.Output.Output;

namespace ClassificatorComplete
{
    class ViewUtils
    {
        private static Color fullSuccessColor = new Color(255, 1, 1);
        private static Color notfullSuccessColor = new Color(1, 1, 255);
        public OverrideGraphicSettings fullSuccessSet;
        public OverrideGraphicSettings notFullSuccessSet;
        private Document doc;

        public ViewUtils(Document doc)
        {
            this.doc = doc;
            fullSuccessSet = getGraphicSettings(fullSuccessColor);
            notFullSuccessSet = getGraphicSettings(notfullSuccessColor);
        }

        private OverrideGraphicSettings getGraphicSettings(Color color)
        {
            FillPatternElement fillPattern = null;

            List<Element> fillPatterns = new FilteredElementCollector(doc)
                .OfClass(typeof(FillPatternElement))
                .WhereElementIsNotElementType()
                .ToElements()
                .ToList();

            foreach (var fp in fillPatterns)
            {
                if (fp.Name.ToString().Contains("Сплошная заливка"))
                {
                    fillPattern = (FillPatternElement)fp;
                    break;
                }
            }
            if (fillPattern == null)
            {
                Print("Заливка \"Сплошная заливка\" не найдена", KPLN_Loader.Preferences.MessageType.Error);
                return null;
            }

            OverrideGraphicSettings overrideGraphic = new OverrideGraphicSettings();
#if Revit2018
            overrideGraphic.SetProjectionFillPatternId(fillPattern.Id);
            overrideGraphic.SetProjectionFillColor(color);

#endif
#if Revit2020
            overrideGraphic.SetSurfaceForegroundPatternId(fillPattern.Id);
            overrideGraphic.SetSurfaceForegroundPatternColor(color);
#endif
            overrideGraphic.SetProjectionLineColor(color);

            return overrideGraphic;
        }

        public static OverrideGraphicSettings getStandartGraphicSettings(Document doc)
        {
            OverrideGraphicSettings overrideGraphic = new OverrideGraphicSettings();
#if Revit2018
            overrideGraphic.SetProjectionFillPatternId(ElementId.InvalidElementId);
            overrideGraphic.SetProjectionFillColor(Color.InvalidColorValue);
#endif
#if Revit2020
            overrideGraphic.SetSurfaceForegroundPatternId(ElementId.InvalidElementId);
            overrideGraphic.SetSurfaceForegroundPatternColor(Color.InvalidColorValue);
#endif
            overrideGraphic.SetProjectionLineColor(Color.InvalidColorValue);

            return overrideGraphic;
        }
    }
}
