using SocialFilm.Application;
using SocialFilm.Infrastructure;
using SocialFilm.Persistance;
using SocialFilm.Presentation;
using SocialFilm.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddApplicationPart(typeof(SocialFilm.Presentation.AssemblyReference).Assembly);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddPersistance(builder.Configuration)
    .AddInfrastructure();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddlewareExtensions();

app.UseAuthorization();

app.MapControllers();

app.Run();
