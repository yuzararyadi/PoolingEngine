using AutoMapper;
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
    public class TagItemsController : ControllerBase
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public TagItemsController(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        { 
            var tagItems = _mapper.Map<List<TagItemDto>>(_dbContext.TagItems.OrderBy(x => x.Id).ToList());
            return tagItems == null ? NotFound() : Ok(tagItems);
        }
        // GET: api/ManageTagItems


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TagItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            //TagItem item = new TagItem();
            var TagItem = _mapper.Map<TagItemDto>(await _dbContext.TagItems.FindAsync(id));
            return TagItem == null ? NotFound() : Ok(TagItem);
        }

        // POST api/ManageTagItems
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateTag(TagItemDto tagItemDto)
        {
            TagItem tagItem = new TagItem
            {
                Name = tagItemDto.Name,
                Description = tagItemDto.Description,
                DataType = tagItemDto.DataType
            };

            await _dbContext.AddAsync(tagItem);
            await _dbContext.SaveChangesAsync();
            tagItemDto.Id = tagItem.Id;
            return CreatedAtAction(nameof(CreateTag), new {id = tagItem.Id}, tagItemDto);
        }

        // PUT api/ManageTagItems/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, TagItemDto tagItemDto)
        {
            if (id != tagItemDto.Id) return BadRequest();
            var tagItem = _dbContext.TagItems.Find(id);
            if (tagItem == null) return NotFound();

            tagItem.Name = tagItemDto.Name;
            tagItem.Description = tagItemDto.Description;

            _dbContext.Entry(tagItem).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
 

        // DELETE api/ManageTagItems/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var tagItemToDelete = await _dbContext.TagItems.FindAsync(id);
            if (tagItemToDelete == null) return NotFound();

            _dbContext.TagItems.Remove(tagItemToDelete);
            await _dbContext.SaveChangesAsync();
               
            return NoContent();
        }
    }
}
