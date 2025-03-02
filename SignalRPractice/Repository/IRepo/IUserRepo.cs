using Microsoft.AspNetCore.Identity;

namespace SignalRPractice.Repository.IRepo
{
    public interface IUserRepo
    {
        IQueryable<IdentityUser> GetContacts(string userId);
    }
}
