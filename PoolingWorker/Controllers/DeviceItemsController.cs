using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PoolingWorker.Data;
using PoolingWorker.Models.Domain;
using PoolingWorker.Models.Dtos;

namespace PoolingWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceItemsController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public DeviceItemsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var deviceItems = _mapper.Map<List<DeviceItemDto>>( _dbContext.DeviceItems.ToList());
            return deviceItems == null ? NotFound() : Ok(deviceItems);
        }
            
        // GET api/DeviceItems/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var deviceItem = _mapper.Map<DeviceItemDto>(await _dbContext.DeviceItems.FindAsync(id));
            return deviceItem == null ? NotFound() : Ok(deviceItem);
        }

        // POST api/ManageDeviceItems
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateDevice(DeviceItemDto deviceItemDto)
        {
            DeviceItem deviceItem = new DeviceItem
            {
                Name = deviceItemDto.Name,
                Description = deviceItemDto.Description
            };
            
            await _dbContext.AddAsync(deviceItem);
            await _dbContext.SaveChangesAsync();
            deviceItemDto.Id = deviceItem.Id;
            return CreatedAtAction(nameof(CreateDevice), new {id = deviceItem.Id}, deviceItemDto);
        }

        // PUT api/DeviceItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, DeviceItemDto deviceItemDto)
        {
            if (id != deviceItemDto.Id) return BadRequest();
            var deviceItem = _dbContext.DeviceItems.Find(id);
            if (deviceItem == null) return NotFound();
            
            deviceItem.Name = deviceItemDto.Name;
            deviceItem.Description = deviceItemDto.Description;
            _dbContext.Entry(deviceItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // PUT api/DeviceItems/5
        [HttpPut("{id}/TagLink")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTagLink(int id, DeviceItemDto deviceItemDto)
        {

            if (id != deviceItemDto.Id) return BadRequest();
            var deviceItem = _dbContext.DeviceItems.Find(id);
            if (deviceItem == null) return NotFound();

            deviceItem.Name = deviceItemDto.Name;
            deviceItem.Description = deviceItemDto.Description;
            _dbContext.Entry(deviceItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        // PUT api/DeviceItems/5
        [HttpPut("{id}/TagGroupLink")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTagGroupLink(int id, List<int> TagGroupIds)
        {
            List<TagItem> ListTagItems = new List<TagItem>();
            List<TagGroup> ListTagGroup = new List<TagGroup>();
 
            foreach (int Tgid in TagGroupIds)
            {
                var tagGroup = _dbContext.TagGroups.Find(Tgid);
                if (tagGroup == null) continue;

                await _dbContext.TagGroups.Include(c => c.TagItems)
                    .Where(x => x.Id == Tgid)
                    .ForEachAsync(x=> 
                    {   
                        ListTagGroup.Add(x);
                        if (x.TagItems != null)
                        {
                            ListTagItems.AddRange(x.TagItems);
                        }
                    } );
                
            }
            
            var deviceItem = _dbContext.DeviceItems.Find(id);
            if (deviceItem == null) return NotFound();
            if (deviceItem.TagItems != null)
                if(deviceItem.TagItems.Count > 0)
                    ListTagItems = (List<TagItem>)ListTagItems.Union(deviceItem.TagItems);
            
            deviceItem.TagItems = ListTagItems.Distinct().ToList();
            
            if (deviceItem.TagGroups != null)
                if(deviceItem.TagGroups.Count > 0)
                    ListTagGroup = (List<TagGroup>)ListTagGroup.Union(deviceItem.TagGroups);
            deviceItem.TagGroups = ListTagGroup;
            //deviceItem.TagGroups.Add(ListTagGroup);
            _dbContext.Entry(deviceItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        // DELETE api/DeviceItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var deviceItemToDelete = await _dbContext.DeviceItems.FindAsync(id);
            if (deviceItemToDelete == null) return NotFound();

            _dbContext.DeviceItems.Remove(deviceItemToDelete);
            await _dbContext.SaveChangesAsync();
               
            return NoContent();
        }

    }
}
