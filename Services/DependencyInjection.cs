using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.IServices;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICandidatoService, CandidatoService>();
            services.AddScoped<IHabilidadService, HabilidadService>();
            services.AddScoped<IOfertaHabilidadService, OfertaHabilidadService>(); 
            services.AddScoped<ICandidatoHabilidadService, CandidatoHabilidadService>();
            services.AddScoped<ICandidatoOfertaService, CandidatoOfertaService>();
            services.AddScoped<IOfertaService, OfertaService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFormacionService, FormacionService>();
            services.AddScoped<IConsultaService, ConsultaService>();
            return services;
        }
    }
}
