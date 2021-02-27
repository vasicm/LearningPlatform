using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VocabularyBooster.Core.GraphModel;

namespace VocabularyBooster.Service
{
    public interface IUserService
    {
        Task<Guid> AddUser(User user);

        Task<User> GetUser(Guid userUuid);

        Task<User> GetUserByEmail(string email);
    }
}
