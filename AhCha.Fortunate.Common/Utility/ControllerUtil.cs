using System.Reflection;

namespace AhCha.Fortunate.Common.Utility
{
    public class ControllerUtil
    {
        public static List<string> GetAllControllerNames(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var controllerNames = types.Where(t => t.IsClass && t.BaseType.Name == "BaseApiController")
                .Select(t => t.Name.Replace("Controller", ""))
                .ToList();
            return controllerNames;
        }
    }
}
