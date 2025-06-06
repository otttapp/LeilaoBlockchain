namespace Cuca_Api.Infraestructure.Exceptions
{
    public class BusinessException : Exception
    {
        public string Identifier { get; private set; }
        public string Description { get; private set; }
        public object Details { get; private set; }

        public BusinessException(string code, string message) : base(message)
        {
            Identifier = code;
        }

        public BusinessException(string code, string message, object details) : this(code, message)
        {
            Details = details;
        }

        public BusinessException(string code, string description, string message, object details) : this(code, message, details)
        {
            Description = description;
        }

        public BusinessException(string? message) : base(message)
        {
        }
    }
}
