using System;
using System.Threading.Tasks;
using MKS.VehicleRegistrationLookupService.CDLQIntegration;
using MKS.VehicleRegistrationLookupService.Shared.Models;
using Xunit;

namespace MKS.VehicleRegistrationLookupService.Tests.CDLQIntegration
{
    public class RetrieveInformationUsingVRMOnlyTests
    {
        private const string AuthenticationKey = "JUKEKDBK";
        private const string Username = "kslgarageservices8898";
        private readonly Uri _endpoint = new Uri("https://www.cdlvis.com/lookup/getxml");


        [Fact]
        public async Task BasicValidVrm()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                AuthenticationKey = AuthenticationKey,
                IsInTestMode = true,
                ServiceEndPoint = _endpoint,
                Username = Username
            };
            //create the service
            var service = new VrmService(credentials);
            //make a request for a valid test reg
            var result = await service.BasicVrmLookup("DA70XSC");

            var expected = new BaseVehicleInformation
                               {
                                   Make = "FORD",
                                   Model = "FOCUS ZETEC CLIMATE 116",
                                   EngineSize = 1596
                               };

            //ensure we aren't faulted
            Assert.False(result.IsFaulted);
            Assert.Equal(expected, result.Result);
        }

        [Fact]
        public async Task ValidVrm()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                AuthenticationKey = AuthenticationKey,
                IsInTestMode = true,
                ServiceEndPoint = _endpoint,
                Username = Username
            };
            //create the service
            var service = new VrmService(credentials);
            //make a request for a valid test reg
            var result = await service.VrmLookup("DA70XSC");

            var expected = new EnhancedVehicleInformation
            {
                Make = "FORD",
                Model = "FOCUS ZETEC CLIMATE 116",
                EngineSize = 1596,
                Vin = "A22322C2F9D5CC939",
                Doors = "5 DOORS",
                Seats = 5,
                EngineNumber = "86DAF26",
                FuelType = FuelType.Petrol,
                Wheelplan = "2 AXLE RIGID BODY"
            };

            //ensure we aren't faulted
            Assert.False(result.IsFaulted);
            Assert.Equal(expected, result.Result);
        }

        
    }
}
