using SocialNetwork.DAL.Interfaces;
using SocialNetwork.Domain.Entity;

namespace SocialNetwork.DAL.Repositories
{
    public class UserRepository : IBaseRepository<UserEntity>
    {
        private AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext) =>
            _appDbContext = appDbContext;

        public async Task Create(UserEntity entity)
        {
            await _appDbContext.Users.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(UserEntity entity)
        {
            _appDbContext.Users.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<UserEntity> GetAll()
        {
            return _appDbContext.Users;
        }

        public async Task<UserEntity> Update(UserEntity entity)
        {
            _appDbContext.Users.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Attach(UserEntity entity)
        {
            _appDbContext.Users.Attach(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
