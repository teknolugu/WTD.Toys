using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using Serilog;
using WinToys.Models;

namespace WinToys.Utils;

public static class RegistryUtil
{
    private const string AppName = "WinToys Browser Switcher";
    private const string AppId = "WinToys.BrowserSwitcher";
    private const string AppDescription = "Let Toys help select right browser for you.";

    public static void IntegrateBrowserSwitcher(bool isRegister)
    {
        Log.Information("Checking privilege..");

        if (ProcessUtil.IsAdministrator())
        {
            if (isRegister)
                SetDefaultBrowser();
            // RegisterBrowser2();
            else
                UnRegisterBrowser();
        }
        else
        {
            ProcessUtil.RestartAppAsAdmin(!isRegister ? "remove-browser-map" : "add-browser-map");
        }
    }

    public static void ReadArgs(List<string> args)
    {
        var payload = args.FirstOrDefault();

        if (payload.Contains("add"))
            RegisterBrowser2();
        else
            UnRegisterBrowser();

        Application.Current.Shutdown(0);
    }

    private static void SetDefaultBrowser()
    {
        Log.Information("Registering WinToys for HTTP handler..");

        var software = "SOFTWARE\\" + AppName;
        var startMenuInternet = @"SOFTWARE\Clients\StartMenuInternet\" + AppName;
        var capabilities = startMenuInternet + "\\Capabilities";
        var fileAssociations = capabilities + "\\FileAssociations";
        var startMenu = capabilities + "\\Startmenu";
        var urlAssociation = capabilities + "\\URLAssociations";

        Registry.LocalMachine.CreateSubKey(startMenuInternet).SetValue("", AppName);
        Registry.LocalMachine.CreateSubKey(startMenuInternet).SetValue("LocalizedString", EnvVar.ExePath);
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationDescription", AppDescription);
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationIcon", EnvVar.ExePath + ",0");
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationName", AppName);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".htm", AppId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".html", AppId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".shtml", AppId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".xhtml", AppId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".xht", AppId);
        Registry.LocalMachine.CreateSubKey(startMenu).SetValue("StartMenuInternet", AppName);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("ftp", AppId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("http", AppId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("https", AppId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("mailto", AppId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("webcal", AppId);
        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\DefaultIcon").SetValue("", EnvVar.ExePath + ",0");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\InstallInfo").SetValue("", "");

        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\shell\open\command")
            .SetValue("", $"\"{EnvVar.ExePath}\"");

        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\DefaultIcon").SetValue("", EnvVar.ExePath + ",0");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\shell\open\command").SetValue("", EnvVar.ExePath);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\.htm\OpenWithProgIds").SetValue(AppId, "");

        Registry.LocalMachine.CreateSubKey(software + "\\Capabilities")
            .SetValue("ApplicationDescription", AppDescription);

        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities").SetValue("ApplicationName", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities").SetValue("ApplicationIcon", EnvVar.ExePath);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".htm", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".html", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".shtml", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".xhtml", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".xht", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("ftp", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("http", AppName);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("https", AppName);

        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\Startmenu")
            .SetValue("StartmenuInternet", EnvVar.ExePath);

        Registry.LocalMachine.CreateSubKey("SOFTWARE\\RegisteredApplications")
            .SetValue(AppName, $"{software}\\Capabilities");

        Registry.LocalMachine.CreateSubKey("SOFTWARE\\RegisteredApplications").SetValue(AppName, capabilities);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId).SetValue("FriendlyTypeName", AppName);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId).SetValue("Editflags", 2);

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId + "\\DefaultIcon")
            .SetValue("", $"{EnvVar.ExePath},0");

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId + @"\shell\open\command")
            .SetValue("", $"{EnvVar.ExePath} %1");

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId).SetValue("FriendlyTypeName", AppName);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId).SetValue("Editflags", 2);

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId + "\\DefaultIcon")
            .SetValue("", $"{EnvVar.ExePath},0");

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + AppId + @"\shell\open\command")
            .SetValue("", $"{EnvVar.ExePath} %1");

        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\MDK\" + AppId + "\\Capabilities")
            .SetValue("ApplicationName", AppName);

        Registry.CurrentUser.CreateSubKey("SOFTWARE\\RegisteredApplications")
            .SetValue(AppName, software + "\\Capabilities");

        Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + AppName + ".exe")
            .SetValue("Path", EnvVar.ExePath);

        Registry.ClassesRoot.CreateSubKey(AppId).SetValue("AppUserModelId", AppName);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\DefaultIcon").SetValue("", $"{EnvVar.ExePath},0");
        Registry.ClassesRoot.CreateSubKey(AppId + @"\shell\open\command").SetValue("", $"{EnvVar.ExePath} %1");
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationDescription", AppDescription);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationName", AppName);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationIcon", EnvVar.ExePath);

        Log.Information("Register BrowserMap completed successfully..");
    }

    private static void RegisterBrowser2()
    {
        Registry.ClassesRoot.CreateSubKey(AppId).SetValue("AppUserModelId", AppName);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\DefaultIcon").SetValue("", EnvVar.ExePath + ",0");
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationDescription", AppDescription);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationName", AppName);
        Registry.ClassesRoot.CreateSubKey(AppId + "\\Application").SetValue("ApplicationIcon", EnvVar.ExePath + ",0");
        Registry.ClassesRoot.CreateSubKey(AppId + "\\shell\\open\\command").SetValue("", $"{EnvVar.ExePath} %1");
    }

    private static void UnRegisterBrowser()
    {
        Log.Information("Unregistering WinToys from Http handler..");

        var software = "SOFTWARE\\" + AppName;
        var classes = @"SOFTWARE\Classes\" + AppId;
        var startMenuInternet = @"SOFTWARE\Clients\StartMenuInternet\" + AppName;
        var capabilities = startMenuInternet + "\\Capabilities";
        var fileAssociations = capabilities + "\\FileAssociations";
        var startMenu = capabilities + "\\Startmenu";
        var urlAssociations = capabilities + "\\URLAssociations";

        Registry.LocalMachine.DeleteSubKeyTree(startMenuInternet);
        Registry.LocalMachine.DeleteSubKeyTree(classes);
    }
}