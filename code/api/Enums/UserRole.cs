namespace Quality.Storage.Api.Enums;

public enum UserRole
{
	LEAST_PRIVILEGED = 0,
	ADMIN = 1,
}

public static class UserRoleHelper
{
	public static UserRole FromString(string role) => role switch {
			"least_privileged" => UserRole.LEAST_PRIVILEGED,
			"admin" => UserRole.ADMIN,
			_ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
	};

	public static string ToString(UserRole role) => role switch {
			UserRole.LEAST_PRIVILEGED => "least_privileged",
			UserRole.ADMIN => "admin",
			_ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
	};
}
