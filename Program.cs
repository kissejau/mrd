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
System.Diagnostics.Debug.WriteLine("Debug message");

app.Run();

void RegisterServicies(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();

    services.AddTransient<IApi, UserApi>();
    services.AddTransient<IApi, PostApi>();
    services.AddTransient<IApi, ReplyApi>();

    services.AddTransient<DAO<User>, UserDAO>();
    services.AddTransient<DAO<Post>, PostDAO>();

    services.AddCors(opt =>
    {
        opt.AddPolicy("origins", policy =>
        {
            policy.WithOrigins("https://restninja.io").AllowAnyHeader();
        });
    });

    services.AddSwaggerGen();
}

void Configure(WebApplication app)
{
    app.UseCors("origins");

    app.UseHttpsRedirection();

    app.UseSwagger();
    app.UseSwaggerUI();
}
