public class PlayerGameData
{
    public object minigameStats;
    public int noOfCraftsMade;
    public int totalObjPicked;

    public PlayerGameData(object minigameStats, int noOfCraftsMade, int totalObjPicked)
    {
        this.minigameStats = minigameStats;
        this.noOfCraftsMade = noOfCraftsMade;
        this.totalObjPicked = totalObjPicked;
    }
}
