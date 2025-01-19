using Domain.Generic;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepository<UserInfo> _userInfoRepository;
        public UserInfoService(IRepository<UserInfo> userInfoRepository)
        {
            _userInfoRepository = userInfoRepository;
        }
        public async Task AddAsync(UserInfo entity)
        {
            await _userInfoRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var info = await _userInfoRepository.GetByIdAsync(id);

            await _userInfoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserInfo>> GetAllAsync()
        {
            return await _userInfoRepository.GetAllAsync();
        }

        public async Task<UserInfo> GetByIdAsync(int id)
        {
            return await _userInfoRepository.GetByIdAsync(id);
        }

        public async Task<UserInfo> GetByUserIdAsync(string id)
        {
            var userInfos = await _userInfoRepository.GetAllAsync();

            if (userInfos == null)
            {
                throw new ArgumentNullException(nameof(userInfos));
            }

            var userInfo = userInfos.Where(ui => ui.UserId == id).FirstOrDefault();

            if (userInfo == null)
            {
                throw new ArgumentNullException(nameof(userInfo));
            }

            return userInfo;
        }

        public async Task UpdateAsync(UserInfo entity)
        {
            await _userInfoRepository.UpdateAsync(entity);
        }


    }
}
