//using Lucene.Net.Support;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using System.Threading;
//using System.Web;
//using System.Web.Http;
//using System.Web.Http.Controllers;
//using System.Web.Http.Filters;

//namespace Medical.Controllers



//{
//    public class Authorizer : AuthorizationFilterAttribute
//    {
//        public override void OnAuthorization(HttpActionContext actionContext)
//        {
//            try
//            {
//                if (actionContext.Request.Headers == null || actionContext.Request.Headers.Authorization == null)
//                    throw new Exception();

//                var encodedToken = actionContext.Request.Headers.Authorization.Parameter;
//                // todo: validate token first
//                var handler = new JwtSecurityTokenHandler();
//                var validationParameters = GetValidationParameters();
//                SecurityToken validatedToken;
//                handler.ValidateToken(encodedToken, validationParameters, out validatedToken);

//                // this part is executed iff token is valid
//                var jsonToken = handler.ReadToken(encodedToken);

//                var tokenStream = handler.ReadToken(encodedToken) as JwtSecurityToken;
//                string role = tokenStream.Claims.First<Claim>(c => c.Type == "role").Value;
//                string id = tokenStream.Claims.First<Claim>(c => c.Type == "id").Value;

//                System.Diagnostics.Debug.WriteLine(id + " auth as: " + role);
//                IPrincipal principal = new GenericPrincipal(new GenericIdentity(id), new string[] { role });
//                SetPrincipal(principal);
//            }
//            catch (Exception ex)
//            {
//                ex.Message.ToString();
//            }
//        }
//        public void SetPrincipal(IPrincipal principal)
//        {
//            Thread.CurrentPrincipal = principal;
//            if (HttpContext.Current != null)
//            {
//                HttpContext.Current.User = principal;
//            }
//        }
//        private static TokenValidationParameters GetValidationParameters()
//        {
//            return new TokenValidationParameters()
//            {
//                // todo: move to separate file
//                ValidateLifetime = false, // Because there is no expiration in the generated token
//                ValidateAudience = false, // Because there is no audiance in the generated token
//                ValidateIssuer = false,   // Because there is no issuer in the generated token
//                ValidIssuer = "Sample",
//                ValidAudience = "Sample",
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Secret)) // The same key as the one that generate the token
//            };
//        }
//    }
//}
