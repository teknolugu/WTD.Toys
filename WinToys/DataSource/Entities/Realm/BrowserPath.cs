using Realms;

namespace WinToys.DataSource.Entities.Realm;

public class BrowserPath : RealmObject
{
    [PrimaryKey]
    public string Path { get; set; }

    public int Status { get; set; }
}