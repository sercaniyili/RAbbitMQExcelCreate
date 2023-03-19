using FileCreateWorkersService;
using FileCreateWorkersService.Models;
using FileCreateWorkersService.Services;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx,services) =>
    {

        services.AddSingleton<RabbitMQClientService>();

        services.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlServer(ctx.Configuration.GetConnectionString("SqlServer"));
        });
   
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
