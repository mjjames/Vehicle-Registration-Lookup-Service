using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using MKS.VehicleRegistrationLookupService.Shared.Interfaces;
using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.KwickFitIntegration
{
    public class TyreLookupService : ITyreLookupService
    {
        
        private readonly ServiceCredentials _serviceCredentials;

        public TyreLookupService(ServiceCredentials serviceCredentials)
        {
            //TODO: validation
            _serviceCredentials = serviceCredentials;

        }
        public async Task<ServiceResult<BaseVehicleInformation>> GetBaseInfo(string vrm)
        {
            //http://www.kwik-fit.com/ajax/tyre-pressure-search/tyre-pressure-data.asp?vnp=Y1KSL&unc=11372110113
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(_serviceCredentials.ServiceEndPoint + "?vnp=" + vrm);
                    var xDoc = XDocument.Parse(response);
                    return new ServiceResult<BaseVehicleInformation>(_serviceCredentials, XDocumentConversions.BaseVehicleInformation(xDoc));
                }
            }
            catch(Exception ex)
            {
                return new ServiceResult<BaseVehicleInformation>(_serviceCredentials, new ServiceError
                    {
                        Exception = ex,
                    });
            }
        }
        
        public async Task<ServiceResult<VehicleTyreInformation>>  GetTyreInfo(string vrm)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetStringAsync(_serviceCredentials.ServiceEndPoint + "?vnp=" + vrm);
                    var xDoc = XDocument.Parse(response);
                    return new ServiceResult<VehicleTyreInformation>(_serviceCredentials, XDocumentConversions.VehicleTyreInformation(xDoc));
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<VehicleTyreInformation>(_serviceCredentials, new ServiceError
                {
                    Exception = ex,
                });
            }
        }
    }
}
