#region #Copyright
//  ----------------------------------------------------------------------------------
//   COPYRIGHT (c) 2022 CONTOU CONSULTING
//   ALL RIGHTS RESERVED
//   AUTHOR: Kyle Vanderstoep
//   CREATED DATE: 2022/01/22
// ----------------------------------------------------------------------------------
#endregion

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

            var type = Type.GetType(preferences.PluginType + $", {GetAssembly(preferences.PluginType)}");
            if (type is null)
                throw new InvalidOperationException("Type not found");

            var fetcher = Activator.CreateInstance(type) as IExternalFileServiceProvider;
            return fetcher;
        }

        private static string GetAssembly(string fullyQualified)
        {
            if (string.IsNullOrWhiteSpace(fullyQualified)) return "";
            string[] namespaces = fullyQualified.Split('.');

            string name = "";
            for (int i = 0; i < 4; i++)
            {
                name += namespaces[i] + ".";
            }

            name = name.TrimEnd('.');
            return name;
        }
    }
}