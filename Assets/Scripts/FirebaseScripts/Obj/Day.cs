public class Day
{
    public string[] currentlyActive;
    public string date;
    public float totalPlaySession;
    public string[] wasActive;

    public Day(string[] currentlyActive, string date, float totalPlaySession, string[] wasActive)
    {
        this.currentlyActive = currentlyActive;
        this.date = date;
        this.totalPlaySession = totalPlaySession;
        this.wasActive = wasActive;
    }

}
