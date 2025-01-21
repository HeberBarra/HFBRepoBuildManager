using RepoBuildManager.Configurator;

Configurator configurator = new("configuration.json");
configurator.Read();

Repository newRepo = new("act", "https://github.com/nektos/act", "origin", false);
configurator.RegisterRepository(newRepo);

Repository[]? repositories = configurator.GetRepositories();

if (repositories == null)
{
    Console.WriteLine("Oops! Something went wrong");
    return;
}

foreach (Repository repository in repositories)
{
    Console.WriteLine(repository.Name);
    Console.WriteLine(repository.Url);
    Console.WriteLine(repository.CurrentCommit);
    Console.WriteLine(repository.Disabled);
}

configurator.Save();
