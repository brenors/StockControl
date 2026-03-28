using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockControl.Application.DTOs.Auth;
using StockControl.Application.Interfaces;
using StockControl.Common.Validator;
using StockControl.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockControl.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Register(RegisterRequest request)
        {
            var validator = DomainValidator.Contract();

            validator
                .Assert(request.Name.IsNotNullOrWhiteSpace(), "Name is required")
                .Assert(request.Email.IsValidEmail(), "Invalid email format")
                .Assert(request.Password.IsNotNullOrWhiteSpace(), "Password is required")
                .Assert(request.Password.IsLongerThan(11), "Password must be at least 12 characters")
                .Validate();

            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            DomainValidator.Assert(existingUser is null, "User already exists");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.Seller,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);

            return new AuthResponse
            {
                Token = GenerateToken(user)
            };
        }

        public async Task<AuthResponse> Login(LoginRequest request)
        {
            var validator = DomainValidator.Contract();

            validator
                .Assert(request.Email.IsValidEmail(), "Invalid email")
                .Assert(request.Password.IsNotNullOrWhiteSpace(), "Password is required")
                .Validate();

            var user = await _userRepository.GetByEmailAsync(request.Email);

            DomainValidator.Assert(user is not null, "Invalid credentials");

            var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            DomainValidator.Assert(isValidPassword, "Invalid credentials");

            return new AuthResponse
            {
                Token = GenerateToken(user)
            };
        }

        private string GenerateToken(User user)
        {
            var keyString = _configuration["Jwt:Key"];

            DomainValidator.Assert(keyString.IsNotNullOrWhiteSpace(), "JWT key not configured");

            var keyBytes = Encoding.UTF8.GetBytes(keyString);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
