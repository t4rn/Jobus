namespace Jobus.Common.Results
{
    public class GenericResult<T> : Result
    {
        public T Data { get; set; }
    }
}
