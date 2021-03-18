using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Repositories;
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

namespace GraphQL
{
    public class Startup
    {
        private readonly string AllowedOrigin = "allowedOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MainContext>((opt) =>
            {
                opt.UseSqlServer("Data Source=.;Initial Catalog=graphql;User ID=SA;Password=OreKim1820");
            });
            
            services.AddScoped<AuthorRepository>();
            services.AddScoped<BookRepository>();
            services.AddScoped<AuthorHandler>();
            
            services.AddInMemorySubscriptions();

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>();    
            
            services.AddCors(option => {
                option.AddPolicy("allowedOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "GraphQL", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQL v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseCors(AllowedOrigin);
            app.UseWebSockets();


            app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
        }
    }
}