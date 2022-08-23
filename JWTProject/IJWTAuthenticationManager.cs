namespace JWTProject
{
    //Middleware'da authentication işlemlerini yönetecek olan class'ın implement edeceği interface
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string password);
    }

}
