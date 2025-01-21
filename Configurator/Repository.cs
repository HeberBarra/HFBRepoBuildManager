namespace RepoBuildManager.Configurator;

public class Repository(string name, string url, string currentCommit, bool disabled)
{
    public string Name { get; set; } = name;
    public string Url { get; set; } = url;
    public string CurrentCommit { get; set; } = currentCommit;
    public bool Disabled { get; set; } = disabled;
}