using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WebApi {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            services.AddIdentityService(new IdentityServiceOptions {
                Authority = "http://localhost:8080/auth/realms/master",
                ClientId = "web-api",
                ClientSecret = "b69692dd-4097-4e8c-9671-d4636fb46737",
                CallbackPath = "/login",
                Events = new OpenIdConnectEvents {
                    OnTokenResponseReceived = context => {
                        var ticket = context.ProtocolMessage.AccessToken;
                        var json = JsonConvert.SerializeObject(context.ProtocolMessage, Formatting.Indented);
                        Console.WriteLine(json);
                        return Task.CompletedTask;
                    }
                }
            });



            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            app.UseAuthentication();
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
