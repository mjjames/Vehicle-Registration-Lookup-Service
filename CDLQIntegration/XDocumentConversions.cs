using System.Linq;
using System.Xml.Linq;
using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.VehicleRegistrationLookupService.CDLQIntegration
{
    internal class XDocumentConversions
    {
        /// <summary>
        /// Document to use for all conversions
        /// </summary>
        private readonly XDocument _document;

        internal XDocumentConversions(XDocument document)
        {
            _document = document;
        }

        /// <summary>
        /// Checks the Vehicle Information XDocument to see if its a request that has errored, if it has a error object is returned, otherwise null is returned
        /// </summary>
        /// <returns>ServiceError if XDocument contains error, otherwise null indicating no error was recieved</returns>
        internal ServiceError ErrorDetails()
        {
            //check for valid response
            if (_document == null || _document.Root == null)
            {
                return new ServiceError
                {
                    Message = "Invalid response returned by the service",
                    Number = 0
                };
            }
            //see if our root element 'result' contains the error attribute, if it doesn't there is no error here
            if (_document.Root.Attributes().All(a => a.Name != "error"))
            {
                return null;
            }

            //create a service error object with the error details on the root node
            return new ServiceError
            {
                Message = _document.Root.Attribute("message").Value,
                Number = int.Parse(_document.Root.Attribute("error").Value)
            };
        }

        /// <summary>
        /// Converts the Vehicle Information XDocument to a base vehicle information object
        /// </summary>
        /// <returns></returns>
        public BaseVehicleInformation BasicVehicleInformation()
        {
            //generate vehicle informations from the result data
            var info = from result in _document.Descendants("result")
                       select new BaseVehicleInformation
                                  {
                                      EngineSize = int.Parse(result.Element("engine_size").Value),
                                      Make = result.Element("make").Value,
                                      Model = result.Element("model").Value
                                  };
            //we should only ever have one result so use single
            //this will throw if we some how get multiple
            return info.Single();
        }

        public EnhancedVehicleInformation EnhancedVehicleInformation()
        {
            //generate vehicle informations from the result data
            var info = from result in _document.Descendants("result")
                       select new EnhancedVehicleInformation()
                       {
                           EngineSize = int.Parse(result.Element("engine_size").Value),
                           Make = result.Element("make").Value,
                           Model = result.Element("model").Value,
                           Seats = int.Parse(result.Element("seats").Value == "0"? result.Element("smmt_no_of_seats_mv").Value : result.Element("seats").Value),
                           Doors = result.Element("doors").Value,
                           EngineNumber = result.Element("engine_number").Value,
                           Vin = result.Element("vin").Value,
                           Wheelplan = result.Element("wheelplan").Value,
                           FuelType = GetFuelType(result.Element("fuel").Value)
                       };
            //we should only ever have one result so use single
            //this will throw if we some how get multiple
            return info.Single();
        }

        private FuelType GetFuelType(string value)
        {
            switch (value.ToUpper())
            {
                case "PETROL":
                    return FuelType.Petrol;
                case "HEAVY OIL":
                    return FuelType.Diesel;
                case "ELECTRICITY":
                    return FuelType.Electric;
                case "STEAM":
                    return FuelType.Steam;
                case "GAS":
                    return FuelType.Lpg;
                case "PETROL/GAS":
                    return FuelType.PetrolLpg;
                case "GAS BI-FUEL":
                    return FuelType.GasBiFuel;
                case "HYBRID ELECTRIC":
                    return FuelType.HybridElectric;
                case "GAS DIESEL":
                    return FuelType.GasDiesel;
                case "FUEL CELLS":
                    return FuelType.FuelCell;
                case "ELECTRIC DIESEL":
                    return FuelType.ElectricDiesel;
                default:
                    return FuelType.Other;
            }
        }
    }
}
