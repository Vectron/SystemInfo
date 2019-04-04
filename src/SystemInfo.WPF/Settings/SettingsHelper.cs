using System;
using System.IO;
using System.Linq;
using System.Reflection;
using VectronsLibrary.DI;

namespace SystemInfo.WPF.Settings
{
    public static class SettingsHelper
    {
        private const string FileName = "settings.json";

        public static void CreateIfNotExcist(string path)
        {
            var folder = Path.HasExtension(path) ? Path.GetDirectoryName(path) : path;
            Directory.CreateDirectory(folder);
        }

        public static string GetDataFolder()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var appdataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            if (string.IsNullOrEmpty(appdataFolder))
            {
                return Helper.AssemblyDirectory;
            }

            var fullPath = appdataFolder;

            var attributes = entryAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

            if (attributes.Length > 0)
            {
                var attribute = attributes[0] as AssemblyCompanyAttribute;
                if (!string.IsNullOrWhiteSpace(attribute.Company))
                {
                    fullPath = Path.Combine(fullPath, attribute.Company);
                }
            }

            var appName = entryAssembly.GetName().Name.Split('.').FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(appName))
            {
                fullPath = Path.Combine(fullPath, appName);
            }

            return fullPath;
        }

        public static string GetSettingsFilePath()
            => Path.Combine(GetDataFolder(), FileName);
    }
}