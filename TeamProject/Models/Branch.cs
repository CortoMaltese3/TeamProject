namespace TeamProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Branch")]
    public partial class Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Branch()
        {
            Court = new HashSet<Court>();
            Review = new HashSet<Review>();
            Facility = new HashSet<Facility>();
        }

        public int ID { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }
        
        [Required]
        [StringLength(20)]
        public string City { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(200)]
        public string ZipCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Court> Court { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Facility> Facility { get; set; }

    }
}
