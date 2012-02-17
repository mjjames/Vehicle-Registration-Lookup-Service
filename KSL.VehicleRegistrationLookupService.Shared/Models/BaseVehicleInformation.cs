namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class BaseVehicleInformation
    {
        /// <summary>
        /// Make of the vehicle
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// Model of the vehicle
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Size of the vehicles engine, in cubic centimetres
        /// </summary>
        public int EngineSize { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (BaseVehicleInformation)) return false;
            return Equals((BaseVehicleInformation) obj);
        }

        public bool Equals(BaseVehicleInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Make, Make) && Equals(other.Model, Model) && other.EngineSize == EngineSize;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Make != null ? Make.GetHashCode() : 0);
                result = (result*397) ^ (Model != null ? Model.GetHashCode() : 0);
                result = (result*397) ^ EngineSize;
                return result;
            }
        }
    }
}
