namespace DemoMVVM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SinhVienModel : DbContext
    {
        public SinhVienModel()
            : base("name=SinhVienModel")
        {
        }

        public virtual DbSet<Lop> Lop { get; set; }
        public virtual DbSet<SinhVien> SinhVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lop>()
                .Property(e => e.IdLop)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<SinhVien>()
                .Property(e => e.IdLop)
                .IsUnicode(false);
        }
    }
}
