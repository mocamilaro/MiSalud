using lib_entidades.Modelos;
using lib_utilidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace lib_repositorios
{
    public class Conexion : DbContext
    {
        public string? StringConnection { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(StringConnection))
            {
                optionsBuilder.UseNpgsql(StringConnection, p =>
                {
                    p.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), null);
                    p.MapEnum<EstadoCita>("estado_cita"); // CAMBIO 1: Puse el mapeo de enum
                });

                // Configuración para mejorar rendimiento
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                optionsBuilder.EnableDetailedErrors();
                optionsBuilder.EnableSensitiveDataLogging(); // Solo para desarrollo
            }
        }

        // DbSets para cada entidad

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Secretaria> Secretarias { get; set; }
        public DbSet<HistoriaClinica> HistoriasClinicas { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }
        public DbSet<Tratamiento> Tratamientos { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configuración de herencia (TPH - Table Per Hierarchy)
            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Paciente>().ToTable("paciente");
            modelBuilder.Entity<Medico>().ToTable("medico");
            modelBuilder.Entity<Secretaria>().ToTable("secretaria");


            // Registra el enum de PostgreSQL, esto se hace para manejar el tipo ENUM
            modelBuilder.HasPostgresEnum<EstadoCita>("estado_cita"); 

            // Configura la propiedad Estado en la entidad Cita
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.Property(e => e.Estado)
                      .HasColumnType("estado_cita")  // Usa el tipo ENUM de PostgreSQL
                      .HasConversion<string>();  // Opcional: Convierte el enum a string al guardar/leer
            });

            // Configuración de relaciones especiales

            modelBuilder.Entity<Cita>()
               .HasOne(c => c.Paciente)
               .WithMany(p => p.Citas) // Un paciente puede tener muchas citas
               .HasForeignKey(c => c.PacienteId) // Clave foránea en Cita
               .OnDelete(DeleteBehavior.Restrict);// Evita eliminación en cascada

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Cita)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull); // Si se borra la cita, no se borra la notificación

            // Configuración de índices para mejor rendimiento
            modelBuilder.Entity<Persona>().HasIndex(p => p.Cedula).IsUnique();
            modelBuilder.Entity<Medico>().HasIndex(m => m.Especialidad);
            modelBuilder.Entity<Cita>().HasIndex(c => new { c.Fecha, c.MedicoId });

       
        }

        #region Métodos Genéricos (similares a tu implementación actual)

        public virtual DbSet<T> ObtenerSet<T>() where T : class => Set<T>();

        public virtual List<T> Listar<T>() where T : class =>
            Set<T>().AsNoTracking().ToList();

        public virtual List<T> Buscar<T>(Expression<Func<T, bool>> condiciones) where T : class =>
            Set<T>().AsNoTracking().Where(condiciones).ToList();

        // Métodos específicos con includes para relaciones comunes
        public virtual List<Cita> BuscarCitas(Expression<Func<Cita, bool>> condiciones)
        {
            return Set<Cita>()
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .Include(c => c.Secretaria)
                .Where(condiciones)
                .AsNoTracking()
                .ToList();
        }

        public virtual List<Paciente> BuscarPacientes(Expression<Func<Paciente, bool>> condiciones)
        {
            return Set<Paciente>()
                .Include(p => p.HistoriaClinica)
                .ThenInclude(h => h.Diagnosticos)
                .Where(condiciones)
                .AsTracking()
                .ToList();
        }

        public virtual List<HistoriaClinica> BuscarHistorias(Expression<Func<HistoriaClinica, bool>> condiciones)
        {
            var historias = Set<HistoriaClinica>()
                .Include(h => h.Paciente)
                .ThenInclude(p => p.Citas)
                .ThenInclude(c => c.Medico)
                .Include(h => h.Diagnosticos)
                .ThenInclude(d => d.Medico)
                .Include(h => h.Tratamientos)
                .Include(h => h.Formulas)
                .ThenInclude(f => f.Medicamentos)
                .Where(condiciones)
                .ToList();

            return historias;

        }

        public virtual bool Existe<T>(Expression<Func<T, bool>> condiciones) where T : class =>
            Set<T>().Any(condiciones);

        public virtual void Guardar<T>(T entidad) where T : class =>
            Set<T>().Add(entidad);

        public virtual async Task GuardarAsync<T>(T entidad) where T : class =>
            await Set<T>().AddAsync(entidad);

        public virtual void Modificar<T>(T entidad) where T : class =>
            Entry(entidad).State = EntityState.Modified;

        public virtual void Borrar<T>(T entidad) where T : class =>
            Set<T>().Remove(entidad);

        public virtual void Separar<T>(T entidad) where T : class =>
            Entry(entidad).State = EntityState.Detached;

        public virtual int GuardarCambios() => SaveChanges();

        public virtual async Task<int> GuardarCambiosAsync() =>
            await SaveChangesAsync();

        #endregion

        #region Métodos Específicos para gestionar

        public virtual async Task<List<Cita>> ObtenerAgendaMedico(int medicoId, DateTime fecha)
        {
            return await Set<Cita>()
                .Include(c => c.Paciente)
                .Where(c => c.MedicoId == medicoId && c.Fecha == fecha.Date)
                .OrderBy(c => c.Hora)
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<List<Notificacion>> ObtenerNotificacionesPendientes(int pacienteId)
        {
            return await Set<Notificacion>()
                .Where(n => n.PacienteId == pacienteId && n.Estado == "Pendiente")
                .OrderByDescending(n => n.FechaEnvio)
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion
    }
}