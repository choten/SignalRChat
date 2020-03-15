using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMvcChat
{
    public class MyIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var id = request.QueryString["id"];
            return string.IsNullOrEmpty(id) ? string.Empty : id;
        }
    }
}