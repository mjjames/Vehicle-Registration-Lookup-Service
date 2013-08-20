using System;

namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class TyreSize : IEquatable<TyreSize>
    {
        public bool Equals(TyreSize other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Width == other.Width && Profile == other.Profile && Size == other.Size;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Width;
                hashCode = (hashCode*397) ^ Profile;
                hashCode = (hashCode*397) ^ Size;
                return hashCode;
            }
        }

        public static bool operator ==(TyreSize left, TyreSize right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TyreSize left, TyreSize right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Tyre Width
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Tyre Profile
        /// </summary>
        public int Profile { get; set; }
        /// <summary>
        /// Rim Diameter
        /// </summary>
        public int Size { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TyreSize) obj);
        }
    }
}