using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppTemplate.Model.Response;
using FunctionAppTemplate.Model.Requests;
using FunctionAppTemplate.Interfaces;

namespace FunctionAppTemplate
{
    public  class Home
    {
        private readonly IGraphRepository _graphRepository;

        public Home(IGraphRepository graphRepository) =>
            _graphRepository = graphRepository;

        [FunctionName("Home")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] RequestContent req,
            ILogger log)
        {
            if (string.IsNullOrEmpty(req.Email) || !req.Email.Contains("@"))
            {
                return new BadRequestObjectResult(new ShowBlockPageResponseContent("Correo requerido"));
            }

            try
            {
                var exists = await _graphRepository.ExistsUserWithEmailAsync(req.Email);

                if (exists)
                {
                    return new OkObjectResult(new ShowBlockPageResponseContent("Ya existe un usuario con este correo electrónico."));
                }
            }
            catch (Exception e)
            {
                log.LogError("Error executing MS Graph request: ", e);
                return new OkObjectResult(new ShowBlockPageResponseContent("There was a problem with your request."));
            }

            return new OkObjectResult(new ContinueResponseContent());
        }
    }
}
