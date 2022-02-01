using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppTemplate.Model.Response
{
    public class ContinueResponseContent : ResponseContentBase
    {
        public ContinueResponseContent()
            : base(ResponseType.Continue, string.Empty)
        {
        }
    }
}
