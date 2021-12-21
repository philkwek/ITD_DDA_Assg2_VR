public class PlayerSessionTime
{
    public string dateOfSession;
    public string dayOfSession;
    public string timeOfSession;
    public float timeSpendMin;
    public string titleSessionTime;

    public PlayerSessionTime(string dateOfSession, string dayOfSession, string timeOfSession, float timeSpendMin, string titleSessionTime)
    {
        this.dateOfSession = dateOfSession;
        this.dayOfSession = dayOfSession;
        this.timeOfSession = timeOfSession;
        this.timeSpendMin = timeSpendMin;
        this.titleSessionTime = titleSessionTime;
    }
}
