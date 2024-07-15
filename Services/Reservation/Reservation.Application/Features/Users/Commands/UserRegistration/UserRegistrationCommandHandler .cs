namespace Reservation.Application.Features.Users.Commands
{
    public class UserRegistrationCommandHandler : ICommandHandler<UserRegistrationCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserRegistrationCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new User { UserName = request.UserName, Email = request.UserName };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Use NameIdentifier for user ID
                new Claim(ClaimTypes.Name, user.UserName), // Use Name for user name or email
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new AuthResponseDTO { Success = true, Token = tokenString };
            }
            return new AuthResponseDTO { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };
        }
        //public async Task<AuthResponseDTO> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        //{
        //    var user = new User { UserName = request.UserName, Email = request.UserName };
        //    var result = await _userManager.CreateAsync(user, request.Password);

        //    if (result.Succeeded)
        //    {
        //        var tokenHandler = new JwtSecurityTokenHandler();
        //        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //        var issuer = _configuration["Jwt:Issuer"];
        //        var audience = _configuration["Jwt:Audience"];
        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new Claim[]
        //            {
        //        new Claim(ClaimTypes.Name, user.Id.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //            }),
        //            Expires = DateTime.UtcNow.AddDays(7),
        //            Issuer = issuer,
        //            Audience = audience,
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = tokenHandler.CreateToken(tokenDescriptor);
        //        var tokenString = tokenHandler.WriteToken(token);

        //        return new AuthResponseDTO { Success = true, Token = tokenString };
        //    }
        //    return new AuthResponseDTO { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };
        //}


    }
}
