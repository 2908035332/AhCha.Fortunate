using Autofac;
using System.Reflection;

namespace AhCha.Fortunate.Api.AppCode
{
    /// <summary>
    /// 依赖自动注入
    /// </summary>
    public class DependencyAutoInjection : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly Service = Assembly.Load("AhCha.Fortunate.Service");
            Assembly IService = Assembly.Load("AhCha.Fortunate.IService");
            builder.RegisterAssemblyTypes(Service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(IService).Where(t => t.Name.EndsWith("IService")).AsImplementedInterfaces();
        }
    }
}
