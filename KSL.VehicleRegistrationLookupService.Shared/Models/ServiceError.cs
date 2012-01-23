using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KSL.VehicleRegistrationLookupService.Shared.Models
{
    public class ServiceError
    {
        /// <summary>
        /// The exception thrown by the service error - if thrown
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The error message returned by the service - if any
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The error number returned by the service - if any
        /// </summary>
        public int Number { get; set; }
    }
}
