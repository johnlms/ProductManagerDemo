namespace ProductManager.Exceptions
{
    // Primary Exception class for data access specific errors
    public class ProductDataException : Exception
    {
        public ProductDataException() { }
        public ProductDataException(string message) : base(message) { }
        public ProductDataException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
