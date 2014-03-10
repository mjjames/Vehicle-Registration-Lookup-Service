using System.Xml.Linq;
using MKS.KwickFitIntegration;
using MKS.VehicleRegistrationLookupService.Shared.Models;
using Xunit;

namespace KwickFitIntegration.Tests
{
    public class XDocumentConversionTests
    {

        [Fact]
        public void MakeModelParsed()
        {
            var expected = new BaseVehicleInformation
                {
                    EngineSize = 1910,
                    Make = "FIAT",
                    Model = "BRAVO"
                };
            var xDocument = XDocument.Parse(Resources.ValidResponseTestData);
            var baseInfo = XDocumentConversions.BaseVehicleInformation(xDocument);
            Assert.Equal(expected, baseInfo);
        }

        [Fact]
        public void SingleTyreParsed()
        {
            var expected = new TyreInformation
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
                };
            var element = XDocument.Parse(Resources.ValidSingleTyre).Root;
            var tyre = XDocumentConversions.TyreInformation(element);
            Assert.Equal(expected, tyre);
        }

        [Fact]
        public void SingleTyreWithoutPressureInformationParsed()
        {
            var expected = new TyreInformation
            {
                LadenPressure = new TyrePressure
                {
                    Front = 0,
                    Rear = 0
                },
                LoadIndex = 104,
                NutTorque = 160,
                Pressure = new TyrePressure
                {
                    Front = 0,
                    Rear = 0
                },
                RimOffset = 50,
                RimSize = "5.5x16",
                Size = new TyreSize
                {
                    Size = 16,
                    Profile = 70,
                    Width = 205
                },
                SpeedIndex = 'S'
            };
            var element = XDocument.Parse(Resources.ValidSingleTyreNoPressures).Root;
            var tyre = XDocumentConversions.TyreInformation(element);
            Assert.Equal(expected, tyre);
        }

        [Fact]
        public void FullResponseParsed()
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
            var xDocument = XDocument.Parse(Resources.ValidResponseTestData);
            var tyreInfo = XDocumentConversions.VehicleTyreInformation(xDocument);
            Assert.Equal(expected, tyreInfo);
        }

        [Fact]
        public void HandleInvalidTyreInformationRequest()
        {
            //our data is real data where the tyresize is missing the profile value
            //we therefore have to skip the tyre during parse
            var document = XDocument.Parse(Resources.dk51jypRequest);
            Assert.NotNull(document);
            var vehicle = XDocumentConversions.VehicleTyreInformation(document);
            Assert.NotNull(vehicle);
            Assert.Equal(1, vehicle.TyreInformation.Count);
        }
    }
}
