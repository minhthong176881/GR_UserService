using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserService.Data;

namespace UserService.Services
{
    public class UserService : UserMicroService.UserMicroServiceBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserDbContext _context;
        
        public UserService(ILogger<UserService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _context = new UserDbContext(_configuration);
        }
        

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RegisterResponse
            {
                Success = true
            });
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var user = _context.Users.SingleOrDefault(x => x.UserName == request.Username && x.Password == request.Password);
            if (user == null)
                return Task.FromResult(new LoginResponse
                {
                    Token = null
                });
            return Task.FromResult(new LoginResponse
            {
                Token = "123456789"
            });
        }
    }
}
