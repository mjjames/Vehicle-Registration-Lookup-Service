using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class VehicleTyreInformation : BaseVehicleInformation, IEquatable<VehicleTyreInformation>
    {
        public VehicleTyreInformation()
        {
            TyreInformation = new List<TyreInformation>();
        }

        public bool Equals(VehicleTyreInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return base.Equals(other) && Enumerable.SequenceEqual(TyreInformation, other.TyreInformation);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode()*397) ^ (TyreInformation != null ? TyreInformation.GetHashCode() : 0);
            }
        }

        public ICollection<TyreInformation> TyreInformation { get; set; }
        public override bool Equals(object obj)
        {

            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VehicleTyreInformation)obj);
        }
    }
}

