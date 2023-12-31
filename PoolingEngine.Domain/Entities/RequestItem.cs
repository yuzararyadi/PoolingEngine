﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PoolingEngine.Domain.Enum;

namespace PoolingEngine.Domain.Entities
{
    public class RequestItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public EnumPoolingPriority Priority { get; set; } = EnumPoolingPriority.LOW;
        [Required] 
        public DeviceItem? DeviceItem { get; set; }
        [Required] 
        public IEnumerable<TagItem> TagItem { get; set; } = new HashSet<TagItem>();
    }
}
