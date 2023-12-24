﻿
using PoolingEngine.Domain.Enum;

namespace PoolingEngine.API.Dtos
{
    public class TagItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public EnumDataType DataType { get; set; }
    }

}
