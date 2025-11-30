using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Ocelot configuration dosyasını yükle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2️⃣ Ocelot servislerini ekle
builder.Services.AddOcelot(builder.Configuration);

// 3️⃣ Swagger veya UI eklemek istersen (isteğe bağlı)
 builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

// 4️⃣ Middleware pipeline
// Eğer Swagger eklediysen: app.UseSwaggerForOcelotUI(...);

app.UseRouting();

// 5️⃣ Ocelot middleware
await app.UseOcelot();

app.Run();