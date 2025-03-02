using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SignalRPractice.Controllers;
using SignalRPractice.DTO;
using SignalRPractice.Service;

namespace Test
{
    public class AuthControllerTests
    {
        private Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(
            userStoreMock.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        }

        [Fact]
        public async void Should_Get_Ok_Result()
        {
            var ConfigMock = new Mock<IConfiguration>();

            string email = "Ram@gmail.com";
            string password = "Ram@123#";

            var TokenServiceMock = new Mock<TokenService>(ConfigMock.Object);
            var UserManagerMock = GetMockUserManager();

            var user = new IdentityUser() { Id = "glsd-ga23wr-ghaf-fdswry", Email = email, UserName = "Ram" };


            UserManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);

            UserManagerMock.Setup(x => x.CheckPasswordAsync(user, password)).ReturnsAsync(true);

            IList<string> Roles = new List<string>() { "GEN" };

            ConfigMock.Setup(ops => ops["Jwt:Audience"]).Returns("http://localhost:4200");
            ConfigMock.Setup(ops => ops["Jwt:Issuer"]).Returns("https://localhost:7007");
            ConfigMock.Setup(ops => ops["Jwt:SecretKey"]).Returns("fdslaOIWERnglasmfslwobgSOreqwjglsrwdqwLgwoemfwdQWGwe");

            UserManagerMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(Roles);

            //TokenServiceMock.Setup(x => x.GenerateToken(user, new List<string>() { "GEN" })).Returns("DUMMYTOKEN");

            var AuthCtrl = new AuthController(tokenService: TokenServiceMock.Object,
               userManager: UserManagerMock.Object);

            var result = await AuthCtrl.Login(new UserLogin(email, password));

            Assert.NotNull(result);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Should_Get_NotFound_Result()
        {
            var UserManagerMock = GetMockUserManager();

            var ConfigMock = new Mock<IConfiguration>();

            var TokenServiceMock = new Mock<TokenService>(ConfigMock.Object);

            var email = "facke@email.com";
            var password = "Fake@123";

            var AuthCtrl = new AuthController(userManager: UserManagerMock.Object, tokenService: TokenServiceMock.Object);

            var result = await AuthCtrl.Login(new UserLogin(email, password));

            Assert.NotNull(result);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void Should_Get_BadRequest_Result()
        {
            var ConfigMock = new Mock<IConfiguration>();
            var UserManagerMock = GetMockUserManager();
            var TokenServiceMock = new Mock<TokenService>(ConfigMock.Object);

            var email = "Ram@gmail.com";
            var password = "Fake@123";

            var user = new IdentityUser()
            {
                Email = email,
            };

            UserManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            
            var AuthCtrl = new AuthController(userManager:UserManagerMock.Object,tokenService:TokenServiceMock.Object);

            var result = await AuthCtrl.Login(new UserLogin(email,password));

            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
