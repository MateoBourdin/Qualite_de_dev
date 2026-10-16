using ClientBlazor.Components;
using ClientBlazor.Dtos;
using ClientBlazor.Services;
using ClientBlazor.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = builder.Configuration["ApiBaseUrl"]
    ?? throw new InvalidOperationException("ApiBaseUrl manquant dans appsettings.json");

builder.Services.AddHttpClient<IApiService<ProduitDto, ProduitDetailDto>, ProduitService>(c => c.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IApiService<MarqueDto, MarqueDto>, MarqueService>(c => c.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IApiService<TypeProduitDto, TypeProduitDto>, TypeProduitService>(c => c.BaseAddress = new Uri(apiUrl));

// ViewModels génériques : un seul enregistrement pour toutes les entités
builder.Services.AddScoped(typeof(ListViewModel<,>));
builder.Services.AddScoped(typeof(DetailViewModel<,>));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
