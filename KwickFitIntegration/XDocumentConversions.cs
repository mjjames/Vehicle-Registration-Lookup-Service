using System.Linq;
using System.Xml.Linq;
using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.KwickFitIntegration
{
    public class XDocumentConversions
    {
        public static BaseVehicleInformation BaseVehicleInformation(XDocument xDocument)
        {
            return new BaseVehicleInformation
                {
                    EngineSize = int.Parse(GetValue(xDocument, "ENGINECAPACITY")),
                    Make = GetValue(xDocument, "MAKE"),
                    Model = GetValue(xDocument, "MODEL")
                };
        }

        private static string GetValue(XDocument xDocument, string element)
        {
            return xDocument.Descendants(element).First().Value.Trim();
        }

        public static TyreInformation TyreInformation(XElement element)
        {
            var sizeSegments = element.Element("TYRESIZE").Value.Split('/');
            var profileSegments = sizeSegments[1].Split('R');
            var tyreSize = new TyreSize
                {
                    Width = int.Parse(sizeSegments[0]),
                    Size = int.Parse(profileSegments[1]),
                    Profile = int.Parse(profileSegments[0])
                };

            var ladenPressure = GetLadenPressure(element);
            var tyrePressure = GetTyrePressure(element);

            return new TyreInformation
            {
                LadenPressure = ladenPressure,
                LoadIndex = int.Parse(element.Element("LOADINDEX").Value),
                NutTorque = int.Parse(element.Element("NUTBOLTTORQUE").Value),
                Pressure = tyrePressure,
                RimOffset = int.Parse(element.Element("RIMOFFSET").Value),
                RimSize = element.Element("RIMSIZE").Value,
                Size = tyreSize,
                SpeedIndex = element.Element("SPEEDINDEX").Value.First()
            };
        }

        private static TyrePressure GetTyrePressure(XElement element)
        {
            TyrePressure tyrePressure;
            if (element.Element("TYREPRESSURE") != null && element.Element("TYREPRESSUREREAR") != null)
            {
                float frontPressure, rearPressure;
                float.TryParse(element.Element("TYREPRESSURE").Value, out frontPressure);
                float.TryParse(element.Element("TYREPRESSUREREAR").Value, out rearPressure);
                tyrePressure = new TyrePressure
                {
                    Front = frontPressure,
                    Rear = rearPressure
                };
            }
            else
            {
                tyrePressure = new TyrePressure();
            }
            return tyrePressure;
        }

        private static TyrePressure GetLadenPressure(XElement element)
        {
            TyrePressure ladenPressure;
            if (element.Element("LADENTPFRONT") != null && element.Element("LADENTPREAR") != null)
            {
                float frontPressure, rearPressure;
                float.TryParse(element.Element("LADENTPFRONT").Value, out frontPressure);
                float.TryParse(element.Element("LADENTPREAR").Value, out rearPressure);
                ladenPressure = new TyrePressure
                {
                    Front = frontPressure,
                    Rear = rearPressure
                };
            }
            else
            {
                ladenPressure = new TyrePressure();
            }
            return ladenPressure;
        }

        public static VehicleTyreInformation VehicleTyreInformation(XDocument xDocument)
        {
            var baseInfo = BaseVehicleInformation(xDocument);
            var tyreInfo = new VehicleTyreInformation
                {
                    EngineSize = baseInfo.EngineSize,
                    Make = baseInfo.Make,
                    Model = baseInfo.Model,
                    TyreInformation = xDocument.Descendants("DRDITEMS").Select(TyreInformation).ToList()
                };

            return tyreInfo;

        }
    }
}
