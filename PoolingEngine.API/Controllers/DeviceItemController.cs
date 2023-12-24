using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;

namespace PoolingEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeviceItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var DeviceItems = _unitOfWork.TagItem.GetAll();
            return Ok(DeviceItems);
        }
        [HttpGet("{id}")]
        public IActionResult GetbyId(int id)
        {
            var DeviceItem = _unitOfWork.DeviceItem.GetById(id);
            return Ok(DeviceItem);
        }

        [HttpPost]
        public IActionResult Add(DeviceItem deviceItem)
        {
            if (deviceItem == null) return BadRequest();
            _unitOfWork.DeviceItem.Add(deviceItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deviceItem = _unitOfWork.DeviceItem.GetById(id);
            if (deviceItem == null) return NotFound();
            _unitOfWork.DeviceItem.Remove(deviceItem);
            return Ok();
        }
    }
}
