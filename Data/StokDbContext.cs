using DepoStok.Models.Entities;
using DepoStok.Models.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DepoStok.Data
{
    public class StokDbContext : IdentityDbContext<AppUser,IdentityRole<int>,int >
    {
        public StokDbContext(DbContextOptions<StokDbContext> options) : base(options) { }

        public DbSet<malzeme> malzemeler { get; set; }
        public DbSet<depo> depolar {  get; set; }
        public DbSet<stok> stoklar { get; set; }
        public DbSet<cari> cariler {  get; set; }
        public DbSet<logTakip> logTakipler { get; set; }
        //public DbSet<kullanici> kullanicilar { get; set; }
        public DbSet<depoTransfer> depoTransferleri { get; set; }
        public DbSet<depoTransferDetay> depoTransferDetaylari { get; set; }
        public DbSet<irsaliye> irsaliyeler { get; set; }
        public DbSet<irsaliyeDetay> irsaliyeDetaylari { get; set; }
        public DbSet<Vw_StokDurumu> Vw_StokDurumu { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                base.OnModelCreating(modelBuilder);

            //enum hareket tipi
            modelBuilder.Entity<stok>()
                .Property(s => s.HareketTipi)
                .HasConversion<int>();

            //malzeme stok ilişkisi
            modelBuilder.Entity<stok>()
                .HasOne(s=>s.Malzeme)
                .WithMany()
                .HasForeignKey(s=>s.MalzemeId)
                .OnDelete(DeleteBehavior.Restrict);

            //depo stok ilişkisi
            modelBuilder.Entity<stok>()
                .HasOne(s=>s.Depo)
                .WithMany()
                .HasForeignKey(s=>s.DepoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<stok>()
    .HasOne(s => s.cari)
    .WithMany()
    .HasForeignKey(s => s.carId)
    .OnDelete(DeleteBehavior.Restrict);


            //cari irsaliye ilişki
            modelBuilder.Entity<irsaliye>()
                .HasOne(i=>i.cari)
                .WithMany()
                .HasForeignKey(i=>i.carId)
                .OnDelete(DeleteBehavior.Restrict);
            

            modelBuilder.Entity<logTakip>()
                .HasOne(l => l.AppUser)
                .WithMany()
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);


            //irsaliye irsaaliyedetay ilişkisi

            modelBuilder.Entity<irsaliyeDetay>()
                .HasOne(d => d.irsaliye)
                .WithMany(i => i.irsaliyeDetaylari)
                .HasForeignKey(d => d.irsaliyeId)
                .OnDelete(DeleteBehavior.Restrict);

            //malzeme irsaliyedetay
            modelBuilder.Entity<irsaliyeDetay>()
                .HasOne(id=>id.malzeme)
                .WithMany()
                .HasForeignKey(id=>id.malzemeId)
                .OnDelete(DeleteBehavior.Restrict);

            //depo transfer ve detayı ilişkisi
            modelBuilder.Entity<depoTransferDetay>()
                .HasOne(d => d.depoTransfer)
                .WithMany(dt => dt.depoTransferDetaylari)
                .HasForeignKey(d => d.transferId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<depoTransferDetay>()
                .HasOne(d => d.malzeme)
                .WithMany()
                .HasForeignKey(d => d.malzemeId)
                .OnDelete(DeleteBehavior.Restrict);

            //depo transfer kaynak ve hedef depo
            modelBuilder.Entity<depoTransfer>()
                .HasOne(dt=>dt.kaynakDepo)
                .WithMany()
                .HasForeignKey(dt=>dt.kaynakDepoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<depoTransfer>()
                .HasOne(dt=>dt.hedefDepo)
                .WithMany()
                .HasForeignKey(dt=>dt.hedefDepoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vw_StokDurumu>()
                .HasNoKey().ToView("Vw_StokDurumu");

        }
    }
}
