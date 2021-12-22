public class PlayerGameData
{
    public object minigameStats;
    public int noOfCraftsMade;
    public int totalObjPicked;
    public string username;

    public PlayerGameData(object minigameStats, int noOfCraftsMade, int totalObjPicked, string username)
    {
        this.minigameStats = minigameStats;
        this.noOfCraftsMade = noOfCraftsMade;
        this.totalObjPicked = totalObjPicked;
        this.username = username;
    }
}
