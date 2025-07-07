using BDRiccosModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BDRiccosModel;

public partial class _dbRiccosContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
ApplicationUserRole, IdentityUserLogin<string>,
IdentityRoleClaim<string>, IdentityUserToken<string>>
{
	public _dbRiccosContext()
	{
	}
	public virtual DbSet<EmailSuscriptor> EmailSuscriptors { get; set; }


	/*public _dbRiccosContext(DbContextOptions<_dbRiccosContext> options)
        : base(options)
    {
    }*/
	public virtual DbSet<HorariosRegulares> HorariosRegulares { get; set; }
	public virtual DbSet<Empresa> Empresas { get; set; }



	public virtual DbSet<Asunto> Asuntos { get; set; }
	public virtual DbSet<EnTienda> Entiendas { get; set; }



	public virtual DbSet<Categoria> Categoria { get; set; }

	public virtual DbSet<Cliente> Clientes { get; set; }

	public virtual DbSet<ClientePersonaJuridica> ClientePersonaJuridicas { get; set; }

	public virtual DbSet<ClientePersonaNatural> ClientePersonaNaturals { get; set; }

	public virtual DbSet<Delivery> Deliveries { get; set; }

	public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
	public virtual DbSet<DetallesPromocion> DetallesPromocions { get; set; }


	public virtual DbSet<EnTienda> EnTiendas { get; set; }

	public virtual DbSet<EstadoMensaje> EstadoMensajes { get; set; }

	public virtual DbSet<EstadoVenta> EstadoVenta { get; set; }

	public virtual DbSet<MensajeContacto> MensajeContactos { get; set; }

	public virtual DbSet<Mesa> Mesas { get; set; }

	public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

	public virtual DbSet<OnlineUser> OnlineUsers { get; set; }

	public virtual DbSet<Pedido> Pedidos { get; set; }

	public virtual DbSet<PersonaJuridica> PersonaJuridicas { get; set; }

	public virtual DbSet<PersonaNatural> PersonaNaturals { get; set; }

	public virtual DbSet<PersonaTipoDocumento> PersonaTipoDocumentos { get; set; }

	public virtual DbSet<PersonalEmpresa> PersonalEmpresas { get; set; }

	public virtual DbSet<Pickup> Pickups { get; set; }

	public virtual DbSet<Producto> Productos { get; set; }

	public virtual DbSet<Promocion> Promocions { get; set; }

	public virtual DbSet<Remite> Remites { get; set; }

	public virtual DbSet<Service> Services { get; set; }


	public virtual DbSet<UsuariosSistema> UsuariosSistemas { get; set; }


	public virtual DbSet<VistaDetallePedido> VistaDetallePedidos { get; set; }

	public virtual DbSet<Venta> Ventas { get; set; }

	public virtual DbSet<VistaVenta> VistaVenta { get; set; }


	/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
	   => optionsBuilder.UseSqlServer("workstation id=riccos_db.mssql.somee.com;packet size=4096;user id=fanny1010_SQLLogin_1;pwd=aajsv5zdwp;data source=riccos_db.mssql.somee.com;persist security info=False;initial catalog=riccos_db;TrustServerCertificate=True");*/



	/*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
			 => optionsBuilder.UseSqlServer("Data Source=MARIN\\SQLEXPRESS;Initial Catalog=RiccosDB;Integrated Security=True;Trusted_Connection=true;Trust Server Certificate=true");*/



	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		//Este método comprueba si ya se configuró una cadena de conexión desde otro lugar
		if (!optionsBuilder.IsConfigured)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.Development.json")
				.Build();

			var connectionString = configuration.GetConnectionString("RiccosDbSomee1");
			optionsBuilder.UseSqlServer(connectionString);
		}
	}



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		/*modelBuilder.Entity<VistaDetallePedido>(entity =>
        {
            entity.ToView("VistaDetallePedido");
        });*/

		/* modelBuilder.Entity<UsuariosSistema>()
        .HasOne(u => u.IdApplicationUserNavigation)
        .WithMany()
        .HasForeignKey(u => u.IdApplicationUser)
        .HasPrincipalKey(a => a.Id); // `Id` es la clave primaria en ApplicationUser

        */

		//modelBuilder.Entity<ApplicationUser>().HasKey<string>(l => l.Id); 


		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<ApplicationUserRole>(userRole =>
		{
			userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

			userRole.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId);

			userRole.HasOne(ur => ur.User)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.UserId);
		});

		/*modelBuilder.Entity<DetallesPromocion>(entity =>
    {
        entity.HasOne<Producto>()
              .WithMany()
              .HasForeignKey(dp => dp.IdProducto)
              .OnDelete(DeleteBehavior.Cascade);

        entity.HasOne<Promocion>()
              .WithMany()
              .HasForeignKey(dp => dp.IdPromocion)
              .OnDelete(DeleteBehavior.Cascade);
    });*/

	}
}
