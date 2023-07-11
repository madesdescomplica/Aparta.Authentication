using Aparta.Authentication.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAppConnection()
    .AddUseCases()
    .AddAndConfigureControllers();


var app = builder.Build();
app.UseDocumentation();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { };
