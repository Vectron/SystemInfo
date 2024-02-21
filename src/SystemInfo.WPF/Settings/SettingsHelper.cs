using System.IO;
using System.Reflection;
using VectronsLibrary.DI;

namespace SystemInfo.WPF.Settings;

/// <summary>
/// Helper class for managing settings.
/// </summary>
public static class SettingsHelper
{
    private const string FileName = "settings.json";

    /// <summary>
    /// Create the settings file if it does not exist.
    /// </summary>
    /// <param name="path">The path to create.</param>
    public static void CreateIfNotExist(string path)
    {
        var folder = Path.HasExtension(path) ? Path.GetDirectoryName(path) : path;
        if (folder == null)
        {
            return;
        }

        _ = Directory.CreateDirectory(folder);
    }

    /// <summary>
    /// Get the application data folder.
    /// </summary>
    /// <returns>The application data folder.</returns>
    public static string GetDataFolder()
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        if (string.IsNullOrEmpty(appDataFolder))
        {
            return Helper.AssemblyDirectory;
        }

        var fullPath = appDataFolder;
        if (entryAssembly == null)
        {
            return fullPath;
        }

        var attributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), inherit: false);
        if (attributes.Length > 0)
        {
            if (attributes[0] is AssemblyCompanyAttribute attribute
                && !string.IsNullOrWhiteSpace(attribute.Company))
            {
                fullPath = Path.Combine(fullPath, attribute.Company);
            }
        }

        var appName = entryAssembly.GetName().Name?.Split('.').FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(appName))
        {
            fullPath = Path.Combine(fullPath, appName);
        }

        return fullPath;
    }

    /// <summary>
    /// Get the full path to the settings file.
    /// </summary>
    /// <returns>The full settings file path.</returns>
    public static string GetSettingsFilePath()
        => Path.Combine(GetDataFolder(), FileName);
}
