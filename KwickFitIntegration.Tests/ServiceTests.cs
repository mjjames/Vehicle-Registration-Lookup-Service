using System;
using MKS.KwickFitIntegration;
using MKS.VehicleRegistrationLookupService.Shared.Models;
using Xunit;

namespace KwickFitIntegration.Tests
{
    public class ServiceTests
    {

        [Fact]
        public async void MakeModelParsed()
        {
            var expected = new BaseVehicleInformation
            {
                EngineSize = 1910,
                Make = "FIAT",
                Model = "BRAVO"
            };
            var service = new TyreLookupService(new ServiceCredentials
                {
                    ServiceEndPoint = new Uri("http://www.kwik-fit.com/ajax/tyre-pressure-search/tyre-pressure-data.asp")
                });
            var baseInfo = await service.GetBaseInfo("MT57XAJ");
            Assert.False(baseInfo.IsFaulted);
            Assert.Equal(expected, baseInfo.Result);
        }


        [Fact]
        public async void FullResponseParsed()
        {
            var expected = new VehicleTyreInformation
            {
                EngineSize = 1910,
                Make = "FIAT",
                Model = "BRAVO",
                TyreInformation =
                        {
                           new TyreInformation
                {
                    LadenPressure = new TyrePressure
                        {
                            Front = 2.6f,
                            Rear = 2.6f
                        },
                        LoadIndex = 91,
                        NutTorque = 90,
                        Pressure = new TyrePressure
                            {
                                Front = 2.3f,
                                Rear = 2.3f
                            },
                            RimOffset = 31,
                            RimSize = "7x16",
                            Size = new TyreSize
                                {
                                    Size = 16,
                                    Profile = 55,
                                    Width = 205
                                },
                                SpeedIndex = 'V'
                },
                
                new TyreInformation
                {
                    LadenPressure = new TyrePressure
                        {
                            Front = 2.6f,
                            Rear = 2.6f
                        },
                        LoadIndex = 91,
                        NutTorque = 90,
                        Pressure = new TyrePressure
                            {
                                Front = 2.3f,
                                Rear = 2.3f
                            },
                            RimOffset = 31,
                            RimSize = "7x17",
                            Size = new TyreSize
                                {
                                    Size = 17,
                                    Profile = 45,
                                    Width = 225
                                },
                                SpeedIndex = 'V'
                },
                                new TyreInformation
                {
                    LadenPressure = new TyrePressure
                        {
                            Front = 2.9f,
                            Rear = 2.9f
                        },
                        LoadIndex = 92,
                        NutTorque = 90,
                        Pressure = new TyrePressure
                            {
                                Front = 2.6f,
                                Rear = 2.6f
                            },
                            RimOffset = 35,
                            RimSize = "7.5x18",
                            Size = new TyreSize
                                {
                                    Size = 18,
                                    Profile = 40,
                                    Width = 225
                                },
                                SpeedIndex = 'V'
                },
                        }
            };
            var service = new TyreLookupService(new ServiceCredentials
            {
                ServiceEndPoint = new Uri("http://www.kwik-fit.com/ajax/tyre-pressure-search/tyre-pressure-data.asp")
            });
            var tyreInfo = await  service.GetTyreInfo("MT57XAJ");
            Assert.False(tyreInfo.IsFaulted);
            Assert.Equal(expected, tyreInfo.Result);
        }

        [Fact]
        public async void InvalidEndpointGetBaseInfo()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                ServiceEndPoint = new Uri("http://helloworld.net"),
            };
            //create the service
            var service = new TyreLookupService(credentials);
            //make a request for a valid test reg
            var result = await service.GetBaseInfo("DA70XSC");
            //ensure we aren't faulted
            Assert.True(result.IsFaulted);
        }


        [Fact]
        public async void InvalidEndpointGetTyreInfo()
        {
            //create the credentials
            var credentials = new ServiceCredentials
            {
                ServiceEndPoint = new Uri("http://helloworld.net"),
            };
            //create the service
            var service = new TyreLookupService(credentials);
            //make a request for a valid test reg
            var result = await service.GetTyreInfo("DA70XSC");
            //ensure we aren't faulted
            Assert.True(result.IsFaulted);
        }
    }
}
