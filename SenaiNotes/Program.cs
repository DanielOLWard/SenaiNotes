using System.Text;
using Microsoft.IdentityModel.Tokens;
using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddSwaggerGen(options =>
{
options.EnableAnnotations();
});

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
                // TODO
                policy.WithOrigins("http://localhost:7114");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
    });

builder.Services.AddAuthentication();

var app = builder.Build();

app.UseCors("minhasOrigens");

app.UseSwagger();
app.UseSwaggerUI(options => // Faz o Swagger abrir direto
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
