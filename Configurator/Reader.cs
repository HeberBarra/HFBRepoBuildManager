using Newtonsoft.Json;

namespace RepoBuildManager.Configurator;

public class Reader
{

    public Configuration? readFile(string filepath)
    {
        return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(filepath));
    }
    
}