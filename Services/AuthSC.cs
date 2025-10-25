using Identity.Data;
using Identity.Models;
using Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Services
{
    public class AuthSC
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private string userRole;

        public AuthSC(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
    IConfiguration configuration, ApplicationDbContext context)
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        // ✅ Register Method
        public async Task<AuthResult> RegisterAsync(SignupVM model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return new AuthResult { Success = false, Errors = new List<string> { "User already exists" } };

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return new AuthResult { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };

            
            if (!string.IsNullOrEmpty(model.Role))
            {
                
                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                await _userManager.AddToRoleAsync(user, model.Role);
            }

            if (model.Role == "Student")
            {
                _context.Students.Add(new Student
                {
                    UserId = user.Id,
                    University = model.University,
                    Faculty = model.Faculty,
                    Department = model.Department,
                    Year = model.Year,
                    RegisterCourses = model.RegisterCourses
                });
            }
            else if (model.Role == "Doctor")
            {
                _context.Doctors.Add(new Doctor
                {
                    UserId = user.Id,
                    University = model.University,
                    Faculty = model.Faculty,
                    Department = model.Department,
                    Specialization = model.Specialization
                });
            }
            else if (model.Role == "Support")
            {
                _context.Supports.Add(new Support
                {
                    UserId = user.Id,
                    Department = model.Department,
                    IsOnline = false,
                    LastTicketLocal = "",
                    TicketsHandled = 0
                });
            }
            else if (model.Role == "Proctor")
            {
                _context.Proctors.Add(new Proctor
                {
                    UserId = user.Id,
                    AssignedExams = "",
                    ReviewedAlerts = ""
                });
            }
            else if (model.Role == "Admin")
            {
                _context.Admins.Add(new Admin
                {
                    UserId = user.Id,
                    SystemLogsAccess = true,
                    ActionsHistory = "Created Account"
                });
            }

            await _context.SaveChangesAsync();
            return new AuthResult { Success = true };
        }
        


        // ✅ Login Method
        public async Task<AuthResult> LoginAsync(LoginVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new AuthResult { Success = false, Errors = new List<string> { "Invalid email or password" } };

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return new AuthResult { Success = false, Errors = new List<string> { "Invalid email or password" } };

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault() ?? "User";

            var token = await GenerateJwtToken(user);
            return new AuthResult
            {
                Success = true,
                Token = token,
                Role = userRole,
                UserId = user.Id,
                FullName = user.FullName
            };
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            


            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
