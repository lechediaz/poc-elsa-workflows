namespace FactoryApp.Services.Base
{
    public class ServiceResult
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T Extras { get; set; }
    }
}