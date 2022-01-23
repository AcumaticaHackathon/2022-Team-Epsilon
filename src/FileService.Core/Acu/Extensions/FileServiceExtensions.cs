
using System;
using FileService.Core;
using FileService.Acu.DAC;

namespace FileService.Acu.Extensions
{
    public static class FileServiceExtensions
    {
        public static IExternalFileServiceProvider GetProvider(this FileServicePreferences preferences)
        {
            if (string.IsNullOrWhiteSpace(preferences.PluginType))
                throw new InvalidOperationException("No data fetcher type specified");

            var type = Type.GetType($"{preferences.PluginType}, FileService");
            if (type is null)
                throw new InvalidOperationException("Type not found");

            var fetcher = Activator.CreateInstance(type) as IExternalFileServiceProvider;
            return fetcher;
        }

    }
}