namespace ProductManager.Exceptions
{
    // Primary Exception class for application service specific errors
    public class ProductServiceException : Exception
    {
        public ProductServiceException() { }
        public ProductServiceException(string message) : base(message) { }
        public ProductServiceException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
