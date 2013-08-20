using System;

namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class TyrePressure : IEquatable<TyrePressure>
    {
        public bool Equals(TyrePressure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Front.Equals(other.Front) && Rear.Equals(other.Rear);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Front.GetHashCode()*397) ^ Rear.GetHashCode();
            }
        }

        public static bool operator ==(TyrePressure left, TyrePressure right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TyrePressure left, TyrePressure right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Pressure in BAR
        /// </summary>
        public float Front { get; set; }
        /// <summary>
        /// Pressure in BAR
        /// </summary>
        public float Rear { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TyrePressure) obj);
        }
    }
}