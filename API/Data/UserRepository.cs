using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API;

public class UserRepository : IUserRepository
{
    private readonly DataContext _dataContext;

    public UserRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await _dataContext.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string username)
    {
        return await _dataContext.Users.SingleOrDefaultAsync(user => user.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await _dataContext.Users.ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        _dataContext.Entry(user).State = EntityState.Modified;
    }
}
