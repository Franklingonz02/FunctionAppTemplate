using FunctionAppTemplate.Interfaces;
using FunctionAppTemplate.Settings;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppTemplate.Repository
{
    public class GraphRepository : IGraphRepository
    {

        private readonly GraphServiceClient _graphClient;
        private readonly Localsettings _settings;

        public GraphRepository(GraphServiceClient graphClient, IOptions<Localsettings> options)
        {
            _graphClient = graphClient;
            _settings = options.Value;
        }



        private async Task<User[]> GetUsersByEmailAsync(string email)
        {
            var result = await _graphClient.Users
                .Request()
                .Filter($"otherMails/any(c:c eq '{email}') and UserType eq 'Member'")
                .GetAsync();

            return result.ToArray();
        }

        public async Task<bool> ExistsUserWithEmailAsync(string email)
        {
            var taskResult = await Task.WhenAll(
                GetUsersByIdentityAsync(email),
                GetUsersByEmailAsync(email));

            var result = taskResult
                .SelectMany(user => user);

            return result.Any();
        }


        private async Task<User[]> GetUsersByIdentityAsync(string email)
        {
            var result = await _graphClient.Users
                .Request()
                .Filter($"identities/any(c:c/issuerAssignedId eq '{email}' and c/issuer eq '{_settings.TenantId}')")
                .GetAsync();

            return result.ToArray();
        }






        //public async Task<PerfilClienteB2c> ObtenerUsuario(string TipoDocumento, string NumeroDocumento)
        //{
        //    try
        //    {

        //        Helpers.B2cCustomAttributeHelper helper = new Helpers.B2cCustomAttributeHelper(_B2cExtensionAppClientId);
        //        string NumeroDocumentoAtributo = helper.GetCompleteAttributeName(NumeroDoc);
        //        string TipoDocumentoAtributo = helper.GetCompleteAttributeName(TipoDoc);

        //        var result = await _graphServiceClient.Users
        //           .Request()
        //           .Filter($" {NumeroDocumentoAtributo} eq '{NumeroDocumento}' and {TipoDocumentoAtributo} eq '{TipoDocumento}'")
        //           .Select(e => new
        //           {
        //               e.DisplayName,
        //               e.Id,
        //               e.Identities,
        //               e.CreatedDateTime
        //           })
        //           .GetAsync();

        //        var perfilClienteB2C = new PerfilClienteB2c();

        //        if (result.Count == 0)
        //            return perfilClienteB2C;

        //        perfilClienteB2C.Id = result[0].Id;
        //        perfilClienteB2C.Correo = result[0].Identities.First<ObjectIdentity>().IssuerAssignedId;
        //        perfilClienteB2C.FechaCreacion = result[0].CreatedDateTime.Value.DateTime;

        //        return perfilClienteB2C;

        //    }
        //    catch (ServiceException e)
        //    {
        //        throw e;
        //    }
        //}

    }
}
