using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PoolingWorker.Data;
using PoolingWorker.Models.Domain;
using PoolingWorker.Models.Dtos;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PoolingWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagGroupController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public TagGroupController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // GET: api/<TagGroupController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tagGroup = _mapper.Map<List<TagGroupDto>>(_dbContext.TagGroups.ToList());
            return tagGroup == null ? NotFound() : Ok(tagGroup);
        }

        // GET api/TagGroup/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var tagGroup = _mapper.Map<TagGroupDto>(await _dbContext.TagGroups.FindAsync(id));
            return tagGroup == null ? NotFound() : Ok(tagGroup);
        }


        // POST api/TagGroup
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTagGroup(string name)
        {
            TagGroup tagGroup = new TagGroup
            {
                Name = name
            };
            await _dbContext.AddAsync(tagGroup);
            await _dbContext.SaveChangesAsync();
            TagGroupDto taggroupDto = new TagGroupDto
            {
                Name = tagGroup.Name
            };
            taggroupDto.Id = tagGroup.Id;
            return CreatedAtAction(nameof(CreateTagGroup), new { id = tagGroup.Id }, taggroupDto);
        }


        // DELETE api/TagGroup/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var tagGroupToDelete = await _dbContext.TagGroups.FindAsync(id);
            if (tagGroupToDelete == null) return BadRequest();

            _dbContext.TagGroups.Remove(tagGroupToDelete);
            _dbContext.SaveChanges();
            return NoContent();
        }



        // PUT api/TagGroup/5/TagLink
        [HttpPut("{id}/TagLink")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTagLink(int id, List<int> TagIds)
        {
            List<TagItem> ListTagItems = GetListTagItem(TagIds);
            var tagGroup = await _dbContext.TagGroups.FindAsync(id);
            if (tagGroup == null) return BadRequest();
            if (tagGroup.TagItems != null) if(tagGroup.TagItems.Count > 0) ListTagItems = (List<TagItem>)ListTagItems.Union(tagGroup.TagItems);

            tagGroup.TagItems = ListTagItems;
            _dbContext.Entry(tagGroup).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        
        private List<TagItem> GetListTagItem(List<int> TagIds)
        {
            List<TagItem> ListTagItems = new List<TagItem>();
            foreach (int tagId in TagIds)
            {
                var tagItem = _dbContext.TagItems.Find(tagId);
                if (tagItem != null)
                {
                    ListTagItems.Add(tagItem);
                }
            }
            return ListTagItems;
        }

    }
}
