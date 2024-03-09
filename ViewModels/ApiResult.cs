namespace ViewModels
{
    public class APIResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
        public int StatusCode { get; set; }
    }
}
