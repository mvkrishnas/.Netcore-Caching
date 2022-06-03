using Caching.Model;
using Caching.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // thread safe lock object creation.
        private static readonly SemaphoreSlim semaphore = new(1, 1);
        
        // Repository object for employe data
        private readonly IEmployeeReposiotry employeeRepository;
        private List<Employee> emp = new();
        
        // cache memory creation
        private readonly IMemoryCache memoryCache;

        private readonly IDistributedCache distributedCache;

        // object creation for  memory cache entry option
        MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                   .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                   .SetPriority(CacheItemPriority.Normal)
                   .SetSize(1024);
        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
            .SetSlidingExpiration(TimeSpan.FromSeconds(3600));


        public EmployeeController(IEmployeeReposiotry _employeeRepository, IMemoryCache _memoryCache, IDistributedCache _distributedCache)
        {
            employeeRepository = _employeeRepository;
            memoryCache = _memoryCache;
            distributedCache = _distributedCache;
        }

        [HttpGet("GetEmployeeDetails")]
        public IActionResult GetEmployeeDetails()
        {
            emp = employeeRepository.GetEmployeDetails();
            return Ok (emp);
        }
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            // Thread safe lock starting at below line.
            await semaphore.WaitAsync();
            try
            {
                if (!memoryCache.TryGetValue(id, out emp))
                {

                    // Below commented code for redis cache. If you want to uncomment the code first unzip the rediszipfile which exist in repo and run redis server.
                    //var result = await distributedCache.GetAsync(id.ToString());
                    //if (result != null)
                    //    emp = JsonConvert.DeserializeObject<List<Employee>>(Encoding.UTF8.GetString(result));
                    //if (emp.Count == 0)
                    //{

                        emp = employeeRepository.GetEmploye(id);
                        memoryCache.Set(id, emp, cacheEntryOptions);
                    //    await distributedCache.SetAsync(id.ToString(), Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emp)), options);

                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            { 
                semaphore.Release(); 
            }
            
            // Thread safe un locked.
            return Ok(emp);
        }
    }
}
