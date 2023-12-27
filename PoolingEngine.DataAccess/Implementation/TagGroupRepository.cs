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

            var linkedTagItems = evalTagGroup.TagItems.ToList();

            foreach (var linkedTagItem in linkedTagItems)
            {
                var tagItem = tagItems.Where(x => x.Id == linkedTagItem.Id).FirstOrDefault();
                if (tagItem != null) 
                    _dbcontext.Entry(linkedTagItem).CurrentValues.SetValues(tagItem);
                else 
                    _dbcontext.Remove(linkedTagItem);
            }

            foreach (var tagItem in tagItems)
            {
                if (linkedTagItems.All(x => x.Id != tagItem.Id))
                    evalTagGroup.TagItems.Add(tagItem);
            }
        }

    }
}
