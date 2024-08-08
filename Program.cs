using Quartz;
using Quartz.Impl;
using projMaxPark.BL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddSingleton<SmartAlgorithm>();

// Configure Quartz.NET
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Create a job
    var jobKey = new JobKey("DailyAlgorithmJob");
    q.AddJob<DailyAlgorithmJob>(opts => opts.WithIdentity(jobKey));

    // Create a trigger to run at 6:00 AM and 8:00 PM every day
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DailyAlgorithmTrigger")
        .WithCronSchedule("0 0 6,20 * * ?")); // Cron expression for 6:00 AM and 8:00 PM
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
