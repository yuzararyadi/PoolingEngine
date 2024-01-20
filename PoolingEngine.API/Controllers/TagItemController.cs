using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolingEngine.API.Dtos;
using PoolingEngine.DataAccess.Implementation;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;

namespace PoolingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var tagItems = _unitOfWork.TagItem.GetAll();
            var tagItemsDto = _mapper.Map<List<TagItemDto>>(tagItems);
            return Ok(tagItemsDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        { 
            var tagItem = _unitOfWork.TagItem.GetById(id);
            if (tagItem == null) return NotFound();
            var tagItemDto = _mapper.Map<TagItemDto>(tagItem);
            return Ok(tagItemDto);
        }
        [HttpPost]
        public IActionResult Add(TagItemDto tagItemDto)
        {
            if(tagItemDto == null) return BadRequest();
            var tagItem = _mapper.Map<TagItem>(tagItemDto);
            _unitOfWork.TagItem.Add(tagItem);
            _unitOfWork.Save();
            tagItemDto.Id = tagItem.Id;
            return CreatedAtAction(nameof(GetById), new { id = tagItem.Id }, tagItemDto);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var tagItem = _unitOfWork.TagItem.GetById(id);
            if (tagItem == null) return NotFound();

            var childTagDeftoDel = _unitOfWork.TagDef.GetAllwithChild(x=>x.TagItem).Where(x=>x.TagItem == tagItem).ToList();
            if (childTagDeftoDel.Count > 0) _unitOfWork.TagDef.RemoveRange(childTagDeftoDel);
            _unitOfWork.TagItem.Remove(tagItem);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, TagItemDto tagItemDto) 
        { 
            if(id != tagItemDto.Id) return BadRequest();
            var tagItem = _mapper.Map<TagItem>(tagItemDto);
            if (_unitOfWork.TagItem.UpdateById(id, tagItem) == false) return NotFound();
            _unitOfWork.Save();
            return Ok(tagItemDto);
        }
    }
}
