using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Blog_Management_System.Auth
{
	public class Logged: AuthorizationFilterAttribute
	{
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization;
            if (token == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    System.Net.HttpStatusCode.Unauthorized, "Authorization token is missing.");
                
            }
            else if(!AuthService.IsTokenValid(token.ToString()))
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                   System.Net.HttpStatusCode.Unauthorized, "Supply token is invalid or expired");
            }
                base.OnAuthorization(actionContext);
        }
    }
}