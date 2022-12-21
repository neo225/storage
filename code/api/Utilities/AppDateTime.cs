namespace I2R.Storage.Api.Utilities;

public class AppDateTime
{
    private static DateTime? dateTime;

    public static DateTime UtcNow => dateTime ?? DateTime.UtcNow;

    public static void Set(DateTime setDateTime) {
        dateTime = setDateTime;
    }

    public static void Reset() {
        dateTime = null;
    }
}