using Film_Rating_System.Models;
using Film_Rating_System.Models.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ≈÷«›… CORS ··”„«Õ »«·Ê’Ê· «·⁄«„
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()  // «·”„«Õ »√Ì „’œ—
                  .AllowAnyMethod()  // «·”„«Õ »√Ì ÿ—Ìﬁ… (GET, POST, PUT, DELETE)
                  .AllowAnyHeader(); // «·”„«Õ »√Ì —√” (headers)
        });
});

// =====================
// 1. ≈⁄œ«œ ﬁ«⁄œ… «·»Ì«‰« 
// =====================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================
// 2. Õﬁ‰ «·„” Êœ⁄«  (Repositories)
// =====================
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Movie>, MovieRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IFileService, FileService>();

// =====================
// 3. ≈⁄œ«œ «· Êﬂ‰ JWT
// =====================
var jwtKey = "b7f9c5d6e1a4d2f3c7f8e6e2d5a1b5c7f9e4d2a7f8e6f3c2f1e7a6b8f9c5d4";
var issuer = "YourIssuer";
var audience = "YourAudience";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        ValidAudience = audience,

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// =====================
// 4.  ›⁄Ì· «·’·«ÕÌ«  (Authorization)
// =====================
builder.Services.AddAuthorization();

// =====================
// 5. ≈÷«›… MVC ÊŒœ„«  Swagger
// =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =====================
// 6. ≈⁄œ«œ „Ìœ· ÊÌ— «· ÿ»Ìﬁ
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Â‰«°  √ﬂœ „‰ √‰ﬂ  ” Œœ„ CORS √Ê·« ﬁ»· «·„’«œﬁ… Ê«·‹ Authorization
app.UseCors("AllowAll");  //  ÿ»Ìﬁ ”Ì«”… CORS

app.UseAuthentication(); // ?? ÌÃ» √‰ ÌﬂÊ‰ ﬁ»· UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
