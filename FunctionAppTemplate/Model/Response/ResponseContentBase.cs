using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppTemplate.Model.Response
{
    public abstract class ResponseContentBase
    {
        protected const string DefaultApiVersion = "1.0.0";

        protected ResponseContentBase(
            string action,
            string userMessage,
            string version = DefaultApiVersion)
        {
            Action = action;
            UserMessage = userMessage;
            Version = version;
        }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "version")]
        public string Version { get; private set; }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "action")]
        public string Action { get; private set; }

        [JsonProperty(
            NullValueHandling = NullValueHandling.Ignore,
            PropertyName = "userMessage")]
        public string UserMessage { get; private set; }
    }
}
