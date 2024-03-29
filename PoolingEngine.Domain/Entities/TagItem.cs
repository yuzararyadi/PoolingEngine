﻿using System.ComponentModel.DataAnnotations;
using PoolingEngine.Domain.Enum;

namespace PoolingEngine.Domain.Entities
{
    public class TagItem
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }
        public EnumDataType DataType { get; set; }

        public ICollection<TagGroup>? TagGroups { get; set; } = new HashSet<TagGroup>();
        public ICollection<TagDef>? tagDefs { get; set; } = new HashSet<TagDef>();
    }

}
