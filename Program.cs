using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSignalR().AddAzureSignalR();

var connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connection));

var connectionURI = builder.Configuration.GetConnectionString("AZURE_TEXTANALYTICS");
var azureKey = builder.Configuration.GetConnectionString("AZURE_KEY");

builder.Services.AddSingleton(new TextAnalyticsClient(new Uri(connectionURI), new AzureKeyCredential(azureKey)));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapHub<ChatHub>(ChatHub.HubUrl);
app.MapFallbackToPage("/_Host");

app.Run();
