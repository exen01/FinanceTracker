using FinanceTracker.Domain.Abstractions;
using FinanceTracker.Domain.Logger;
using FinanceTracker.Domain.Services;
using FinanceTracker.Infrastructure.Configuration;
using FinanceTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerWeb
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
      builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
      builder.Services.AddScoped<ITransactionService, TransactionService>();
      builder.Services.AddScoped<ICategoryService, CategoryService>();
      builder.Services.AddSingleton<Logger>(provider =>
        new Logger(Path.Combine(Directory.GetCurrentDirectory(), "Logs", "Transactions.log")));

      // Add services to the container.
      builder.Services.AddRazorPages();

      builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(
          builder.Configuration.GetConnectionString("ConnectionString")
        )
      );

      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (!app.Environment.IsDevelopment())
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.MapRazorPages();

      app.Run();
    }
  }
}