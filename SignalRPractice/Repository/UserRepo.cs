using Microsoft.AspNetCore.Identity;
using SignalRPractice.Data;
using SignalRPractice.Repository.IRepo;

namespace SignalRPractice.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepo(Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IQueryable<IdentityUser> GetContacts(string userId)
        {
            return _userManager.Users.Where(a => a.Id != userId);
        }
    }
}
