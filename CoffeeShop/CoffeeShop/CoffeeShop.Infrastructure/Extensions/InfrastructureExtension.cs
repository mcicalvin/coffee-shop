using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces;
using CoffeeShop.Domain.Services;
using CoffeeShop.Domain.Utils;
using CoffeeShop.Infrastructure.DbContexts;
using CoffeeShop.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Infrastructure.Repositories.BaseRepositories;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Infrastructure.Repositories;

namespace CoffeeShop.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            //services.Configure<DirectoryConfigModel>(
            //   config.GetSection(""));



            //services.Configure<ColorOptions>(
            //    config.GetSection(ColorOptions.Color));
            //services.AddTransient<IConfiguration>(config);
            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environments.Development}.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<UserStore<User>>();
            services.AddDbContext<CoffeeShopDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<DbContext, CoffeeShopDbContext>();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddUtils();
            services.AddServices();
            services.AddCommandRepositories();
            services.AddQueryRepositories();

            return services;
        }

        private static IServiceCollection AddCommandRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICommandRepository<Coffee>, CommandRepository<Coffee>>();
            services.AddTransient<ICommandRepository<Client>, CommandRepository<Client>>();
            services.AddTransient<ICommandRepository<Sale>, CommandRepository<Sale>>();
            services.AddTransient<ICommandRepository<SaleItem>, CommandRepository<SaleItem>>();
            services.AddTransient<ICommandRepository<Point>, CommandRepository<Point>>();

            return services;
        }

        private static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            services.AddTransient<IQueryRepository<Coffee, CoffeeFilter>, CoffeeQueryRepository>();
            services.AddTransient<IQueryRepository<Client, ClientFilter>, ClientQueryRepository>();
            services.AddTransient<IQueryRepository<Sale, SaleFilter>, SaleQueryRepository>();
            services.AddTransient<IQueryRepository<SaleItem, SaleItemFilter>, SaleItemQueryRepository>();
            services.AddTransient<IQueryRepository<Point, PointFilter>, PointQueryRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {


            return services;
        }

        private static IServiceCollection AddUtils(this IServiceCollection services)
        {

            services.AddTransient<ISerializer, Serializer>();
            services.AddTransient<IBaseRestClient, BaseRestClient>();
            return services;
        }


    }
}
