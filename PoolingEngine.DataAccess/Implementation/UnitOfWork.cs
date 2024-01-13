using PoolingEngine.DataAccess.Context;
using PoolingEngine.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolingEngine.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbcontext;
        private readonly InMemoryDbContext _inMemoryDbContext;

        public UnitOfWork(AppDbContext context, InMemoryDbContext IMContext)
        {
            _dbcontext = context;
            _inMemoryDbContext = IMContext;
            DeviceItem = new DeviceItemRepository(_dbcontext);
            //RequestItem = new RequestItemRepository(_inMemoryDbContext);
            TagGroup = new TagGroupRepository(_dbcontext);
            TagItem = new TagItemRepository(_dbcontext);
            TagValue = new TagValueRepository(_dbcontext);
            TagDef = new TagDefRepository(_dbcontext);
        }
        public IDeviceItemRepository DeviceItem { get; private set; }

        //public IRequestItemRepository RequestItem { get; private set; }

        public ITagGroupRepository TagGroup { get; private set; }

        public ITagItemRepository TagItem { get; private set; }

        public ITagValueRepository TagValue { get; private set; }
        public ITagDefRepository TagDef { get; private set; }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public int Save()
        {
            return _dbcontext.SaveChanges();
        }
        public int InMemorySave()
        {
            return _inMemoryDbContext.SaveChanges();
        }
    }
}
