namespace I2R.Storage.Api.Enums;

public enum EUserRole
{
    LEAST_PRIVILEGED = 0,
    ADMIN = 1,
}

public static class UserRole
{
    public static EUserRole FromString(string role) => role switch {
        "least_privileged" => EUserRole.LEAST_PRIVILEGED,
        "admin" => EUserRole.ADMIN,
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };

    public static string ToString(EUserRole role) => role switch {
        EUserRole.LEAST_PRIVILEGED => "least_privileged",
        EUserRole.ADMIN => "admin",
        _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
}