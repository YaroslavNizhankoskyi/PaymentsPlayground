namespace PaymentsPlayground.Interfaces
{
    public interface ICredentials<T>
    {
        public T Value { get; set; }
    }

    public interface ICredentials
    {

    }
}
