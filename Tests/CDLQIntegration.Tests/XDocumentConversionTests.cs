﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MKS.VehicleRegistrationLookupService.Shared.Models;
using Xunit;
using MKS.VehicleRegistrationLookupService.CDLQIntegration;

namespace MKS.VehicleRegistrationLookupService.Tests.CDLQIntegration
{
    public class XDocumentConversionTests
    {
        [Fact]
        public void SeatsFromBaseDataTest()
        {
            var xmlData =
                "<result id=\"3135994\" generated=\"1373281540\" mode=\"test\" account_id=\"242\"><vrm>DB70XSC</vrm><make>SEAT</make><model>IBIZA CHILL</model><vin>VSSZZZ6KZ2R019024</vin><doors>5 DOORS</doors><seats>5</seats>" +
                "<wheelplan>2 AXLE RIGID BODY</wheelplan><engine_size>1391</engine_size><engine_number>AUD123456</engine_number><fuel>PETROL</fuel><published_engine_size>1.4 LITRES</published_engine_size>" +
                "<smmt_no_of_seats_mv>0</smmt_no_of_seats_mv><smmt_wheelbase>NA</smmt_wheelbase><smmt_gross_vehicle_weight/></result>";
            var doc = XDocument.Parse(xmlData);
            var converter = new XDocumentConversions(doc);
            Assert.Equal(5, converter.EnhancedVehicleInformation().Seats);
        }

        [Fact]
        public void SeatsFromSmntDataTest()
        {
            var xmlData =
                "<result id=\"3135994\" generated=\"1373281540\" mode=\"test\" account_id=\"242\"><vrm>DB70XSC</vrm><make>SEAT</make><model>IBIZA CHILL</model><vin>VSSZZZ6KZ2R019024</vin><doors>5 DOORS</doors><seats>0</seats>" +
                "<wheelplan>2 AXLE RIGID BODY</wheelplan><engine_size>1391</engine_size><engine_number>AUD123456</engine_number><fuel>PETROL</fuel><published_engine_size>1.4 LITRES</published_engine_size>" +
                "<smmt_no_of_seats_mv>5</smmt_no_of_seats_mv><smmt_wheelbase>NA</smmt_wheelbase><smmt_gross_vehicle_weight/></result>";
            var doc = XDocument.Parse(xmlData);
            var converter = new XDocumentConversions(doc);
            Assert.Equal(5, converter.EnhancedVehicleInformation().Seats);
        }

        [Fact]
        public void FuelTypePetrolTest()
        {
            var xmlData =
               "<result id=\"3135994\" generated=\"1373281540\" mode=\"test\" account_id=\"242\"><vrm>DB70XSC</vrm><make>SEAT</make><model>IBIZA CHILL</model><vin>VSSZZZ6KZ2R019024</vin><doors>5 DOORS</doors><seats>0</seats>" +
               "<wheelplan>2 AXLE RIGID BODY</wheelplan><engine_size>1391</engine_size><engine_number>AUD123456</engine_number><fuel>PETROL</fuel><published_engine_size>1.4 LITRES</published_engine_size>" +
               "<smmt_no_of_seats_mv>5</smmt_no_of_seats_mv><smmt_wheelbase>NA</smmt_wheelbase><smmt_gross_vehicle_weight/></result>";
            var doc = XDocument.Parse(xmlData);
            var converter = new XDocumentConversions(doc);
            Assert.Equal(FuelType.Petrol, converter.EnhancedVehicleInformation().FuelType);
        }

        [Fact]
        public void FuelTypeDieselTest()
        {
            var xmlData =
               "<result id=\"3135994\" generated=\"1373281540\" mode=\"test\" account_id=\"242\"><vrm>DB70XSC</vrm><make>SEAT</make><model>IBIZA CHILL</model><vin>VSSZZZ6KZ2R019024</vin><doors>5 DOORS</doors><seats>0</seats>" +
               "<wheelplan>2 AXLE RIGID BODY</wheelplan><engine_size>1391</engine_size><engine_number>AUD123456</engine_number><fuel>HEAVY OIL</fuel><published_engine_size>1.4 LITRES</published_engine_size>" +
               "<smmt_no_of_seats_mv>5</smmt_no_of_seats_mv><smmt_wheelbase>NA</smmt_wheelbase><smmt_gross_vehicle_weight/></result>";
            var doc = XDocument.Parse(xmlData);
            var converter = new XDocumentConversions(doc);
            Assert.Equal(FuelType.Diesel, converter.EnhancedVehicleInformation().FuelType);
        }
    }
}
