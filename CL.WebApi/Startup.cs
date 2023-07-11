using CL.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using CL.Manager.Interfaces;
using CL.Data.Repository;
using CL.Manager.Implementation;
using FluentValidation.AspNetCore;
using CL.Manager.Validator;

namespace CL.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => 
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Consultório Legal", Version = "v1" }));
            
            services.AddControllers()
                // Injeção de Dependência da validação na Controller
                .AddFluentValidation(c => 
                    { 
                        c.RegisterValidatorsFromAssemblyContaining<ClienteValidator>(); 
                        // Foi adicionado {} dentro da expressão Lambda para tranformá-la em uma coleção de comportamentos.
                        c.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("pt-BR");
                    })
                ;

            //Quando chamar "IClienteRepository", intancie "ClienteRepository".
            //INVERSÃO DE CONTROLE
            //Desacoplamento
            services.AddScoped<IClienteRepository, ClienteRepository>();
            
            //Regra de negócio disponibilizada como serviço. 
            //Proximo passo é injetar na Classe Controller.
            services.AddScoped<IClienteManager, ClienteManager>();

            //Como o banco de dados é o SQL Server, foi instalado o nugget EF CORE.SQLSERVER. 
            services.AddDbContext<ClContext>(options => 
                //A ConectionStrings será configurada na classe appsettings.json
                options.UseSqlServer(Configuration.GetConnectionString("ClConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
                {
                    //comando necessário para abrir o Swagger diretamente na barra de endereço do launcher.
                    c.RoutePrefix = string.Empty;
                    //este "v1" deve ser igual ao v1 do SwaggerDoc do services.
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CL V1"); 
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
