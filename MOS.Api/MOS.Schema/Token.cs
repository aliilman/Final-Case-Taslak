
namespace MOS.Schema;

public class TokenRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class TokenResponse
{
    public DateTime ExpireDate { get; set; }
    public string Token { get; set; }
    public string Role { get; set; }

    //public int PersonalNumber{ get; set; }
}