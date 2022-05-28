namespace PaymentsPlayground.Interfaces
{
    public interface IUserValidatorService
    {
        bool UserExists(string email);

        bool HasSameEmailAsCurrentUser(string email);
    }
}
