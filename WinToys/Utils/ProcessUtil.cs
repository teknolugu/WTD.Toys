using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows;
using Serilog;
using WinToys.DataSource.Repository;
using WinToys.Models;

namespace WinToys.Utils;

public static class ProcessUtil
{
    public static void DirectBrowserSwitch()
    {
        Log.Information("Starting BrowserSwitch..");
        var args = Environment.GetCommandLineArgs().Skip(1);
        var url = args.LastOrDefault("http");
        Log.Debug("Param Url: {Url}", url);

        var select = BrowserSwitchRepository.SelectBrowserMap(url);

        if (select == null)
        {
            Log.Information("BrowserMap not found");
            Application.Current.Shutdown(0);
            return;
        }

        Log.Information("Starting Browser: {Path}", select.Path);
        Process.Start(select.Path, url);
        Application.Current.Shutdown(0);
    }

    public static bool IsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);

        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    public static Process RestartAppAsAdmin(string args = "")
    {
        var proc = new Process()
        {
            StartInfo =
            {
                FileName = EnvVar.ExePath,
                Arguments = args,
                UseShellExecute = true,
                Verb = "runas"
            }
        };

        proc.Start();

        return proc;
    }
}