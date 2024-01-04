var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddAuthenticationCore();
builder.Services.AddScoped<AuthenticationStateProvider, LabAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IUsuarioService<AlunoDTO>, AlunoService>();
builder.Services.AddScoped<IUsuarioService<RegistrarUsuarioDTO>, ProfessorService>();
builder.Services.AddScoped<ITreinosService, TreinosService>();
builder.Services.AddScoped<IExercicioService, ExercicioService>();
builder.Services.AddScoped<IUsuarioTreinoService, UsuarioTreinoService>();
builder.Services.AddScoped<ISystemStringHelper, SystemStringHelper>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudServices();
builder.Services.AddHttpClient("LabAspNetIdentity", a =>
{
#if DEBUG
    a.BaseAddress = new Uri("https://localhost:7121");
#else
    a.BaseAddress = new Uri("https://providerjwt20231223203207.azurewebsites.net");
#endif
});
builder.Services.AddHttpClient("LabAcademiaAPI", a =>
{
#if DEBUG
    a.BaseAddress = new Uri("http://localhost:5239");
#else
    a.BaseAddress = new Uri("https://labacademiaapi20231226214200.azurewebsites.net");
#endif
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
