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
    public class TagDefController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TagDefController(IUnitOfWork unitOfWork, IMapper mapper)
        {
           _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tagDefs = _unitOfWork.TagDef.GetAllwithChild(x => x.DeviceItem, y => y.TagItem);
            var tagDefsDto = _mapper.Map<List<TagDefDto>>(tagDefs);
            return Ok(tagDefsDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            var tagDef = _unitOfWork.TagDef.GetAllwithChild(x => x.DeviceItem, y => y.TagItem).Where(z => z.Id == id).FirstOrDefault();
            if (tagDef == null) return NotFound();
            var tagDefDto = _mapper.Map<TagDefDto>(tagDef);
            return Ok(tagDefDto);
        }

        [HttpPost]
        public IActionResult Add(TagDefDto tagDefDto)
        {
            if (tagDefDto != null)
                if (tagDefDto.DeviceItemId != null && tagDefDto.TagItemId != null && tagDefDto.MapAddress != string.Empty)
                {
                    //var tagDef = _mapper.Map<TagDef>(tagDefDto);
                    var tagDefCheck = _unitOfWork.TagDef.GetAllwithChild(x => x.DeviceItem, y => y.TagItem)
                        .Where(z => (z.DeviceItem.Id == tagDefDto.DeviceItemId && z.TagItem.Id == tagDefDto.TagItemId)).FirstOrDefault();
                    
                    if (tagDefCheck == null)
                    {
                        //add new
                        TagDef tagDef = new TagDef()
                        {
                           DeviceItem = _unitOfWork.DeviceItem.GetById(tagDefDto.DeviceItemId),
                           TagItem = _unitOfWork.TagItem.GetById(tagDefDto.TagItemId),
                           MapAddress = tagDefDto.MapAddress

                        };
                        _unitOfWork.TagDef.Add(tagDef);
                        tagDefDto.Id = tagDef.Id;
                    }
                    else
                    {
                        //update existing
                        tagDefCheck.MapAddress = tagDefDto.MapAddress;
                        _unitOfWork.TagDef.UpdateById(tagDefCheck.Id, tagDefCheck);
                        tagDefDto.Id = tagDefCheck.Id;
                    }
                    _unitOfWork.Save();
                    
                    return CreatedAtAction(nameof(GetById), new { id = tagDefDto.Id }, tagDefDto);
                }
            return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var tagDef = _unitOfWork.TagDef.GetById(id);
            if (tagDef == null) return NotFound();
            _unitOfWork.TagDef.Remove(tagDef);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, TagDefDto tagDefDto)
        {
            if(id != tagDefDto.Id) return BadRequest();
            var tagDef = _mapper.Map<TagDef>(tagDefDto);
            if(_unitOfWork.TagDef.UpdateById(id, tagDef)==false) return NotFound();
            _unitOfWork.Save();
            return Ok(tagDefDto);
        }

    }
}
