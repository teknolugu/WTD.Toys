using MongoDB.Bson;
using Realms;

namespace WinToys.DataSource.Entities.Realm;

public class BrowserMap : RealmObject
{
    [PrimaryKey]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public string Path { get; set; }
    public string Url { get; set; }
    public int Status { get; set; }
}