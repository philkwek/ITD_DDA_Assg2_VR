public class MinigameStats
{
    public string accuracyPercentage;
    public int highscore;
    public float longestRoundMinutes;
    public int longestThrowStreak;
    public int totalHits;
    public int totalMiss;
    public int totalThrows;

    public MinigameStats(string accuracyPercentage, int highscore, float longestRoundMinutes,
        int longestThrowStreak, int totalHits, int totalMiss, int totalThrows)
    {
        this.accuracyPercentage = accuracyPercentage;
        this.highscore = highscore;
        this.longestRoundMinutes = longestRoundMinutes;
        this.longestThrowStreak = longestThrowStreak;
        this.totalHits = totalHits;
        this.totalMiss = totalMiss;
        this.totalThrows = totalThrows;
    }

}
