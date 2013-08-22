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
            return new TyreInformation
            {
                LadenPressure = new TyrePressure
                    {
                        Front = float.Parse(element.Element("LADENTPFRONT").Value),
                        Rear = float.Parse(element.Element("LADENTPREAR").Value)
                    },
                    LoadIndex = int.Parse(element.Element("LOADINDEX").Value),
                    NutTorque = int.Parse(element.Element("NUTBOLTTORQUE").Value),
                    Pressure = new TyrePressure
                        {
                            Front = float.Parse(element.Element("TYREPRESSURE").Value),
                            Rear = float.Parse(element.Element("TYREPRESSUREREAR").Value)
                        },
                        RimOffset = int.Parse(element.Element("RIMOFFSET").Value),
                        RimSize = element.Element("RIMSIZE").Value,
                        Size = tyreSize,
                        SpeedIndex = element.Element("SPEEDINDEX").Value.First()

            };
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
