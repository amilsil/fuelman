using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fuelman.Models
{
    public class Vehicle : IBaseEntity
    {
        public Vehicle()
        {
            if(this.Refills == null)
                this.Refills = new HashSet<Refill>();
        }

        [Key]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        public int BrandId { get; set; }
        [JsonIgnore]
        public virtual Brand Brand { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public virtual string BrandName
        {
            get
            {
                if (this.Brand != null)
                    return this.Brand.BrandName;
                else
                    return "";
            }
        }

        public int ModelId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public virtual string ModelName
        {
            get
            {
                if (this.Model != null)
                    return this.Model.ModelName;
                else
                    return "";
            }
        }

        public int RefillUnitId { get; set; }
        [ScaffoldColumn(false)]
        public RefillUnit RefillUnit { get; set; }
        public ICollection<Refill> Refills { get; set; }
    }

    public class Brand : IBaseEntity
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }

        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string BrandName { get; set; }

        public ICollection<Model> Models { get; set; }
    }

    public class Model : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string ModelName { get; set; }
        
        public Brand Brand { get; set; }
    }

    public class Refill : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public float RefillAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime RefillDate { get; set; }

        [Required]
        public long Odometer { get; set; }

        [Required]
        [JsonConverter(typeof(BoolJsonConverter))]
        public bool IsFullTank { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }

    public class RefillUnit : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(5)]        
        public string Unit { get; set; }

        [StringLength(7)]
        public string Description { get; set; }
    }
}