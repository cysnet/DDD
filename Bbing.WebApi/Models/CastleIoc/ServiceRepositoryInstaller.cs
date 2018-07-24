using Bbing.Domain.IRepositories;
using Bbing.Domain.Service;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Bbing.WebApi.Models.CastleIoc
{
    public class ServiceRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Classes.FromAssemblyInDirectory(new AssemblyFilter("bin"))
                    .Where(e =>
                    e.Name.EndsWith("Service") && e.GetInterface("IBaseService`1") != null
                    ||
                    //e.Name.EndsWith("Repository") && e.GetInterface("IRepository`1") != null
                    ((e.Name.EndsWith("EFRepository")||(e.Name.EndsWith("Repository")&& !e.Name.EndsWith("MongoRepository"))) && e.GetInterface("IRepository`1") != null)
                    )
                    //.Where(e => e.Name.EndsWith("Service") || e.Name.EndsWith("Repository"))
                    .WithService.AllInterfaces()
                    .LifestylePerThread()
                );
        }
    }
}