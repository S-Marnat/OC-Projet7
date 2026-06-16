using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Middlewares;
using P7CreateRestApi.Repositories.Interfaces;
using P7CreateRestApi.Services;
using P7CreateRestApi.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Ajout des contrôleurs MVC
builder.Services.AddControllers();

// Injection des services et repositories
builder.Services.AddScoped<IBidListService, BidListService>();
builder.Services.AddScoped<IBidListRepository, BidListRepository>();
builder.Services.AddScoped<ICurvePointService, CurvePointService>();
builder.Services.AddScoped<ICurvePointRepository, CurvePointRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRuleNameService, RuleNameService>();
builder.Services.AddScoped<IRuleNameRepository, RuleNameRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

// Permet d'accéder à l'utilisateur authentifié dans les services
builder.Services.AddHttpContextAccessor();

// Configuration de Swagger + sécurité JWT dans l'interface Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Entrez votre token"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuration du DbContext avec SQL Server
builder.Services.AddDbContext<LocalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration d’Identity
builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
{
    // Configuration des règles de mot de passe
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<LocalDbContext>()
.AddDefaultTokenProviders();

// Ajout de JWT
string? key = configuration["Jwt:Key"]
           ?? Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

if (string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException("Jwt:Key", "JWT Key configuration is missing.");
}

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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ClockSkew = TimeSpan.Zero
    };
});

// Activation de l’autorisation
builder.Services.AddAuthorization();

var app = builder.Build();

// Activation de Swagger uniquement en environnement de développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware personnalisé pour logger les requêtes
app.UseMiddleware<RequestLoggingMiddleware>();

// Activation de l’authentification et de l’autorisation
app.UseAuthentication();
app.UseAuthorization();

// Redirection automatique vers HTTPS
app.UseHttpsRedirection();

// Mapping des contrôleurs
app.MapControllers();

// Démarrage de l’application
app.Run();
