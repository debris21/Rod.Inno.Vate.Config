public class StartUp
{
    string? rPalValue = "";
    public void ConfigureServices(IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        rPalValue = configuration["rPal"];
        var allowOOrigins = configuration.GetSection("allowO").GetChildren().Select(x => x.Value).ToArray();
        services.AddControllers().AddApplicationPart(System.Reflection.Assembly.Load("Rod.Inno.Vate.Config.Api"));
        services.AddCors(r =>
            r.AddPolicy(name: rPalValue, rp =>
            {
                rp.WithOrigins(allowOOrigins).AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            })
            );
        //services.AddSwaggerGen();
        //services.AddScoped<ABSTACTCOUGARDEPENDENCY>();
        //services.AddScoped<ICOUGARSERVICE, COUGARSERVICE>();
        //services.AddScoped<ICOUGARACCESS, COUGARACCESS>();
        services.AddSignalR();
        services.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            //app.UseSwagger(); 
            //app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCors(rPalValue);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}