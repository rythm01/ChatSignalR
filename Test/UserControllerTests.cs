
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SignalRPractice.Controllers;
using SignalRPractice.Repository.IRepo;

namespace Test
{

    public class UserControllerTests
    {
        private IQueryable<IdentityUser> GetUserRepoData(string userId)
        {
            return new List<IdentityUser>() { new IdentityUser() { Id = "glsd-ga23wr-ghaf-fdswry", UserName = "Ram" }, new IdentityUser() { Id = "gwly-ye36dw-hame-hsdrgh", UserName = "Om" } }.AsQueryable();
        }

        [Fact]
        public async void Get_User_Ok_Result()
        {
            string userId = "flds-ga3sfe-half-Lwlgfd";
            var userRepoMock = new Mock<IUserRepo>();
            UserController controller = new(userRepo: userRepoMock.Object);

            //Console.WriteLine(JsonSerializer.Serialize(new List<IdentityUser>() { new IdentityUser() { Id = "12345", UserName = "Ridham" } }));

            userRepoMock.Setup(repo => repo.GetContacts(userId)).Returns(GetUserRepoData(userId));

            var result = await controller.GetContacts(userId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
