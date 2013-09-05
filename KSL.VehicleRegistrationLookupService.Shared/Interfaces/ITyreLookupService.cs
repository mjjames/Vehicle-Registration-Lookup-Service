using System.Threading.Tasks;
using MKS.VehicleRegistrationLookupService.Shared.Models;

namespace MKS.VehicleRegistrationLookupService.Shared.Interfaces
{
    public interface ITyreLookupService
    {
        Task<ServiceResult<BaseVehicleInformation>> GetBaseInfo(string vrm);
        Task<ServiceResult<VehicleTyreInformation>>  GetTyreInfo(string vrm);
    }
}