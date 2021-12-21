public class PlayerProfileData
{
    public int completion;
    public int noOfMinigamesCompleted;
    public int noOfTaskCompleted;
    public float totalTimePlayed;

    public PlayerProfileData(int completion, int noOfMinigamesCompleted, int noOfTaskCompleted, float totalTimePlayed)
    {
        this.completion = completion;
        this.noOfMinigamesCompleted = noOfMinigamesCompleted;
        this.noOfTaskCompleted = noOfTaskCompleted;
        this.totalTimePlayed = totalTimePlayed;
    }
}