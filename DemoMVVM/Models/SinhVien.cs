namespace DemoMVVM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SinhVien")]
    public partial class SinhVien
    {
        [StringLength(20)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        [StringLength(20)]
        public string IdLop { get; set; }

        public virtual Lop Lop { get; set; }
    }
}
