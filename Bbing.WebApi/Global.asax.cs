using Autofac;
using Autofac.Integration.WebApi;
using Bbing.Domain.IRepositories;
using Bbing.Repository.Repositories.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bbing.Infrastructure;
using Bbing.Infrastructure.Redis;
using Bbing.WebApi.Filters;

namespace Bbing.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var builder = new ContainerBuilder();
            // 注册所有的Controller
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var IRepositoryAndService = Assembly.Load("Bbing.Domain");
            var Repository = Assembly.Load("Bbing.Repository");
            var Service = Assembly.Load("Bbing.Service");
            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IRepositoryAndService, Repository).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(IRepositoryAndService, Service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterType<CSRedisHelper>().As<IRedisHelper>();
            // 把容器装入到微软默认的依赖注入容器中
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((IContainer)container);
        }

    }
}
