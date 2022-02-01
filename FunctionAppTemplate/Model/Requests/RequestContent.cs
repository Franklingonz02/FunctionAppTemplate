using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppTemplate.Model.Requests
{
    public class RequestContent
    {
        [JsonProperty(
        NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(
        NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "extension_CaptchaUserResponseToken")]
        public string CaptchaUserResponseToken { get; set; }

        [JsonProperty(
        NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "extension_IdTipoDocumento")]
        public string IdTipoDocumento { get; set; }

        [JsonProperty(
        NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "extension_NumeroDocumento")]
        public string NumeroDocumento { get; set; }
    }
}
