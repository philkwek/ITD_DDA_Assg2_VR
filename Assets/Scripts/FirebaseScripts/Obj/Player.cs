public class Player
{
    public bool admin;
    public string email;
    public string userID;
    public string username;

    public Player(bool admin, string email, string userID, string username)
    {
        this.admin = admin;
        this.email = email;
        this.userID = userID;
        this.username = username;
    }
}
