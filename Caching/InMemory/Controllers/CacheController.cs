using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private IMemoryCache memoryCache;

        public CacheController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;

            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(10);
            options.Priority = CacheItemPriority.High;
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                memoryCache.Set("callback", $"{key}->{value} => reason:{reason}");
            });

            memoryCache.Set<String>("time", DateTime.Now.ToString(), options);

            User user = new User() { Id = 1, Name = "Fatih", Age = 24 };

            memoryCache.Set<User>($"user:{user.Id}", user);
        }


        [HttpGet("GetUserById")]
        public IActionResult GetUserById(int id)
        {
            var user = memoryCache.Get<User>($"user:{id}");
            return Ok(user);
        }

        [HttpGet("GetTime")]
        public IActionResult GetTime()
        {
            var time = memoryCache.Get<String>("time");
            return Ok(time);
        }
    }
}
