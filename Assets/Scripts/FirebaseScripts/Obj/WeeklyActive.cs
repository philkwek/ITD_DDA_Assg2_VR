public class WeeklyActive
{
    public string currentlyActive; // array
    public string date;
    public float totalPlaySession;
    public string wasActive;

    public WeeklyActive(string currentlyActive, string date, float totalPlaySession, string wasActive)
    {
        this.currentlyActive = currentlyActive;
        this.date = date;
        this.totalPlaySession = totalPlaySession;
        this.wasActive = wasActive;
    }
}
