using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
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
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<Conexion, Conexion>();

            // Repositorios
            services.AddScoped<IPruebaConexionRepositorio, PruebaConexionRepositorio>();
            services.AddScoped<ICitaRepositorio, CitaRepositorio>();
            services.AddScoped<IDiagnosticoRepositorio, DiagnosticoRepositorio>();
            services.AddScoped<IFormulaRepositorio, FormulaRepositorio>();
            services.AddScoped<IHistoriaClinicaRepositorio, HistoriaClinicaRepositorio>();
            services.AddScoped<IMedicamentoRepositorio, MedicamentoRepositorio>();
            services.AddScoped<IMedicoRepositorio, MedicoRepositorio>();
            services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
            services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
            services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
            services.AddScoped<IRecomendacionRepositorio, RecomendacionRepositorio>();
            services.AddScoped<ISecretariaRepositorio, SecretariaRepositorio>();
            services.AddScoped<ITratamientoRepositorio, TratamientoRepositorio>();

            // Aplicaciones
            services.AddScoped<ICitaAplicacion, CitaAplicacion>();
            services.AddScoped<IDiagnosticoAplicacion, DiagnosticoAplicacion>();
            services.AddScoped<IFormulaAplicacion, FormulaAplicacion>();
            services.AddScoped<IHistoriaClinicaAplicacion, HistoriaClinicaAplicacion>();
            services.AddScoped<IMedicamentoAplicacion, MedicamentoAplicacion>();
            services.AddScoped<IMedicoAplicacion, MedicoAplicacion>();
            services.AddScoped<INotificacionAplicacion, NotificacionAplicacion>();
            services.AddScoped<IPacienteAplicacion, PacienteAplicacion>();
            services.AddScoped<IPersonaAplicacion, PersonaAplicacion>();
            services.AddScoped<IRecomendacionAplicacion, RecomendacionAplicacion>();
            services.AddScoped<ISecretariaAplicacion, SecretariaAplicacion>();
            services.AddScoped<ITratamientoAplicacion, TratamientoAplicacion>();

            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                });
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            app.UseRouting();
            app.UseCors();
        }
    }
}