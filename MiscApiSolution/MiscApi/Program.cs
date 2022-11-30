using MiscApi.Adapters;

namespace MiscApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.

        // Lazy Version
        builder.Services.AddSingleton<ISystemTime, SystemTime>();
        builder.Services.AddScoped<IProvideLogging, LoggingAdapter>();

        // Lazy, but with a factory
        //builder.Services.AddSingleton<ISystemTime>(f =>
        //{
        //    // do whatever work you need to set this
        //    return new SystemTime();
        //});

        // Eager creation.
        //var UseThisForTheClock = new SystemTime(); // 300 ms
        //builder.Services.AddSingleton<ISystemTime>(UseThisForTheClock);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}