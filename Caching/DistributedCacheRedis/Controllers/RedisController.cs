using DistributedCacheRedis.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DistributedCacheRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly RedisService redisService;
        private readonly IDatabase db;

        public RedisController(RedisService redisService)
        {
            this.redisService = redisService;
            db = redisService.GetDatabase(0);
        }

        [HttpGet("StringTypeCaching")]
        public IActionResult StringTypeCaching()
        {
            db.StringSet("Name", "Fatih Dursun");

            var value = db.StringGet("name");
            return Ok(value);
        }

        [HttpGet("ListTypeCaching")]
        public IActionResult ListTypeCaching()
        {
            var listKey = "Languages";
            List<string> languages = new List<string>() { "C#", "Golang", "JavaScript", "Python" , "C++" };

            // write list to cache
            foreach (var lang in languages)
                db.ListLeftPush(listKey, lang);

            // remove item in list
            db.ListRemoveAsync(listKey, "C++").Wait();

            // get all item in list
            List<string> languagesFromCache = new List<string>();
            if (db.KeyExists(listKey))
            {
                db.ListRange(listKey).ToList().ForEach(x =>
                {
                    languagesFromCache.Add(x.ToString());
                });
            }

            return Ok(languagesFromCache);
        }

        [HttpGet("SetTypeCaching")]
        public IActionResult SetTypeCaching()
        {
            var listKey = "LanguagesSet";
            HashSet<string> languages = new HashSet<string>() { "C#", "Golang", "JavaScript", "Python", "C++" };

            // write set to cache
            foreach (var lang in languages)
            {
                db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
                db.SetAdd(listKey, lang);
            }

            // remove item in list
            db.SetRemoveAsync(listKey, "C++").Wait();

            // get all item in list
            HashSet<string> languagesFromCache = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    languagesFromCache.Add(x.ToString());
                });
            }

            return Ok(languagesFromCache);
        }

        [HttpGet("HashTypeCaching")]
        public IActionResult HashTypeCaching()
        {
            var hashKey = "fatih";
            Dictionary<string, string> list = new Dictionary<string, string>() ;
            list.Add("Languages" , "C#");
            list.Add("Provider", "Microsoft");
            list.Add("Release Date ", "2000");

            foreach (var hash in list)
                db.HashSet(hashKey, hash.Key, hash.Value);

            db.HashDelete(hashKey, "Provider");

            Dictionary<string, string> listFromCache = new Dictionary<string, string>();
            if (db.KeyExists(hashKey))
            {
                db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    listFromCache.Add(x.Name, x.Value);
                });
            }

            return Ok(listFromCache);
        }



    }
}
