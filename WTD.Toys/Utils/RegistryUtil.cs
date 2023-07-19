using Microsoft.Win32;

namespace WTD.Toys.Utils;

public static class RegistryUtil
{
    public static void SetDefaultBrowser(string appName, string appPath, string appId, string appDescription)
    {
        var software = "SOFTWARE\\" + appName;
        var startMenuInternet = @"SOFTWARE\Clients\StartMenuInternet\" + appName;
        var capabilities = startMenuInternet + "\\Capabilities";
        var fileAssociations = capabilities + "\\FileAssociations";
        var startMenu = capabilities + "\\Startmenu";
        var urlAssociation = capabilities + "\\URLAssociations";

        Registry.LocalMachine.CreateSubKey(startMenuInternet).SetValue("", appName);
        Registry.LocalMachine.CreateSubKey(startMenuInternet).SetValue("LocalizedString", "@" + appPath);
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationDescription", appDescription);
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationIcon", appPath + ",0");
        Registry.LocalMachine.CreateSubKey(capabilities).SetValue("ApplicationName", appName);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".htm", appId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".html", appId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".shtml", appId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".xhtml", appId);
        Registry.LocalMachine.CreateSubKey(fileAssociations).SetValue(".xht", appId);
        Registry.LocalMachine.CreateSubKey(startMenu).SetValue("StartMenuInternet", appName);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("ftp", appId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("http", appId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("https", appId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("mailto", appId);
        Registry.LocalMachine.CreateSubKey(urlAssociation).SetValue("webcal", appId);
        Registry.LocalMachine.CreateSubKey(startMenuInternet + "\\DefaultIcon").SetValue("", appPath + ",0");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + "\\InstallInfo").SetValue("", "");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\shell\open\command").SetValue("", "\"" + appPath + "\"");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + "\\DefaultIcon").SetValue("", appPath + ",0");
        Registry.LocalMachine.CreateSubKey(startMenuInternet + @"\shell\open\command").SetValue("", appPath);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\.htm\OpenWithProgIds").SetValue(appId, "");
        Registry.LocalMachine.CreateSubKey(software + "\\Capabilities").SetValue("ApplicationDescription", appDescription);
        Registry.LocalMachine.CreateSubKey(software + "\\Capabilities").SetValue("ApplicationName", appId);
        Registry.LocalMachine.CreateSubKey(software + "\\Capabilities").SetValue("ApplicationIcon", appPath);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".htm", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".html", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".shtml", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".xhtml", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\FileAssociations").SetValue(".xht", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\Startmenu").SetValue("StartmenuInternet", appPath);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("ftp", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("http", appId);
        Registry.LocalMachine.CreateSubKey(software + @"\Capabilities\UrlAssociations").SetValue("https", appId);
        Registry.LocalMachine.CreateSubKey("SOFTWARE\\RegisteredApplications").SetValue(appName, software + "\\Capabilities");
        Registry.LocalMachine.CreateSubKey("SOFTWARE\\RegisteredApplications").SetValue(appName, capabilities);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId).SetValue("FriendlyTypeName", appName);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId).SetValue("Editflags", 2);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId + "\\DefaultIcon").SetValue("", appPath + ",0");
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId + @"\shell\open\command").SetValue("", "\"" + appPath + "\"%1");
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId).SetValue("FriendlyTypeName", appName);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId).SetValue("Editflags", 2);
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId + "\\DefaultIcon").SetValue("", appPath + ",0");
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Classes\" + appId + @"\shell\open\command").SetValue("", "\"" + appPath + "\"%1");
        Registry.LocalMachine.CreateSubKey(@"SOFTWARE\MDK\" + appId + "\\Capabilities").SetValue("ApplicationName", appName);

        Registry.CurrentUser.CreateSubKey("SOFTWARE\\RegisteredApplications").SetValue(appName, software + "\\Capabilities");
        Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + appName + ".exe").SetValue("Path", appPath);

        Registry.ClassesRoot.CreateSubKey(appId + "\\DefaultIcon").SetValue("", appPath + ",0");
        Registry.ClassesRoot.CreateSubKey(appId + @"\shell\open\command").SetValue("", "\"" + appPath + "\"%1");
        Registry.ClassesRoot.CreateSubKey(appId).SetValue("AppUserModelId", appName);
        Registry.ClassesRoot.CreateSubKey(appId + "\\Application").SetValue("ApplicationDescription", appDescription);
        Registry.ClassesRoot.CreateSubKey(appId + "\\Application").SetValue("ApplicationName", appName);
        Registry.ClassesRoot.CreateSubKey(appId + "\\Application").SetValue("ApplicationIcon", appPath);
    }

    public static void UnRegisterBrowser(string appName, string appId)
    {
        var software = "SOFTWARE\\" + appName;
        var classes = @"SOFTWARE\Classes\" + appId;
        var startMenuInternet = @"SOFTWARE\Clients\StartMenuInternet\" + appName;
        var capabilities = startMenuInternet + "\\Capabilities";
        var fileAssociations = capabilities + "\\FileAssociations";
        var startMenu = capabilities + "\\Startmenu";
        var urlAssociations = capabilities + "\\URLAssociations";

        Registry.LocalMachine.DeleteSubKeyTree(startMenuInternet);
        Registry.LocalMachine.DeleteSubKeyTree(classes);
        // Registry.ClassesRoot.DeleteSubKeyTree(appId);

        // Registry.LocalMachine.OpenSubKey(fileAssociations)?.DeleteValue(".htm");
        // Registry.LocalMachine.OpenSubKey(fileAssociations)?.DeleteValue(".html");
        // Registry.LocalMachine.OpenSubKey(fileAssociations)?.DeleteValue(".shtml");
        // Registry.LocalMachine.OpenSubKey(fileAssociations)?.DeleteValue(".xhtml");
        // Registry.LocalMachine.OpenSubKey(fileAssociations)?.DeleteValue(".xht");
    }
}