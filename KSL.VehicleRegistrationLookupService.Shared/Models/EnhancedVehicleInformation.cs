namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class EnhancedVehicleInformation : BaseVehicleInformation
    {
        protected bool Equals(EnhancedVehicleInformation other)
        {
            return base.Equals(other) && string.Equals(Vin, other.Vin) && Seats == other.Seats && FuelType == other.FuelType && string.Equals(Doors, other.Doors) && string.Equals(EngineNumber, other.EngineNumber) && string.Equals(Wheelplan, other.Wheelplan);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode*397) ^ (Vin != null ? Vin.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Seats;
                hashCode = (hashCode*397) ^ (int) FuelType;
                hashCode = (hashCode*397) ^ (Doors != null ? Doors.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (EngineNumber != null ? EngineNumber.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Wheelplan != null ? Wheelplan.GetHashCode() : 0);
                return hashCode;
            }
        }

        public string Vin { get; set; }
        public int Seats { get; set; }
        public FuelType FuelType { get; set; }
        public string Doors { get; set; }
        public string EngineNumber { get; set; }
        public string Wheelplan { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EnhancedVehicleInformation) obj);
        }
    }
}
