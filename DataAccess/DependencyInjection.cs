
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MyApiContext>(options =>
            options.UseSqlServer( configuration.GetConnectionString("MyApiContext") ?? throw new InvalidOperationException("Connection string 'MyApiContext' not found.")));

            services.AddScoped<MyApiContext>();

            return services;
        }
    }
}
