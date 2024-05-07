using SocialNetwork.DAL.Interfaces;
using SocialNetwork.Domain.Entity;

namespace SocialNetwork.DAL.Repositories
{
    public class MessageRepository : IBaseRepository<MessageEntity>
    {
        private AppDbContext _appDbContext;

        public MessageRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(MessageEntity entity)
        {
            await _appDbContext.Messages.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(MessageEntity entity)
        {
            _appDbContext.Messages.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<MessageEntity> GetAll()
        {
            return _appDbContext.Messages;
        }

        public async Task<MessageEntity> Update(MessageEntity entity)
        {
            _appDbContext.Messages.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Attach(MessageEntity entity)
        {
            _appDbContext.Messages.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
