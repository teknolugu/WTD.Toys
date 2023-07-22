using System;
using Config.Net;

namespace WinToys.Models;

public static class EnvVar
{
    public static readonly string ExePath = Environment.ProcessPath;

    public static IAppSettings AppSettings => new ConfigurationBuilder<IAppSettings>()
        .UseJsonFile("Config.json")
        .Build();
}