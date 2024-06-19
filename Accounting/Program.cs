using Accounting.Components;
using Accounting.Interfaces;
using Accounting.Repos;

using Microsoft.JSInterop;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLocalization();
builder.Services.AddBlazorBootstrap();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => { options.DetailedErrors = true; }); 

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IBankAccountRepo>(sp => new BankAccountRepo(connectionString));
builder.Services.AddScoped<IDailyTransactionRepo>(sp => new DailyTransactionRepo(connectionString));
builder.Services.AddScoped<ILedgerRepo>(sp => new LedgerRepo(connectionString));
builder.Services.AddScoped<ICustomerRepo>(sp => new CustomerRepo(connectionString));
builder.Services.AddScoped<IChartOfAccountRepo>(sp => new  ChartOfAccountRepo(connectionString));
builder.Services.AddScoped<IExpenseEntryRepo>(sp => new ExpenseEntryRepo(connectionString));
builder.Services.AddScoped<IPurchaseEntryRepo>(sp => new  PurchaseEntryRepo(connectionString));
builder.Services.AddScoped<IPurchaseItemRepo>(sp => new PurchaseItemRepo(connectionString));
builder.Services.AddScoped<IPartyRepo>(sp => new PartyRepo(connectionString));
builder.Services.AddScoped<ISettingRepo>(sp => new SettingRepo(connectionString));

var app = builder.Build();


var supportedCultures = new[] { "en-US", "ne-NP" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); 

app.Run();
