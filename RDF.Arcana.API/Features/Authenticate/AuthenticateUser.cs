using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Users.Exception;

namespace RDF.Arcana.API.Features.Authenticate;

public class AuthenticateUser
{
    public class AuthenticateUserQuery : IRequest<AuthenticateUserResult>
    {
        [Required]
        public string Username
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
    }

    public class AuthenticateUserResult
    {
        public int Id
        {
            get;
            set;
        }

        public string Fullname
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        
        public string Token
        {
            get;
            set;
        }

        public AuthenticateUserResult(User user, string token)
        {
            Id = user.Id;
            Fullname = user.Fullname;
            Username = user.Username;
            Token = token;
        }

        public class Handler : IRequestHandler<AuthenticateUserQuery, AuthenticateUserResult>
        {
            private readonly DataContext _context;
            private readonly IConfiguration _configuration;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IConfiguration configuration, IMapper mapper)
            {
                _context = context;
                _configuration = configuration;
                _mapper = mapper;
            }

            public async Task<AuthenticateUserResult> Handle(AuthenticateUserQuery command,
                            CancellationToken cancellationToken)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == command.Username, cancellationToken);
            
                if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
                {
                    throw new UsernamePasswordIncorrectException();
                }
            
                var token = GenerateJwtToken(user);
            
                var result = new AuthenticateUserResult(user, token);
            
                var results = _mapper.Map<AuthenticateUserResult>(result);
            
                return results;
            }

            private string GenerateJwtToken(User user)
            {
                var key = _configuration.GetValue<string>("JwtConfig:Key");
                var keyBytes = Encoding.ASCII.GetBytes(key);
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
        }
    }
}