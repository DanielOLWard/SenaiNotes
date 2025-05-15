using SenaiNotes.Context;
using SenaiNotes.Interfaces;
using SenaiNotes.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SenaiNotesContext>();
builder.Services.AddTransient<IUsuariorepository, UsuarioRepository>();
builder.Services.AddTransient<ITagRepository, TagRepository>();
builder.Services.AddTransient<ITagNotasRepository, TagNotasRepository>();
 
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: "minhasOrigens", 
            policy =>
            {
                // TODO: Alterar Link para Frontend
                policy.WithOrigins("http://localhost:5500");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
   
            });
    });

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options => // Faz o Swagger abrir direto
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors("minhasOrigens"); // sempre estar por cima do MapControllers

app.MapControllers();

app.UseAuthentication(); 

app.UseAuthorization(); 

app.Run();
