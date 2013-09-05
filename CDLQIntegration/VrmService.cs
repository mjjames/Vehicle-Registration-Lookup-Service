using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using MKS.VehicleRegistrationLookupService.Shared.Interfaces;
using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.VehicleRegistrationLookupService.CDLQIntegration
{
    public class VrmService : IVehicleLookupService
    {
        private readonly ServiceCredentials _serviceCredentials;

        public VrmService(ServiceCredentials serviceCredentials)
        {
            //TODO: validation
            _serviceCredentials = serviceCredentials;

        }

        private Task<XDocument> RequestVehicleInformation(string vehicleRegistrationMark, string vehicleIdentificationNumber)
        {
            //TODO: validation
            //generate a new web client and build the querystring to send with it
            var client = new WebClient { QueryString = GenerateQuerystring(vehicleRegistrationMark, vehicleIdentificationNumber) };
            //fire up a new task which downloads the result from the webclient and parses it as an xmldocument
            //return the task obejct
            return Task<XDocument>.Factory.StartNew(() => XDocument.Parse(client.DownloadString(_serviceCredentials.ServiceEndPoint)));

        }



        private NameValueCollection GenerateQuerystring(string vehicleRegistrationMark, string vehicleIdentificationNumber)
        {
            var querystringCollection = new NameValueCollection
                                            {
                                                {"username", _serviceCredentials.Username.ToLower()},
                                                {"key", _serviceCredentials.AuthenticationKey.ToUpper()},
                                                {"vrm", vehicleRegistrationMark},
                                                {"mode", _serviceCredentials.IsInTestMode ? "test" : "live"}
                                            };

            if (!String.IsNullOrWhiteSpace(vehicleIdentificationNumber))
            {
                querystringCollection.Add("vin", vehicleIdentificationNumber);
            }
            return querystringCollection;
        }

        #region Implementation of IVehicleLookupService

        /// <summary>
        /// Lookup a vehicles base information using its VRM
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <returns>The base information of a vehicle</returns>
        public Task<ServiceResult<EnhancedVehicleInformation>> VrmLookup(string vehicleRegistrationMark)
        {
            return VrmLookup(vehicleRegistrationMark, string.Empty);
        }

        /// <summary>
        /// Lookup a vehicles base information using its VRM
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <returns>The base information of a vehicle</returns>
        public async Task<ServiceResult<BaseVehicleInformation>> BasicVrmLookup(string vehicleRegistrationMark)
        {
            //request the data from the service
            try
            {
                var serviceResult = await RequestVehicleInformation(vehicleRegistrationMark, string.Empty);
                return RetrieveServiceResult<BaseVehicleInformation>(serviceResult);
            }
            catch (Exception ex)
            {
                return new ServiceResult<BaseVehicleInformation>(_serviceCredentials,
                                                                new ServiceError { Exception = ex });
            }

        }

        /// <summary>
        /// Lookup a vehicles base information using its VRM and confirms the VIN matches the VIN held on record for the vehicle
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <param name="vehicleIdentificationNumber">The identification number of the vehicle</param>
        /// <returns></returns>
        public async Task<ServiceResult<EnhancedVehicleInformation>> VrmLookup(string vehicleRegistrationMark, string vehicleIdentificationNumber)
        {
            try
            {
                //request the data from the service
                var serviceResult = await RequestVehicleInformation(vehicleRegistrationMark, vehicleIdentificationNumber);
                return RetrieveServiceResult<EnhancedVehicleInformation>(serviceResult);
            }
            catch (Exception ex)
            {
                return new ServiceResult<EnhancedVehicleInformation>(_serviceCredentials,
                                                                new ServiceError { Exception = ex });
            }
        }

        /// <summary>
        /// Takes a completed task and builds the service result from the task result
        /// </summary>
        /// <param name="serviceResult">Complete task returning an xdocument</param>
        /// <returns></returns>
        private ServiceResult<T> RetrieveServiceResult<T>(XDocument document) where T : BaseVehicleInformation
        {
            var conversion = new XDocumentConversions(document);

            //see if our service returned an error
            var error = conversion.ErrorDetails();
            //if it did return the service result with the error
            if (error != null)
            {
                return new ServiceResult<T>(_serviceCredentials, error);
            }

            //we try catch incase the xml is invalid
            try
            {
                //otherwise go convert the xml into the vehicle information object and return the service result
                if (typeof(T) == typeof(EnhancedVehicleInformation))
                {
                    return new ServiceResult<T>(_serviceCredentials,
                                                                 conversion.EnhancedVehicleInformation() as T);
                }
                return new ServiceResult<T>(_serviceCredentials, conversion.BasicVehicleInformation() as T);

            }
            catch (Exception e)
            {
                return new ServiceResult<T>(_serviceCredentials, new ServiceError { Exception = e });
            }
        }

        #endregion
    }
}
