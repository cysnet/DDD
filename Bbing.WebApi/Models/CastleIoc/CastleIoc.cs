using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bbing.WebApi.Models.CastleIoc
{
    public class CastleIoc
    {
        public static IWindsorContainer Regist()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.This());
            return container;
        }
    }
}