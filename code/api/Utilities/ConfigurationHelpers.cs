namespace I2R.Storage.Api.Utilities;

public static class ConfigurationHelpers
{
    public static string GetAppDbConnectionString(this IConfiguration configuration) {
        return configuration.GetValue<string>(AppEnvVariables.APPDB_CONNECTION_STRING);
    }
}