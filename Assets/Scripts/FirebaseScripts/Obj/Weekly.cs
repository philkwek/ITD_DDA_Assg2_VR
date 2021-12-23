public class Weekly
{
    public object Sunday;
    public object Monday;
    public object Tuesday;
    public object Wednesday;
    public object Thursday;
    public object Friday;
    public object Saturday;
    public int weekNumber;

    public Weekly(object Sunday, object Monday, object Tuesday, object Wednesday, object Thursday, object Friday, object Saturday, int weekNumber)
    {
        this.Sunday = Sunday;
        this.Monday = Monday;
        this.Tuesday = Tuesday;
        this.Wednesday = Wednesday;
        this.Thursday = Thursday;
        this.Friday = Friday;
        this.Saturday = Saturday;
        this.weekNumber = weekNumber;
    }
}
