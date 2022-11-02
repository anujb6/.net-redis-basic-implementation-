using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_redisAPI.Models;
using net_redisAPI.Service;
using StackExchange.Redis;

namespace net_redisAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class redisController : ControllerBase
    {
        private readonly CacheService _cache;
        private readonly redis_testContext _context;
        public redisController(CacheService cache, redis_testContext context)
        {
            _cache = cache;
            _context = context;
        }

        [HttpGet("driver")]
        public async Task<IActionResult> get()
        {
            // check cached data
            var cacheData =  _cache.GetData<IEnumerable<Driver>>("drivers");

            if (cacheData != null && cacheData.Count() > 0)
                return Ok(cacheData);

            cacheData = await _context.Drivers.ToListAsync();

            // Set Expiry Time
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            var confirm =  _cache.SetData<IEnumerable<Driver>>("drivers", cacheData, expiryTime);

            return Ok(cacheData);
        }
    }
}