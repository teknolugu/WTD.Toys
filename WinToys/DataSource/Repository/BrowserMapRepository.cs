using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinToys.DataSource.Entities.EF;
using WinToys.Models.Enums;
using WinToys.Utils;

namespace WinToys.DataSource.Repository;

public class BrowserMapRepository
{
    private readonly EfDbContext _efDbContext;

    public BrowserMapRepository(EfDbContext efDbContext)
    {
        _efDbContext = efDbContext;
    }

    public static void SaveBrowserList(IEnumerable<BrowserPathEntity> data)
    {
    }

    public async Task FeedBrowserMap()
    {
        var allBrowsers = await PathUtil.FindInstalledBrowser();

        var insert = allBrowsers.Select(x => new BrowserPathEntity()
        {
            Path = x.ExecutablePath,
            Status = EventStatus.Completed
        }).ToList();

        _efDbContext.BrowserPath.RemoveRange(_efDbContext.BrowserPath.ToList());
        await _efDbContext.SaveChangesAsync();

        _efDbContext.BrowserPath.AddRange(insert);
        await _efDbContext.SaveChangesAsync();
    }

    public void PrepareEditBrowserMap(string data)
    {
        var browserPath = _efDbContext.BrowserPath.Single(entity => entity.Path == data);

        browserPath.Status = EventStatus.InProgress;

        _efDbContext.SaveChanges();
    }


    public async Task<IEnumerable<string>> GetBrowsers()
    {
        var browsers = await _efDbContext.BrowserPath.ToListAsync();

        return browsers.Select(entity => entity.Path);
    }

    public string GetInProgressBrowser()
    {
        var browserPath = _efDbContext.BrowserPath.Single(map => map.Status == EventStatus.InProgress);

        return browserPath.Path;
    }

    public List<BrowserMapEntity> GetActiveBrowserMap()
    {
        var inProgress = GetInProgressBrowser();

        var activeBrowserPaths = _efDbContext.BrowserMap
            .Where(path => path.Path == inProgress)
            .ToList();

        return activeBrowserPaths;
    }

    public void DeleteMapById(string id)
    {
        var find = _efDbContext.BrowserMap.FirstOrDefault(entity => entity.Id == id);

        if (find == null) return;

        _efDbContext.BrowserMap.Remove(find);
        _efDbContext.SaveChanges();
    }

    public void SaveUrl(BrowserMapEntity data)
    {
        _efDbContext.BrowserMap.Add(data);
        _efDbContext.SaveChanges();
    }
}