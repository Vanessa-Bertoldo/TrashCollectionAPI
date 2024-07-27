using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using System.Text;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Data;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Repositorios
builder.Services.AddScoped<IColetaRepository, ColetaRepository>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Services
builder.Services.AddScoped<IColetaService, ColetaService>();
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<RabbitMQService>();
#endregion

#region AutoMapper
var mapperConfig = new MapperConfiguration(config =>
{
    config.AllowNullCollections = true;
    config.AllowNullDestinationValues = true;

    config.CreateMap<ColetaModel, ColetaViewModel>().ReverseMap();
    config.CreateMap<RotaModel, RotaViewModel>().ReverseMap();
    config.CreateMap<CaminhaoModel, CaminhaoViewModel>().ReverseMap();
    config.CreateMap<StatusModel, StatusViewModel>().ReverseMap();
    config.CreateMap<UserModel, UserViewModel>().ReverseMap();
    config.CreateMap<AuthModel, AuthViewModel>().ReverseMap();
    config.CreateMap<TokenUserModel, TokenUserViewModel>().ReverseMap();
    config.CreateMap<LoginViewModel, AuthModel>()
           .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Usuario))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Senha))
           .ReverseMap()
           .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Username))
           .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Password));
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region Autenticação
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion

#region RabbitMQ
builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    return new ConnectionFactory()
    {
        HostName = configuration["RabbitMQ:HostName"],
        Port = int.Parse(configuration["RabbitMQ:Port"]),
        UserName = configuration["RabbitMQ:UserName"],
        Password = configuration["RabbitMQ:Password"]
    };
});
builder.Services.AddScoped<RabbitMQService>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de solicitação HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<DatabaseContext>();
        InitialData.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro durante a inicialização do banco de dados.");
    }
}

app.MapControllers();

app.Run();
