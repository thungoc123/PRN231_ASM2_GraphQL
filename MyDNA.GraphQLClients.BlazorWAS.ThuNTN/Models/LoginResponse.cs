namespace MyDNA.GraphQLClients.BlazorWAS.ThuNTN.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }

    public class LoginResponseWithUserAccount
    {
        public LoginResponse login { get; set; }
    }

}