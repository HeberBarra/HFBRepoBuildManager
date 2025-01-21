using Newtonsoft.Json;
using RepoBuildManager.Configurator.Exceptions;

namespace RepoBuildManager.Configurator;

public class Configurator(string filepath)
{

    private Configuration? _configuration;
    private readonly Reader _reader = new Reader();

    public void Read()
    {
        _configuration = _reader.readFile(filepath) ?? throw new FileLoadException();
    }

    public void RegisterRepository(Repository repository)
    {
        if (_configuration?.Repositories == null)
        {
            throw new RepositoryRegisterException("Couldn't register repository because configuration is null");
        }

        Repository[] repositories = _configuration.Repositories;
        if (repositories.Any(repo =>
            {
                if (repo.Url == repository.Url)
                    return true;

                if (_configuration.SemanticNames is true or null)
                {
                    return false;
                }
                
                return repo.Name == repository.Name;
            }))
        {
            throw new RepositoryAlreadyRegisteredException();
        }

        _configuration.Repositories = repositories.ToList().Append(repository).ToArray();
    }

    public void UpdateRepository(Repository repository, string repositoryName)
    {
        if (_configuration?.Repositories == null)
        {
            throw new RepositoryUpdateException("Couldn't update repository because configuration is null");
        }

        for (int i = 0; i < _configuration.Repositories.Length; i++)
        {
            if (_configuration.Repositories[i].Name != repositoryName) continue;
            _configuration.Repositories[i] = repository;
            return;
        }

        throw new RepositoryUpdateException("Couldn't find specified repository");
    }
    
    public void DeleteRepository(Repository repository)
    {
        if (_configuration?.Repositories == null)
        {
            throw new RepositoryDeleteException("Couldn't delete repository because configuration is null");
        }

        List<Repository> newRepositories = [];
        foreach (Repository repo in _configuration.Repositories)
        {
            if (repo.Url == repository.Url)
            {
                Console.WriteLine("Successfully delete repository");
                continue;
            };

            if (_configuration.SemanticNames is false && repo.Name == repository.Name)
            {
                Console.WriteLine("Successfully delete repository");
                continue;
            };
            newRepositories.Add(repo);
        }

        _configuration.Repositories = newRepositories.ToArray();
    }

    public void Save()
    {
        File.WriteAllText(filepath, JsonConvert.SerializeObject(_configuration, Formatting.Indented));
    }

    public Repository[]? GetRepositories()
    {
        return _configuration?.Repositories;
    }

}