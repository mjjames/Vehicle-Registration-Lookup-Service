using System;
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
        public void ValidVrm()
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
            var result = service.VrmLookup("DA70XSC");

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
    }
}
