using Microsoft.AspNetCore.Mvc;
using PoolingWorker.Data;
using PoolingWorker.Models.Domain;
using PoolingWorker.Models.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolingWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoolRequestController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public PoolRequestController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<PoolRequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PoolRequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // TODO uncomment below

        //// POST api/<PoolRequestController>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //public async Task<IActionResult> Pooling([FromBody] RequestPoolingDto requestPoolings)
        //{
        //    List<RequestItem> RequestItems = new List<RequestItem>();
        //    foreach (var deviceId in requestPoolings.DeviceItemIds)
        //    {
        //        var DeviceItem = _dbContext.DeviceItems.Find(deviceId);
        //        if (DeviceItem == null) continue;
        //        RequestItem requestItem = new RequestItem();
        //        requestItem.Priority = requestPoolings.Priority;
        //        requestItem.DeviceItem = DeviceItem;
                
        //        foreach(int tagId in requestPoolings.TagIDs)
        //        {
        //            var TagItem = _dbContext.TagItems.Find(tagId);
        //            if (TagItem == null) continue;
        //            requestItem.TagItem.Add(TagItem);
        //        }
        //        requestItem.TagItem.Add(TagItem);
        //    }
            

        //    return CreatedAtAction(nameof(Pooling), DeviceIds);
        //}

        // PUT api/<PoolRequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PoolRequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
