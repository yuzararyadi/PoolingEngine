using Microsoft.EntityFrameworkCore;
using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Entities;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class TagGroupRepository : GenericRepository<TagGroup>, ITagGroupRepository
    {
        private readonly AppDbContext _dbcontext;

        public TagGroupRepository(AppDbContext context) : base(context)
        {
            _dbcontext = context;
        }


        public void UpdateLinkTagItems(TagGroup tagGroup, List<TagItem> tagItems)
        {
            var evalTagGroup = _dbcontext.TagGroups
                .Include(x => x.TagItems)
                .FirstOrDefault(g => g.Id == tagGroup.Id);

            evalTagGroup.TagItems.Clear();

            // add the new items
            foreach (var tagItem in tagItems)
            {
                evalTagGroup.TagItems.Add(tagItem);
            }
        }
        public List<TagItem> LinkedTagItems(List<TagGroup> tagGroups)
        {
            List<TagItem> tagItems = new List<TagItem>();
           
            foreach (var tagGroup in _dbcontext.TagGroups.Where(x => tagGroups.Contains(x)).Include(y => y.TagItems))
            { 
                tagItems.AddRange(tagGroup.TagItems);
            };
            return tagItems;
        }

    }
}
