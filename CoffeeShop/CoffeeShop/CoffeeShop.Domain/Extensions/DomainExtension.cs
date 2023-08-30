using Microsoft.Extensions.DependencyInjection;

using CoffeeShop.Domain.Services.CommandServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Domain.Interfaces.Services.CommandServices;
using CoffeeShop.Domain.Interfaces.Services.QueryServices;
using CoffeeShop.Domain.Services.QueryServices;
using CoffeeShop.Domain.Interfaces.Services.QueryService;

namespace CoffeeShop.Domain.Extensions
{
    public static class DomainExtension
    {

        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddCommandServices();
            services.AddQueryServices();
            services.AddGeneralServices();

            return services;
        }

        static IServiceCollection AddCommandServices(this IServiceCollection services)
        {
            services.AddTransient<ICoffeeCommandService, CoffeeCommandService>();
            services.AddTransient<IClientCommandService, ClientCommandService>();
            services.AddTransient<ISaleCommandService, SaleCommandService>();
            return services;
        }

        static IServiceCollection AddQueryServices(this IServiceCollection services)
        {
            services.AddTransient<ICoffeeQueryService, CoffeeQueryService>();
            services.AddTransient<IClientQueryService, ClientQueryService>();
            //services.AddTransient<ISaleQueryService, ISaleQueryService>();
            return services;
        }

        static IServiceCollection AddGeneralServices(this IServiceCollection services)
        {
       
            return services;
        }

    }
}
