﻿using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface ITagDefRepository : IGenericRepository<TagDef>
    {
        List<TagDef> ListDeviceTagDef(DeviceItem deviceItem, List<TagItem> tagItems);
    }

}
