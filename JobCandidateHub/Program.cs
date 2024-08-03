using JobCandidateHub.Configurations;
using JobCandidateHub.Repositories;
using JobCandidateHub.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");
if (databaseProvider == "SqlServer")
{
    builder.Services.AddDbContext<CandidateContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else if (databaseProvider == "PostgreSQL")
{
    //builder.Services.AddDbContext<CandidateContext>(options =>
    //    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddMemoryCache();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    await next();
});

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
