namespace API.ServiceResponses
{
    public class ServiceResponseSum<T>
    {
        public T Data { get; set; }
        public string Description { get;set; }
        public bool StatusCode { get; set; }
        public int TotalSum { get; set; }
    }
}
