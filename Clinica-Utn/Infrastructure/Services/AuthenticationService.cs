using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.InterFaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        public AuthenticationService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }
        public User? ValidateUser(CredentialRequest credentialRequest)
        {
            User? user = _userRepository.Authenticate(credentialRequest.Email, credentialRequest.Password);
            if (user is not null)
            {
                return user;
            }
            return null;
        }
            public string AuthenticateCredentials(CredentialRequest credentialRequest)
            {
                var userAuthenticated = ValidateUser(credentialRequest);
                if (userAuthenticated == null)
                {
                    return "null";
                }
                
                var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"])); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;
                SigningCredentials signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

                //3) CREAR CLAIMS 

                var claimsForToken = new List<Claim>();
                claimsForToken.Add(new Claim("sub", userAuthenticated.Id.ToString())); //"sub" es una key estándar que significa unique user identifier, es decir, si mandamos el id del usuario por convención lo hacemos con la key "sub".
                claimsForToken.Add(new Claim("given_email", userAuthenticated.Email));
                claimsForToken.Add(new Claim("role", userAuthenticated.UserRole.ToString())); //debería venir de usuario evita ir hasta la bd

                var jwtSecurityToken = new JwtSecurityToken(
                  _config["Authentication:Issuer"],
                  _config["Authentication:Audience"],
                  claimsForToken,
                  DateTime.UtcNow,
                  DateTime.UtcNow.AddHours(1),
                  signature);

                string tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return (tokenToReturn);


            }
        }
}
