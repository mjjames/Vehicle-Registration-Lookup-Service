using System;

namespace MKS.VehicleRegistrationLookupService.Shared.Models
{
    public class ServiceCredentials
    {
        /// <summary>
        /// URL to the service end point
        /// </summary>
        public Uri ServiceEndPoint { get; set; }

        /// <summary>
        /// Username to pass to the service for authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Authentication key / password for service
        /// </summary>
        public string AuthenticationKey { get; set; }

        /// <summary>
        /// Whether the service should be in test mode  
        /// </summary>
        public bool IsInTestMode { get; set; }
    }
}
