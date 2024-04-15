namespace Shared.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }

        public Response() {}

        public Response(string message) 
        {
            Status = "200";
            Message = message;
        }
    }
    public class Response<T>
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }

        public Response(string message, T data)
        {
            Status = "200";
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Status = "200";
            Message = message;
            Data = default(T);
        }

        public Response(string message, T data, string status)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
