public class PlayerProfileData
{
    public int completion;
    public int noOfMinigamesCompleted;
    public int noOfTaskCompleted;
    public float totalTimePlayed;
    public string username;
    public int minigameHighscore;

    public PlayerProfileData(int completion, int noOfMinigamesCompleted, int noOfTaskCompleted,
        float totalTimePlayed, string username, int minigameHighscore)
    {
        this.completion = completion;
        this.noOfMinigamesCompleted = noOfMinigamesCompleted;
        this.noOfTaskCompleted = noOfTaskCompleted;
        this.totalTimePlayed = totalTimePlayed;
        this.username = username;
        this.minigameHighscore = minigameHighscore;
    }
}
