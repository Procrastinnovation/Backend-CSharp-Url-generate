using JLimUrl;
using JLimUrl.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UrlShorteningService>();

var app = builder.Build();
app.UseCors(builder =>
      {
        builder
              .WithOrigins("http://localhost:4200", "https://localhost:4200")
              .SetIsOriginAllowedToAllowWildcardSubdomains()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS")
              .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
 
      }
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("api/url", (
    ShortUrlRequest request,
    UrlShorteningService urlShorteningService,
    HttpContext httpContext) =>
{
    if (!Uri.TryCreate(request.Url, UriKind.Absolute, out _))
    {
        return Results.BadRequest("URL is invalid");
    }

    var code = urlShorteningService.GenerateUniqueCode();

    var ShortenedUrl = new ShortenedUrl
    {
        Id = Guid.NewGuid(),
        RealUrl = request.Url,
        Code = code,
        ShortUrl = $"{httpContext.Request.Scheme}://tinyurl.com/{code}",
        CreatedOnUtc = DateTime.Now
    };


    return Results.Ok(ShortenedUrl.ShortUrl);
});

app.Run();