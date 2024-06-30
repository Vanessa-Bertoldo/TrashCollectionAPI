using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrashCollectionAPI.Data.Contexts;
using TrashCollectionAPI.Data.Repository;
using TrashCollectionAPI.Models;
using TrashCollectionAPI.Services;
using TrashCollectionAPI.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
#endregion

#region Services
builder.Services.AddScoped<IColetaService, ColetaService>();
//builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
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

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
