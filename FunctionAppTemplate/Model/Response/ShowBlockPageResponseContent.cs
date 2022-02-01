using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppTemplate.Model.Response
{
    public class ShowBlockPageResponseContent : ResponseContentBase
    {
        public ShowBlockPageResponseContent(string userMessage)
            : base(ResponseType.ShowBlockPage, userMessage)
        {
        }
    }
}
