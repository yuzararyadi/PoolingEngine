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
        private IRequestItemRepository _requestItemRepository;

        public PoolingController(IUnitOfWork unitOfWork, IMapper mapper, IRequestItemRepository requestItemRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestItemRepository = requestItemRepository; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<RequestItem> requestItems = _requestItemRepository.RequestItems();
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

            var requestItemsDto = _mapper.Map<List<RequestItemDto>>(_requestItemRepository.PopulateRequestItem(requestPooling, tagGroups));
            //_unitOfWork.InMemorySave();
            return CreatedAtAction(nameof(Get), requestItemsDto);
        }

        [HttpDelete("{id}")] 
        public IActionResult Delete(Guid id) 
        {
            var requestItem = _requestItemRepository.RequestItems()
                .Where(x => x.Id == id).FirstOrDefault();
                
            if (requestItem == null) return NotFound();
            _requestItemRepository.Remove(requestItem);
            return Ok(id);
        }
    }
}
