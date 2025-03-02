using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SignalRPractice.Models;

namespace SignalRPractice.Data
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        public DbSet<Chat> Chats { get; set; } = default!;
        public Context(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Chat>()
                .HasOne(a => a.sender)
                .WithMany(a => a.ChatMesgsSender)
                .HasForeignKey(a => a.senderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<Chat>()
                .HasOne(a => a.reciever)
                .WithMany(a => a.ChatMesgsReciever)
                .HasForeignKey(a => a.recieverId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole() { Id = "fdas-LWQ2321-Lffwe-GKlher", Name = "Admin", NormalizedName = "Admin" },
                    new IdentityRole() { Id = "ldfs-QWfdsh2-Lgerq-LGwqer", Name = "GEN", NormalizedName = "GEN" }
                );

            var user1 = new IdentityUser() { Id = "flds-ga3sfe-half-Lwlgfd", Email = "ridham@gmail.com", NormalizedEmail = "ridham@gmail.com", UserName = "Ridham", NormalizedUserName = "Ridham" };

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();

            user1.PasswordHash = passwordHasher.HashPassword(user1, "Ri@973#dham");

            var user2 = new IdentityUser() { Id = "glsd-ga23wr-ghaf-fdswry", Email = "ram@gmail.com", NormalizedEmail = "ram@gmail.com", NormalizedUserName = "Ram", UserName = "Ram" };

            user2.PasswordHash = passwordHasher.HashPassword(user2, "Ram@123#");

            var user3 = new IdentityUser() { Id = "gwly-ye36dw-hame-hsdrgh", Email = "om@gmail.com", NormalizedEmail = "om@gmail.com", NormalizedUserName = "Om", UserName = "Om" };

            user3.PasswordHash = passwordHasher.HashPassword(user3, "OM@123#");

            builder.Entity<IdentityUser>()
                .HasData(
                    user1,
                    user2,
                    user3
                );


            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>() { UserId = "flds-ga3sfe-half-Lwlgfd", RoleId = "fdas-LWQ2321-Lffwe-GKlher" },
                    new IdentityUserRole<string>() { UserId = "glsd-ga23wr-ghaf-fdswry", RoleId = "ldfs-QWfdsh2-Lgerq-LGwqer" },
                    new IdentityUserRole<string>() { UserId = "gwly-ye36dw-hame-hsdrgh", RoleId = "ldfs-QWfdsh2-Lgerq-LGwqer" }
                );
        }
    }
}
