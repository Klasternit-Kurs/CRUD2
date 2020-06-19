using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD2
{
	public class BazaXYZ : DbContext
	{
		public DbSet<Artikal> Artikals { get; set; }
		public DbSet<Racun> Racuns { get; set; }
		public DbSet<ArtKol> ArtKols { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Artikal>().HasKey(a => a.Sifra);
			modelBuilder.Entity<Racun>().HasKey(r => r.ID);
			modelBuilder.Entity<ArtKol>().HasKey(ak => ak.ID);
		}

	}
}
