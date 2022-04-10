using Features;
const string APP_SETTINGS = "appsettings.json";
var appSettingsFilePath = "";

try
{
    if (HasConsoleArguments(args))
    {
        appSettingsFilePath = args[0];
    }
    else
    {
        appSettingsFilePath = ExecutingPathHasAppsettingsFile(APP_SETTINGS) ?
            GetAppsettingsPathFromExecutingFolder(APP_SETTINGS)
            : PromptForAppsettingsPath();
    }

    if (AppsettingsFilePathNullOrEmpty(appSettingsFilePath)) return;

    var (filePath, fileName) = GetUserSettingsFIleParts(appSettingsFilePath);

    DisplayFilePathToUser(filePath);

    if (!FilePathExists(filePath)) return;

    fileName ??= APP_SETTINGS;

    DisplayFileNameToUser(fileName);

    if (!FileIsJson(fileName)) return;

    if (!SettingsFileExists(filePath, fileName)) return;

    DisplayProcessingStatusToUser();

#pragma warning disable CS8604 // Possible null reference argument.
    List<ConfigPaths> newCnfgPaths = ConfigPathsInteractions.GetConfigPathsFromAppSettings(Path.Combine(filePath, fileName));
#pragma warning restore CS8604 // Possible null reference argument.

    WriteOutSettingsPaths(filePath, newCnfgPaths);
}
catch (Exception ex)
{
    ConsoleInteractions.DisplayDangerMessage("Oh No! something unexpected occurred.");
    ConsoleInteractions.DisplayDangerMessage(ex.Message);
}



static (string? filePath, string? fileName) GetUserSettingsFIleParts(string userPath)
{
    var fileName = Path.HasExtension(userPath) ? Path.GetFileName(userPath) : null;
    var filePath = Path.HasExtension(userPath) ? Path.GetDirectoryName(userPath) : Path.GetFullPath(userPath);

    return (filePath, fileName);
}

static string WrapValueInQuotesIfContainsComma(string pathValue)
{
    if (pathValue.Contains(',')) return '"' + pathValue + '"';
    return pathValue;
}

static void WriteOutSettingsPaths(string? filePath, List<ConfigPaths> newCnfgPaths)
{
    const string OUTPUT_HEADER = "VARIABLE NAME, VALUE";
    const string OUTPUT_FILE_NAME = "settingsPaths.csv";
#pragma warning disable CS8604 // Possible null reference argument.
    var outputFilePathAndName = Path.Combine(filePath, OUTPUT_FILE_NAME);
#pragma warning restore CS8604 // Possible null reference argument.
    using var sw = new StreamWriter(outputFilePathAndName);
    sw.WriteLine(OUTPUT_HEADER);
    newCnfgPaths.ToList().ForEach(
        path => sw.WriteLine($"{path.Path},{WrapValueInQuotesIfContainsComma(path.Value)}")
        );
    ConsoleInteractions.DisplaySuccessMessage($"File written to {outputFilePathAndName}");
}

static bool HasConsoleArguments(string[] args)
{
    return args.Length > 0;
}

static bool ExecutingPathHasAppsettingsFile(string appSettingsFilePath)
{
    return File.Exists(appSettingsFilePath);
}

static string PromptForAppsettingsPath()
{
    string? appSettingsFilePath = ConsoleEx.ReadLine<string>("Enter path to an [settings].json file:");
    Console.WriteLine();
    return appSettingsFilePath;
}

string GetAppsettingsPathFromExecutingFolder(string appSettingsFilePath)
{
    return Path.Combine(Directory.GetCurrentDirectory(), appSettingsFilePath);
}

static bool AppsettingsFilePathNullOrEmpty(string? appSettingsFilePath)
{
    if (string.IsNullOrEmpty(appSettingsFilePath))
    {
        ConsoleInteractions.DisplayDangerMessage("Invalid File Path!");
        ConsoleInteractions.DisplayDangerMessage(appSettingsFilePath ?? string.Empty);
        return true;
    }
    return false;
}

bool FilePathExists(string? filePath)
{
    if (filePath == null || !Directory.Exists(filePath))
    {
        ConsoleInteractions.DisplayDangerMessage("File Path Does Not Exist!");
        return false;
    }
    return true;
}

static bool FileIsJson(string? fileName)
{

    if (fileName == null || Path.GetExtension(fileName).ToLower() != ".json")
    {
        ConsoleInteractions.DisplayDangerMessage("File Is Not A JSON File!");
        return false;
    }
    return true;
}

static bool SettingsFileExists(string? filePath, string fileName)
{
    if (filePath == null || !File.Exists(Path.Combine(filePath, fileName)))
    {
        ConsoleInteractions.DisplayDangerMessage("Settings File Does Not Exist!");
        return false;
    }
    return true;
}

static void DisplayFilePathToUser(string? filePath)
{
    ConsoleInteractions.DisplaySuccessMessage("FilePath: " + filePath);
}

static void DisplayFileNameToUser(string? fileName)
{
    ConsoleInteractions.DisplaySuccessMessage("FileName: " + fileName);
}

static void DisplayProcessingStatusToUser()
{
    ConsoleInteractions.DisplaySuccessMessage("Processing Settings File...");
}