namespace KSL.VehicleRegistrationLookupService.Shared.Models
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
            var comparison = (BaseVehicleInformation)obj;
            return      string.Equals(Make, comparison.Make)
                    &&  string.Equals(Model, comparison.Model)
                    &&  Equals(EngineSize, comparison.EngineSize);
        }

    }
}
