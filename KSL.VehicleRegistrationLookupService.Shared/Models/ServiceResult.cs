namespace KSL.VehicleRegistrationLookupService.Shared.Models
{
    public class ServiceResult<T>
    {

        public ServiceResult(ServiceCredentials serviceCredentials, T result)
        {
            ServiceCredentials = serviceCredentials;
            Result = result;
        }

        public ServiceResult(ServiceCredentials serviceCredentials, ServiceError error)
        {
            ServiceCredentials = serviceCredentials;
            Error = error;
        }

        /// <summary>
        /// The credentials used for the service request
        /// </summary>
        public ServiceCredentials ServiceCredentials { get; private set; }

        /// <summary>
        /// If an error occurred during a service request, the error details and exception if thrown
        /// </summary>
        public ServiceError Error { get; private set; }

        /// <summary>
        /// Whether an error occurred during a service request and thus no result has been obtained
        /// </summary>
        public bool IsFaulted { get { return Error != null; } }

        /// <summary>
        /// The result object
        /// </summary>
        public T Result { get; private set; }


    }
}
