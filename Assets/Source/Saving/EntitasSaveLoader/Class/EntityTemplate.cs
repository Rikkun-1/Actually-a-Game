using System.Collections.Generic;

public class EntityTemplate
{
    public string name;
    public string context;
    public string tags;
    
    public readonly Dictionary<string, object> components = new Dictionary<string, object>();

    public override string ToString()
    {
        return $"Name : {name}, Context : {context}, Tags : {tags}, ";
    }
}
