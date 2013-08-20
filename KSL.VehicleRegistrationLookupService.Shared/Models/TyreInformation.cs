using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class TyreInformation : IEquatable<TyreInformation>
    {
        public bool Equals(TyreInformation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Size, other.Size) && Equals(Pressure, other.Pressure) && Equals(LadenPressure, other.LadenPressure) && NutTorque == other.NutTorque && string.Equals(RimSize, other.RimSize) && RimOffset == other.RimOffset && LoadIndex == other.LoadIndex && SpeedIndex == other.SpeedIndex;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Size != null ? Size.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Pressure != null ? Pressure.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (LadenPressure != null ? LadenPressure.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ NutTorque;
                hashCode = (hashCode*397) ^ (RimSize != null ? RimSize.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ RimOffset;
                hashCode = (hashCode*397) ^ LoadIndex;
                hashCode = (hashCode*397) ^ SpeedIndex.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(TyreInformation left, TyreInformation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TyreInformation left, TyreInformation right)
        {
            return !Equals(left, right);
        }

        public TyreSize Size { get; set; }
        public TyrePressure Pressure { get; set; }
        public TyrePressure LadenPressure { get; set; }
        public int NutTorque { get; set; }
        public string RimSize { get; set; }
        public int RimOffset { get; set; }
        public int LoadIndex { get; set; }
        public char SpeedIndex { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TyreInformation) obj);
        }
    }
}
