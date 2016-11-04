﻿using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Cinotam.ModuleZero.AppModule;
using System.Reflection;
using System.Web.Http;

namespace Cinotam.AbpModuleZero.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(CinotamModuleZeroAppModule))]
    public class AbpModuleZeroWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(CinotamModuleZeroAppModule).Assembly, "app")
                .Build();
            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
