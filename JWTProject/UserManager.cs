namespace JWTProject
{
    public class UserManager
    {
        public static readonly IDictionary<string, User> Users = new Dictionary<string, User>
        {
            { "admin",new User{ UserId = 1 ,UserCode = "admin",Password="pw2" ,GroupName="admin2"} },
            { "user",new User{ UserId = 2 ,UserCode = "user",Password="pw1" ,GroupName="user"} }
        };
    }
}
