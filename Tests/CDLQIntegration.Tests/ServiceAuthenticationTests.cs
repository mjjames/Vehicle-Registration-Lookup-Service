using System;
using System.Threading.Tasks;
using MKS.VehicleRegistrationLookupService.CDLQIntegration;
using MKS.VehicleRegistrationLookupService.Shared.Models;
using Xunit;

namespace MKS.VehicleRegistrationLookupService.Tests.CDLQIntegration
{
    public class ServiceAuthenticationTests
    {
        private const string AuthenticationKey = "JUKEKDBK";
        private const string Username = "kslgarageservices8898";
        private readonly Uri _endpoint = new Uri("https://www.cdlvis.com/lookup/getxml");


        [Fact]
        public async Task ValidEndpointUsernameAndAuthenticationKey()
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
            //ensure we aren't faulted
            Assert.False(result.IsFaulted);
        }

        [Fact]
        public async Task ValidEndpointUsernameAndInvalidAuthenticationKey()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                AuthenticationKey = "GHGHJGFHG",
                IsInTestMode = true,
                ServiceEndPoint = _endpoint,
                Username = Username
            };
            //create the service
            var service = new VrmService(credentials);
            //make a request for a valid test reg
            var result = await service.VrmLookup("DA70XSC");
            //ensure we aren't faulted
            Assert.True(result.IsFaulted);
        }

        [Fact]
        public async Task ValidEndpointAndInvalidUsernameAndAuthenticationKey()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                AuthenticationKey = "GHGHJGJ",
                IsInTestMode = true,
                ServiceEndPoint = _endpoint,
                Username = "meh7899"
            };
            //create the service
            var service = new VrmService(credentials);
            //make a request for a valid test reg
            var result = await service.VrmLookup("DA70XSC");
            //ensure we aren't faulted
            Assert.True(result.IsFaulted);
        }

        [Fact]
        public void InvalidEndpoint()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                AuthenticationKey = AuthenticationKey,
                IsInTestMode = true,
                ServiceEndPoint = new Uri("http://helloworld.net"),
                Username = Username
            };
            //create the service
            var service = new VrmService(credentials);
            //make a request for a valid test reg
            var result = service.VrmLookup("DA70XSC");
            //ensure we aren't faulted
            Assert.True(result.IsFaulted);
        }

    }
}
