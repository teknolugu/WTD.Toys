using System.Collections.Generic;

namespace WinToys.Models;

public interface IAppSettings
{
    public List<BrowserSwitch> BrowserMaps { get; set; }
}

public class BrowserSwitch
{
    public string Path { get; set; }
    public string Urls { get; set; }
}