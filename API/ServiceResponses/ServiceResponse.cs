namespace API.ServiceResponses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Description { get; set; }
        public bool StatusCode { get; set; }
    }
}
