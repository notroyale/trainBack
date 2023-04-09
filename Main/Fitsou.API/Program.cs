using Auth;
using Fitsou.API;
using Fitsou.API.Middleware;
using Fitsou.API.SwaggerAPI;
using Fitsou.Application;
using Infrastructure;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services
    .AddEndpointsApiExplorer()
    .AddSwagger(configuration);

services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:8132").AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
services
    .AddAPI()
    .AddApplication()
    .AddInfrastructure()
    .AddAuth(configuration)
    .AddPersistence(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");  
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();



app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//policy.AllowAnyOrigin()
//                   .AllowAnyHeader()
//                   .AllowAnyMethod().AllowCredentials();
//    });