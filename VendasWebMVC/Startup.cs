using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VendasWebMVC.Data;
using VendasWebMVC.Servicos;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace VendasWebMVC {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<CookiePolicyOptions>(options => {
                // Este lambda determina se o consentimento do usuário para cookies não essenciais é necessário para uma determinada solicitação.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<VendasWebMVCContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("VendasWebMVCContext"), builder =>
        builder.MigrationsAssembly("VendasWebMVC")));

            services.AddScoped<PovoarServico>();
            services.AddScoped<VendedorServico>();
            services.AddScoped<DepartamentoServico>();
            services.AddScoped<VendasRecordeServico>();
        }

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PovoarServico povoarServico) {

            var enUs = new CultureInfo("en-US");
            var localizationOptions = new RequestLocalizationOptions {
                DefaultRequestCulture = new RequestCulture(enUs),
                SupportedCultures = new List<CultureInfo> { enUs },
                SupportedUICultures = new List<CultureInfo> { enUs }
            };

            app.UseRequestLocalization(localizationOptions);

            // Se estiver no perfil de desenvolvimento
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                povoarServico.Povoar();

            }
            else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
