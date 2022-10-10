using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public class StructureProducer
{
    Dictionary<string, Type> factory;

    public StructureProducer()
    {
        factory = new Dictionary<string, Type>();
        Initialize();
    }

    private void Initialize()
    {
        var structureTypes = Assembly.GetAssembly(typeof(Structure)).GetTypes()
            .Where( (type) => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Structure)));

        foreach(var type in structureTypes)
        {
            var structure = Activator.CreateInstance(type) as Structure;
            factory.Add(structure.Name, type);
        }
    }

    public Structure GetStructure(string structureType)
    {
        if (factory.ContainsKey(structureType))
        {
            Type type = factory[structureType];
            return Activator.CreateInstance(type) as Structure;
        }

        return null;
    }
}
