using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.VehicleRegistrationLookupService.Shared.Interfaces
{
    public interface IVehicleLookupService
    {
        /// <summary>
        /// Lookup a vehicles information using its VRM
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <returns>The base information of a vehicle</returns>
        ServiceResult<EnhancedVehicleInformation> VrmLookup(string vehicleRegistrationMark);
        /// <summary>
        /// Lookup a vehicles information using its VRM and confirms the VIN matches the VIN held on record for the vehicle
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <param name="vehicleIdentificationNumber">The identification number of the vehicle</param>
        /// <returns></returns>
        ServiceResult<EnhancedVehicleInformation> VrmLookup(string vehicleRegistrationMark, string vehicleIdentificationNumber);

        /// <summary>
        /// Lookup a vehicles base information using its VRM
        /// </summary>
        /// <param name="vehicleRegistrationMark">The registration of the vehicle</param>
        /// <returns>The base information of a vehicle</returns>
        ServiceResult<BaseVehicleInformation> BasicVrmLookup(string vehicleRegistrationMark);
    }
}
