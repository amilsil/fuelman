using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public class Vehicle
    {
        public Vehicle()
        {
            this.Refills = new HashSet<Refill>();
        }

        [Key]
        public int VehicleId { get; set; }

        public int BrandId { get; set; }

        [Required]
        public virtual Brand Brand { get; set; }

        public int ModelId { get; set; }

        [Required]
        public virtual Model Model { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        //[Required]
        public RefillUnit RefillUnit { get; set; }

        public ICollection<Refill> Refills { get; set; }
    }

    public class Brand
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        [Key]
        [Column(Order = 1)]
        public int BrandId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(30)]
        [Required]
        public string BrandName { get; set; }

        public ICollection<Model> Models { get; set; }
    }

    public class Model
    {
        [Key]
        public int ModelId { get; set; }

        [StringLength(30)]
        [Required]
        public string ModelName { get; set; }

        public Brand Brand { get; set; }
    }

    public class Refill
    {
        [Key]
        public int RefillId { get; set; }

        [Required]
        public RefillUnit RefillUnit { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public DateTime RefillDate { get; set; }

        [Required]
        public long Odometer { get; set; }

        public Vehicle Vehicle { get; set; }
    }

    public class RefillUnit
    {
        [Key]
        [StringLength(5)]
        public string Unit { get; set; }
    }
}