using System.Text.RegularExpressions;

namespace Quality.Storage.Api.Utilities;

public static class FileValidators
{
    private static Regex _fileNameRegex => new(@"([^\\/]+)$");
    private static Regex _folderNameRegex => new(@"^(\w+\.?)*\w+$");
    public static bool IsValidFileName(this string value) => _fileNameRegex.IsMatch(value);
    public static bool IsValidFolderName(this string value) => _folderNameRegex.IsMatch(value);
}