using AutoMapper;
using System.Reflection;
using Shared.Lib.Mapping;

namespace Shared.Lib.Mapping;

public class MappingProfile : Profile {
    
    public MappingProfile() {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    private void ApplyMappingsFromAssembly(Assembly assembly) {
        var types = assembly.GetExportedTypes()
            .Where(x => typeof(IMapFrom).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .ToList();

        foreach (var type in types) {
            var instance   = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping");

            methodInfo?.Invoke(instance, [this]);
        }
    }
}