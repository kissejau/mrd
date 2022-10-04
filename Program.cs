var builder = WebApplication.CreateBuilder(args);
RegisterServicies(builder.Services);

var app = builder.Build();
Configure(app);

var apis = app.Services.GetServices<IApi>();
foreach (var api in apis)
{
    if (api is null)
        throw new Exception("Api is null");
    api.Register(app);
}

app.Run();

void RegisterServicies(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();

    services.AddTransient<IApi, UserApi>();

    services.AddCors(opt =>
    {
        opt.AddPolicy("origins", policy =>
        {
            policy.WithOrigins("https://restninja.io").AllowAnyHeader();
        });
    });
}

void Configure(WebApplication app)
{
    app.UseCors("origins");

    app.UseHttpsRedirection();
}