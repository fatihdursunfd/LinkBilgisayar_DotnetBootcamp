using Assessment.Data;
using Assessment.Reporting;
using Assessment.Reporting.Services;
using Microsoft.EntityFrameworkCore;
using Quartz;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        services.AddDbContext<AppDbContext>(x =>
        {
            x.UseNpgsql(configuration.GetConnectionString("NpgsqlConn"));
        });

        services.AddScoped<CustomerService>();
        services.AddScoped<ExcelService>();
        services.AddScoped<UserFileService>();
        services.AddScoped<UserService>();
        services.AddScoped<EmailSenderService>();

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();

            var jobKey = new JobKey("DailyReportJob");

            q.AddJob<WeeklyReport>(opt => opt.WithIdentity(jobKey));

            q.AddTrigger(opt => opt
                            .ForJob(jobKey)
                            .WithIdentity("DailyReportJob-Trigger")
                            .WithCronSchedule("0 00 08 ? * MON") // fire every monday on 08:00
            ) ;

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        });

        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();

            var jobKey = new JobKey("MonthlyReportJob");

            q.AddJob<MonthlyReport>(opt => opt.WithIdentity(jobKey));

            q.AddTrigger(opt => opt
                            .ForJob(jobKey)
                            .WithIdentity("MonthlyReportJob-Trigger")
                            .WithCronSchedule("0 00 08 ? * 1#1") // Fire first monday every month on 08:00
            );

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        });
    })
    .Build();

await host.RunAsync();