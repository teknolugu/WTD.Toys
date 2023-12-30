using System;
using System.IO;
using Config.Net;
using WinToys.Utils;

namespace WinToys.Models;

public static class EnvVar
{
    public static readonly string? ExePath = Environment.ProcessPath;

    public static readonly string AppDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinTenDev", "WinToys");
    public static readonly string PathFileDb = Path.Combine(AppDataDir, "Data", "FileDB").MkDir();
    public static readonly string PathSqliteDb = Path.Combine(AppDataDir, "Data", "Sqlite.db").MkDir();

    public static IAppSettings AppSettings => new ConfigurationBuilder<IAppSettings>()
        .UseJsonFile("Config.json")
        .Build();
}