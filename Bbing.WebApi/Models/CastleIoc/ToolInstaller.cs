using Bbing.EF.Repository;
using Bbing.Infrastructure;
using Bbing.Infrastructure.Redis;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bbing.WebApi.Models.CastleIoc
{
    public class ToolInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    Component.For<IRedisHelper>().ImplementedBy<CSRedisHelper>().LifestylePerWebRequest() //实现类
                );
            container.Register(
                    Component.For<EFUnitOfWork>().LifestylePerWebRequest() //实现类
                );
        }
    }
}