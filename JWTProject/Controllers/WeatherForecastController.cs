using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTProject.Controllers
{
    [Authorize(Roles ="person")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJWTAuthenticationManager jWTAuthenticationManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJWTAuthenticationManager jWTAuthenticationManager)
        {
            _logger = logger;
            this.jWTAuthenticationManager = jWTAuthenticationManager;
        }

        [Authorize(Roles = "user")]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [AllowAnonymous]
        [HttpPost(Name = "GetToken")]
        public async Task<IActionResult> Login([FromBody]  cred user )
        {
            var token = jWTAuthenticationManager.Authenticate(user.username, user.password);
            if (token == null) return BadRequest("Kullanıcı adı veya şifre yanlış!");
            return Ok(token);
        }

        [HttpGet]
        [Route("getAdmin")]
        [Authorize(Roles = "admin")]
        public string Admin()
        {
            //var token = jWTAuthenticationManager.Authenticate(user.username, user.password);    
            //if (token == null) return BadRequest("Kullanıcı adı veya şifre yanlış!");
            return "Merhaba Admin";
        }

        [HttpGet]
        [Route("getAdmin2")]
        [Authorize(Roles = "admin2")]

        public string Admin2()
        {
            //var token = jWTAuthenticationManager.Authenticate(user.username, user.password);    
            //if (token == null) return BadRequest("Kullanıcı adı veya şifre yanlış!");
            return "Merhaba Admin2";
        }
        public class cred
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}