using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolingEngine.API.Dtos;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;

namespace PoolingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagGroupController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagGroupController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tagGroups = _unitOfWork.TagGroup.GetAllwithChild(x => x.TagItems);
            var tagGroupsDto = _mapper.Map<List<TagGroupDto>>(tagGroups);
            return Ok(tagGroupsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var tagGroup = _unitOfWork.TagGroup.GetAllwithChild(x => x.TagItems).Where(x => x.Id == id);
            if(tagGroup == null) return NotFound();
            var tagGroupDto = _mapper.Map<TagGroupDto>(tagGroup);
            return Ok(tagGroupDto);
        }

        [HttpPost]
        public IActionResult Add(TagGroupDto tagGroupDto)
        {
            if (tagGroupDto == null) return BadRequest();
            var tagGroup = _mapper.Map<TagGroup>(tagGroupDto);
            _unitOfWork.TagGroup.Add(tagGroup);
            _unitOfWork.Save();
            tagGroupDto.Id = tagGroup.Id;
            return CreatedAtAction(nameof(GetbyId), new {id = tagGroup.Id}, tagGroupDto);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var tagGroup = _unitOfWork.TagGroup.GetById(id);
            if (tagGroup == null) return NotFound(); 
            _unitOfWork.TagGroup.Remove(tagGroup);
            _unitOfWork.Save();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, TagGroupDto tagGroupDto)
        {
            if(id != tagGroupDto.Id) return BadRequest();
            var tagGroup = _mapper.Map<TagGroup>(tagGroupDto);
            if(_unitOfWork.TagGroup.UpdateById(id,tagGroup)== false) return NotFound();
            _unitOfWork.Save();
            return Ok(tagGroupDto);
        }
        [HttpPut("{id}/linkTagItem")]
        public IActionResult LinkTagItem(int id, List<int> tagItemIds)
        {
            var tagGroup = _unitOfWork.TagGroup.GetById(id);
            if (tagGroup == null) return NotFound();
            List<TagItem> tagItems = new List<TagItem>();
            foreach(int tagItemId in tagItemIds)
            {
                var tagItem = _unitOfWork.TagItem.GetById(tagItemId);
                if (tagItem != null) tagItems.Add(tagItem);
            }
            _unitOfWork.TagGroup.UpdateLinkTagItems(tagGroup, tagItems);
            _unitOfWork.Save();
            return Ok(tagGroup);
        }
    }
}
