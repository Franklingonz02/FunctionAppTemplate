using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppTemplate.Settings
{
    public class Localsettings
    {
        public const string B2CSettingsName = "Values:AzureB2cSettings";

        public string TenantId { get; set; }
        public string ApplicationId { get; set; }
        public string ClientSecret{get; set;}
        public string B2cExtensionAppClientId { get; set; }
    }
}