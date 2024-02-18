using Dapper;
using JWTauthentication_practice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace JWTauthentication_practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : ControllerBase
    {
        private readonly IConfiguration _Configuration;
        private readonly string dbConnection;
        public JwtController(IConfiguration configuration)
        {
            _Configuration = configuration;
            dbConnection = _Configuration["ConnectionStrings:jwtAuthentication"];
        }

        [HttpPost]
        public string AuthenticateUser(string Username, string Password)
        {
            //var user = new UserClass();
            string token = "";
            using (var connection = new SqlConnection(dbConnection))
            {
                var user = connection.QueryFirst<UserClass>("Select * from jwtAuthentication where Username = @Username and Password = @Password;",
                    new { Username, Password});
                if (user == null)
                {
                   token =  "User not null";
                }
                else
                {
                    var jwt = new JwtClass("ACDt1vR3lXToPQ1g3MyNaY14IAnujYasir", "10");
                    token = jwt.generateToken(user);
                   
                }

            } ;
            return token;
            
        }

    }
}
