using lib_comunicaciones.Implementaciones;
using lib_comunicaciones.Interfaces;
using lib_presentaciones.Implementaciones;
using lib_presentaciones.Interfaces;

namespace asp_presentacion
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            // Comunicaciones
            services.AddScoped<ICitaComunicacion, CitaComunicacion>();
            // Presentaciones
            services.AddScoped<ICitaPresentacion, CitaPresentacion>();

            services.AddScoped<IDiagnosticoComunicacion, DiagnosticoComunicacion>();
            // Presentaciones
            services.AddScoped<IDiagnosticoPresentacion, DiagnosticoPresentacion>();

            services.AddScoped<IFormulaComunicacion, FormulaComunicacion>();
            // Presentaciones
            services.AddScoped<IFormulaPresentacion, FormulaPresentacion>();

            services.AddScoped<IPersonaComunicacion, PersonaComunicacion>();
            // Presentaciones
            services.AddScoped<IPersonaPresentacion, PersonaPresentacion>();

            services.AddScoped<IHistoriaClinicaComunicacion, HistoriaClinicaComunicacion>();
            // Presentaciones
            services.AddScoped<IHistoriaClinicaPresentacion, HistoriaClinicaPresentacion>();

            services.AddScoped<IMedicamentoComunicacion, MedicamentoComunicacion>();
            // Presentaciones
            services.AddScoped<IMedicamentoPresentacion, MedicamentoPresentacion>();

            services.AddScoped<IMedicoComunicacion, MedicoComunicacion>();
            // Presentaciones
            services.AddScoped<IMedicoPresentacion, MedicoPresentacion>();

            services.AddScoped<INotificacionComunicacion, NotificacionComunicacion>();
            // Presentaciones
            services.AddScoped<INotificacionPresentacion, NotificacionPresentacion>();

            services.AddScoped<IPacienteComunicacion, PacienteComunicacion>();
            // Presentaciones
            services.AddScoped<IPacientePresentacion, PacientePresentacion>();

            services.AddScoped<IRecomendacionComunicacion, RecomendacionComunicacion>();
            // Presentaciones
            services.AddScoped<IRecomendacionPresentacion, RecomendacionPresentacion>();

            services.AddScoped<ISecretariaComunicacion, SecretariaComunicacion>();
            // Presentaciones
            services.AddScoped<ISecretariaPresentacion, SecretariaPresentacion>();

            services.AddScoped<ITratamientoComunicacion, TratamientoComunicacion>();
            // Presentaciones
            services.AddScoped<ITratamientoPresentacion, TratamientoPresentacion>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddRazorPages();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.UseSession();
            app.Run();
        }
    }
}