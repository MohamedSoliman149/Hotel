namespace Reservation.Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        public UserLoginCommandHandler(UserManager<User> userManager,
                                        SignInManager<User> signInManager,
                                         IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (result.Succeeded)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                    var issuer = _configuration["Jwt:Issuer"];
                    var audience = _configuration["Jwt:Audience"];
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID as NameIdentifier
                    new Claim(ClaimTypes.Email, user.Email), // Email
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName), // Username
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique identifier for the token
                }),
                        Expires = DateTime.UtcNow.AddHours(2),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    return new AuthResponseDTO { Success = true, Token = tokenString };
                }
            }

            return new AuthResponseDTO { Success = false, Errors = new List<string> { "Invalid login attempt." } };
        }

        //public async Task<AuthResponseDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.UserName);
        //    if (user != null)
        //    {
        //        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        //        if (result.Succeeded)
        //        {
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        //            var issuer = _configuration["Jwt:Issuer"];
        //            var audience = _configuration["Jwt:Audience"];
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Subject = new ClaimsIdentity(new[]
        //                {
        //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //                Expires = DateTime.UtcNow.AddHours(2),
        //                Issuer = issuer,
        //                Audience = audience,
        //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //            };

        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            var tokenString = tokenHandler.WriteToken(token);

        //            return new AuthResponseDTO { Success = true, Token = tokenString };
        //        }
        //    }

        //    return new AuthResponseDTO { Success = false, Errors = new List<string> { "Invalid login attempt." } };
        //}

    }
}
