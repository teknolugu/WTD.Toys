using System.IO;
using Realms;
using WinToys.Models;

namespace WinToys.Utils;

public static class RealmUtil
{
    public static Realm GetInstance()
    {
        var dbFile = Path.Combine(EnvVar.AppDataDir, "Data/data.realm");

        Directory.CreateDirectory(Path.GetDirectoryName(dbFile));

        var config = new RealmConfiguration(dbFile)
        {
            ShouldDeleteIfMigrationNeeded = true,
            ShouldCompactOnLaunch = (bytes, used) => true
        };

        var realm = Realm.GetInstance(config);
        return realm;
    }
}