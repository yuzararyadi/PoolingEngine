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
    public class PoolingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PoolingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var requestItems = _unitOfWork.RequestItem.GetAllwithChild(x => x.TagGroups);
            var requestItemsDto = _mapper.Map<List<RequestItemDto>>(requestItems);
            return Ok(requestItemsDto);
        }


        [HttpPost]
        public IActionResult CreatePoolingReqeust(RequestPoolingDto requestPoolingDtos)
        {
            if (requestPoolingDtos == null) return BadRequest();
            var requestPooling = _mapper.Map<RequestPooling>(requestPoolingDtos);
            if(!_unitOfWork.DeviceItem.GetAll().Any(x => requestPooling.DeviceItemIds.Contains(x.Id)))
                return NotFound();
            List<TagGroup> tagGroups = new List<TagGroup>();
            if (requestPooling.TagGroupIds == null) return BadRequest();

            tagGroups = _unitOfWork.TagGroup.Find(x => requestPooling.TagGroupIds.Contains(x.Id)).ToList();
            if (tagGroups.Count == 0) return BadRequest();

            var requestItemsDto = _mapper.Map<List<RequestItemDto>>(_unitOfWork.RequestItem.PopulateRequestItem(requestPooling, tagGroups));
            _unitOfWork.InMemorySave();
            return CreatedAtAction(nameof(CreatePoolingReqeust), requestItemsDto);
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(Guid id) 
        {
            var requestItem = _unitOfWork.RequestItem.GetAll().FirstOrDefault(x => x.Id == id);
            if (requestItem == null) return NotFound();
            _unitOfWork.RequestItem.Remove(requestItem);
            _unitOfWork.InMemorySave();
            return Ok(id);
        }
    }
}
