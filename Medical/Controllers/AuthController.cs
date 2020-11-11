using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Medical.Models;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Results;
using System.Linq;
//using Tweetinvi;
//using Lucene.Net.Support;

namespace Medical.Controllers

{
    [Route("api/auth")]
    public class AuthsController : ApiController
    {
        private MedicalContext db = new MedicalContext();

    
       

        // POST: api/auth
        [ResponseType(typeof(hr))]
        [HttpPost]
        public async Task<IHttpActionResult> Login(Auth authReq)
        {
            hr hr = await GetHr(authReq);

            if (hr != null)
            {
                

                //the query will only display expirydates which are null, meaning the original role of the user
                // or if there is a datetime, it checks if that time has already expired
                //it then returns the highest position. 
                //assume that roleid of delegated is the highest position

                var claims = new[]
                {
                    // TODO: save keys in external file
                    new Claim(JwtRegisteredClaimNames.Sub, "Subject"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("id", hr.emp_id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hjwbfkjefiuewhfiewhfewihf"));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken("Issuer", "Audience", claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                AuthRes res = new AuthRes();
                res.token = new JwtSecurityTokenHandler().WriteToken(token);
                res.id = hr.emp_id;

                //System.Diagnostics.Debug.WriteLine("login role: " + res.role);
                IPrincipal principal = new GenericPrincipal(new GenericIdentity(hr.emp_id.ToString()), new string[] { "hr" });
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }

                return Ok(res);
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }

        private async Task<hr> GetHr(Auth auth)
        {
            return await db.hrs.FirstOrDefaultAsync(e =>  e.password == auth.password);
        }
    }
}