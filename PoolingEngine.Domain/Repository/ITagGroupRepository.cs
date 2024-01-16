using PoolingEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.Domain.Repository
{
    public interface ITagGroupRepository : IGenericRepository<TagGroup>
    {
        void UpdateLinkTagItems(TagGroup tagGroup, List<TagItem> tagItems);
        List<TagItem> LinkedTagItems(List<TagGroup> tagGroups);
    }
}
