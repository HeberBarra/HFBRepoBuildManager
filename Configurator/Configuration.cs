using Newtonsoft.Json;

namespace RepoBuildManager.Configurator;

public class Configuration(string schema,bool semanticNames,Repository[]? repositories)
{

    [JsonProperty(PropertyName = "$schema")]
    public string Schema = schema;

    public bool? SemanticNames = semanticNames;
    public Repository[]? Repositories { get; set; } = repositories;
}