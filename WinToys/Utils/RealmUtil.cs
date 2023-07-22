using System;
using Realms;

namespace WinToys.Utils;

public static class RealmUtil
{
    public static Realm GetInstance()
    {
        var config = new RealmConfiguration(Environment.CurrentDirectory + "/data.realm")
        {
            ShouldDeleteIfMigrationNeeded = true,
            ShouldCompactOnLaunch = (bytes, used) => true
        };

        var realm = Realm.GetInstance(config);
        return realm;
    }
}