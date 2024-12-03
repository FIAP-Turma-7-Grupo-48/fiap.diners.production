using Api.Extensions;
using Api.Filters;
using Api.Middlewares;
using RabbitMQ.Client;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotificationFilter>();
}).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUseCase();
builder.Services.AddInfrastructure();
builder.Services.AddControllerLayerDI();
AddRabbitMqConnectionFactory(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();



 IServiceCollection AddRabbitMqConnectionFactory(IServiceCollection services)
{
    var hostName = Environment.GetEnvironmentVariable("RabbitMqHostName");
    var port = int.Parse(Environment.GetEnvironmentVariable("RabbitMqPort"));
    var user = Environment.GetEnvironmentVariable("RabbitMqUserName");
    var password = Environment.GetEnvironmentVariable("RabbitMqPassword");

    return
        services
            .AddSingleton<IConnectionFactory>(
                new ConnectionFactory()
                {
                    HostName = hostName,
                    Port = port,
                    UserName = user,
                    Password = password
                }
            );
}
