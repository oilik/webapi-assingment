namespace Assingment.TokenManager
{
    public interface IJwtTokenManager
    {
        string Authenticate(string userName, string password);
    }
}
