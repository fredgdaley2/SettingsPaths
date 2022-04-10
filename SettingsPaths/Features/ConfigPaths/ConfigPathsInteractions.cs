using Microsoft.Extensions.Configuration;
namespace Features;
internal static class ConfigPathsInteractions
{
    public static List<ConfigPaths> GetConfigPathsFromAppSettings(string appSettingsPath)
    {
        var builder = new ConfigurationBuilder().AddJsonFile(appSettingsPath).Build();
        var configElements = builder.GetChildren();
        var cnfgPaths = new List<ConfigPaths>();
        GetAllChildrenPaths(configElements, cnfgPaths);
        return cnfgPaths;
    }

    public static void GetAllChildrenPaths(IEnumerable<IConfigurationSection> sections, List<ConfigPaths> cnfgPaths)
    {
        foreach (var child in sections)
        {
            if (child.Value != null)
            {
                cnfgPaths.Add(new ConfigPaths { Path = child.Path, Value = child.Value });
            }
            GetAllChildrenPaths(child.GetChildren(), cnfgPaths);
        }
    }

}
