namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface to define the invalid Output Port.
    /// </summary>
    public interface IOutputPortInvalid
    {
        /// <summary>
        /// Informs the resource was invalid.
        /// </summary>
        /// <param name="message">Text description.</param>
        void Invalid(string message);
    }
}
