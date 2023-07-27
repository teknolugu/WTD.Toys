using System;
using System.IO;
using Config.Net;

namespace WinToys.Models;

public static class EnvVar
{
    public static readonly string ExePath = Environment.ProcessPath;

    public static string AppDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "WinTenDev", "WinToys");

    public static IAppSettings AppSettings => new ConfigurationBuilder<IAppSettings>()
        .UseJsonFile("Config.json")
        .Build();
}