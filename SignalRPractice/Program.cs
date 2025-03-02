
using SignalRPractice.Hubs;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Identity;
using SignalRPractice.Data;
using Microsoft.EntityFrameworkCore;
using SignalRPractice.Repository.IRepo;
using SignalRPractice.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SignalRPractice.Service;

var builder = WebApplication.CreateBuilder(args);
string localIP = LocalIPAddress();

// Add services to the container.
builder.Services.AddCors(ops => ops.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
));

builder.Services.AddDbContext<Context>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthentication(ops => ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(ops =>
    {
        ops.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

//app.Urls.Add("http://" + localIP + ":7008");
app.UseHttpsRedirection();
app.MapControllers();

app.MapHub<UserHub>("/hubs/userCount");
app.MapHub<ChatHub>("/hubs/chat");

app.Run();


static string LocalIPAddress()
{
    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
    {
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint? endPoint = socket.LocalEndPoint as IPEndPoint;
        if (endPoint != null)
        {
            return endPoint.Address.ToString();
        }
        else
        {
            return "127.0.0.1";
        }
    }
}