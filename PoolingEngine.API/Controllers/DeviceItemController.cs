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
    public class DeviceItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeviceItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var deviceItems = _unitOfWork.DeviceItem.GetAllwithChild(x => x.TagGroups);
            var deviceItemsDto = _mapper.Map<List<DeviceItemDto>>(deviceItems);
            return Ok(deviceItemsDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            DeviceItem deviceItem = _unitOfWork.DeviceItem.GetAllwithChild(x => x.TagGroups).Where(y => y.Id == id).FirstOrDefault();
            if (deviceItem == null) return NotFound();
            var deviceItemDto = _mapper.Map<DeviceItemDto>(deviceItem);
            return Ok(deviceItemDto);
        }

        [HttpPost]
        public IActionResult Add(DeviceItemDto deviceItemDto) 
        {
            if (deviceItemDto == null) return BadRequest();
            
            var deviceItem = _mapper.Map<DeviceItem>(deviceItemDto);
            _unitOfWork.DeviceItem.Add(deviceItem);
            _unitOfWork.Save();
            deviceItemDto.Id = deviceItem.Id;
            return CreatedAtAction(nameof(GetById), new {id = deviceItem.Id},deviceItemDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var deviceItem = _unitOfWork.DeviceItem.GetById(id);
            if (deviceItem == null) return NotFound();
            _unitOfWork.DeviceItem.Remove(deviceItem);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(int id, DeviceItemDto deviceItemDto)
        {
            if (id != deviceItemDto.Id) return BadRequest();
            var deviceItem = _mapper.Map<DeviceItem>(deviceItemDto);
            if(_unitOfWork.DeviceItem.UpdateById(id, deviceItem) == false) return NotFound();
            _unitOfWork.Save();
            return Ok(deviceItemDto);
        }

        [HttpPut("{id}/linkTagGroup")]
        public IActionResult LinkTagGroup(int id, List<int> tagGroupIds)
        {
            var deviceItem = _unitOfWork.DeviceItem.GetById(id);
            if (deviceItem == null) return NotFound();
            List<TagGroup> tagGroups = new List<TagGroup>();
            foreach (int tagGroupId in tagGroupIds)
            {
                var tagGroup = _unitOfWork.TagGroup.GetById(tagGroupId);
                if (tagGroup != null) tagGroups.Add(tagGroup);
            }
            _unitOfWork.DeviceItem.updatelinkTagGroups(deviceItem, tagGroups);
            _unitOfWork.Save();
            return Ok();
        }

    }
}
