using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Repositories;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddSwaggerGen(
options =>
{
    options.EnableAnnotations();
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
}
);

builder.Services.AddDbContext<SenaiNotesContext>();
builder.Services.AddTransient<IUsuariorepository, UsuarioRepository>();
builder.Services.AddTransient<INotaRepository, NotaRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ITagNotasRepository, TagNotasRepository>();
builder.Services.AddTransient<ITipoUsuarioRepository, TipoUsuarioRepository>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SenaiNotes",
            ValidAudience = "SenaiNotes",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minha-chave-ultra-mega-secreta-de-seguranca-do-projeto-senai-notes-o-projeto-final-do-senai-o-ultimo-mesmo-eu-juro"))
        };
    });

builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: "minhasOrigens",
            policy =>
            {
                policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
    });


var app = builder.Build();

app.UseCors("minhasOrigens");


app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(options => // Faz o Swagger abrir direto 
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

if (!Directory.Exists(pastaDestino)) 
    Directory.CreateDirectory(pastaDestino);

app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(pastaDestino),
        RequestPath = "/image"
    }
);


app.Run();
