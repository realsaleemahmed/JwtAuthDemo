  using Microsoft.AspNetCore.Mvc;
  using System.Text;
  using Microsoft.AspNetCore.Authorization;

  [ApiController]
  [Route("[controller]")]

  public class UserController : ControllerBase
  {
    private readonly TokenService _tokenService;

    private static readonly List<User> _users = new List<User>
    {
      new User { UserName = "testuser", Password = "testpassword" }
    };

    public UserController(TokenService tokenService)
    {
      _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
      var existingUser = _users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
      if (existingUser == null)
      {
        return Unauthorized("Invalid Username or Password");
      }

      var token = _tokenService.GenerateToken(user.UserName);
      return Ok(new { Token = token });
    }

    [HttpGet("secure-data")]
    [Authorize]
    public IActionResult GetSecureData()
    {
      return Ok(new { Message = "This is a protected endpoint!" });
    }
  };
