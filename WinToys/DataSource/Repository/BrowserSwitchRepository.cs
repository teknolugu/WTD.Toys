using System.Collections.Generic;
using System.Linq;
using WinToys.DataSource.Entities.Realm;
using WinToys.Models.Enums;
using WinToys.Utils;

namespace WinToys.DataSource.Repository;

internal static class BrowserSwitchRepository
{
    public static void SaveBrowserList(IEnumerable<BrowserPath> data)
    {
        var realm = RealmUtil.GetInstance();

        realm.Write(() =>
        {
            realm.Add(data, true);
        });
    }

    public static void SaveBrowserMap(BrowserPath data)
    {
        var realm = RealmUtil.GetInstance();

        realm.Write(() =>
        {
            realm.Add(data, true);
        });
    }

    public static BrowserPath? GetInProgressBrowser()
    {
        var browserPath = RealmUtil.GetInstance().All<BrowserPath>()
            .FirstOrDefault(map => map.Status == (int)EventStatus.InProgress);

        return browserPath;
    }

    public static IEnumerable<BrowserMap> GetActiveBrowserMap()
    {
        var inProgress = GetInProgressBrowser();

        var activeBrowserPaths = RealmUtil.GetInstance()
            .All<BrowserMap>()
            .Where(path => path.Path == inProgress.Path)
            .ToList();

        return activeBrowserPaths;
    }

    public static void SaveUrl(BrowserMap data)
    {
        var realm = RealmUtil.GetInstance();

        realm.Write(() =>
        {
            realm.Add(data);
        });
    }
}