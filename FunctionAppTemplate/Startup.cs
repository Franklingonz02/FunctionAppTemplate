using Azure.Identity;
using FunctionAppTemplate;
using FunctionAppTemplate.Interfaces;
using FunctionAppTemplate.Repository;
using FunctionAppTemplate.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionAppTemplate
{

   public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {


            builder.Services.AddOptions<Localsettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(Localsettings.B2CSettingsName).Bind(settings);
            });

            builder.Services.AddSingleton(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<Localsettings>>();
                var scopes = new[] { "https://graph.microsoft.com/.default" };

                var clientSecretCredential = new ClientSecretCredential(
                    settings.Value.TenantId,
                    settings.Value.ApplicationId,
                    settings.Value.ClientSecret);

                return new GraphServiceClient(clientSecretCredential, scopes);
            });

            builder.Services.AddSingleton<IGraphRepository, GraphRepository>();

        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();

            builder.ConfigurationBuilder
               .SetBasePath(context.ApplicationRootPath)
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
               .AddUserSecrets<Startup>(optional: true, reloadOnChange: false)
               .AddEnvironmentVariables();
        }

        private GraphServiceClient Create(Localsettings settings)
        {

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var clientSecretCredential = new ClientSecretCredential(
                settings.TenantId,
                settings.ApplicationId, 
                settings.ClientSecret);

            return new GraphServiceClient(clientSecretCredential, scopes);
        }
    }
}
