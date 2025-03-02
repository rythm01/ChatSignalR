using Microsoft.Extensions.Configuration;
using Moq;
using SignalRPractice.Service;
using Microsoft.AspNetCore.Identity;

namespace Test
{
    public class TokenServiceTests
    {
        [Fact]
        public void Should_Generate_Token()
        {
            var configMock = new Mock<IConfiguration>();

            var TokenService = new TokenService(configMock.Object);

            configMock.Setup(conf => conf["Jwt:SecretKey"]).Returns("fdslaOIWERnglasmfslwobgSOreqwjglsrwdqwLgwoemfwdQWGwe");

            configMock.Setup(conf => conf["Jwt:Audience"]).Returns("http://localhost:4200");
            configMock.Setup(conf => conf["Jwt:Issuer"]).Returns("https://localhost:7007");

            var user = new IdentityUser() { Id = "gwly-ye36dw-hame-hsdrgh", UserName = "OM", Email="Abc@gmail.com" };
            
            var result = TokenService.GenerateToken(user, new List<string>() { "Admin" });
            Assert.IsType<string>(result);
            Assert.NotNull(result);
        }
    }
}
