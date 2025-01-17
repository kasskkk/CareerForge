using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public interface IUserInfoService
    {
        Task<IEnumerable<UserInfo>> GetAllAsync();
        Task<UserInfo> GetByIdAsync(int id);
        Task AddAsync(UserInfo entity);
        Task UpdateAsync(UserInfo entity);
        Task DeleteAsync(int id);
    }
}
